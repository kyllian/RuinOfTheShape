shader_type canvas_item;

uniform sampler2D screen_texture : hint_screen_texture, repeat_disable, filter_linear_mipmap;

/// 0 = Tier0 (pass-through), 1 = Tier1, 2 = Tier2 (slightly stronger response).
uniform float tier_index : hint_range(0.0, 2.0, 1.0) = 1.0;
uniform float edge_threshold : hint_range(0.0, 0.5, 0.001) = 0.075;
uniform float edge_intensity : hint_range(0.0, 1.5, 0.001) = 0.55;
uniform vec4 edge_tint : source_color = vec4(0.02, 0.02, 0.045, 1.0);
/// Blends Sobel magnitude with cardinal finite differences to reduce single-pixel sparkle.
uniform float neighbor_soften : hint_range(0.0, 1.0, 0.01) = 0.38;

float luminance(vec3 c) {
	return dot(c, vec3(0.299, 0.587, 0.114));
}

float edge_metric(vec3 c) {
	float y = luminance(c);
	float chroma = abs(c.r - c.g) + abs(c.g - c.b) + abs(c.b - c.r);
	return y + 0.33 * chroma;
}

float sample_metric(vec2 uv) {
	return edge_metric(textureLod(screen_texture, uv, 0.0).rgb);
}

void fragment() {
	vec3 base = textureLod(screen_texture, SCREEN_UV, 0.0).rgb;

	if (tier_index < 0.5) {
		COLOR = vec4(base, 1.0);
	} else {
		vec2 ps = SCREEN_PIXEL_SIZE;
		float tier_boost = tier_index > 1.5 ? 1.22 : 1.0;

		float s00 = sample_metric(SCREEN_UV + vec2(-ps.x, -ps.y));
		float s01 = sample_metric(SCREEN_UV + vec2(0.0, -ps.y));
		float s02 = sample_metric(SCREEN_UV + vec2(ps.x, -ps.y));
		float s10 = sample_metric(SCREEN_UV + vec2(-ps.x, 0.0));
		float s12 = sample_metric(SCREEN_UV + vec2(ps.x, 0.0));
		float s20 = sample_metric(SCREEN_UV + vec2(-ps.x, ps.y));
		float s21 = sample_metric(SCREEN_UV + vec2(0.0, ps.y));
		float s22 = sample_metric(SCREEN_UV + vec2(ps.x, ps.y));

		float gx = -s00 - 2.0 * s10 - s20 + s02 + 2.0 * s12 + s22;
		float gy = -s00 - 2.0 * s01 - s02 + s20 + 2.0 * s21 + s22;
		float edge = sqrt(gx * gx + gy * gy) * tier_boost;

		float e1 = abs(sample_metric(SCREEN_UV + vec2(ps.x, 0.0)) - sample_metric(SCREEN_UV - vec2(ps.x, 0.0)));
		float e2 = abs(sample_metric(SCREEN_UV + vec2(0.0, ps.y)) - sample_metric(SCREEN_UV - vec2(0.0, ps.y)));
		float edge_soft = mix(edge, (edge + e1 + e2) / 3.0, neighbor_soften);

		float edge_mask = smoothstep(edge_threshold, edge_threshold * 2.35, edge_soft);
		vec3 outc = base * (1.0 - edge_mask * edge_intensity) + edge_tint.rgb * (edge_mask * edge_intensity);
		COLOR = vec4(outc, 1.0);
	}
}
