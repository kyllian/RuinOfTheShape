namespace RuinOfTheShape.Simulation;

public sealed class FixedTickSystem : ISimulationTickSystem
{
    private long _tick;

    public void Initialize()
    {
        _tick = 0;
    }

    public void Tick(int ticks = 1)
    {
        if (ticks < 0)
        {
            ticks = 0;
        }

        _tick += ticks;
    }
}

