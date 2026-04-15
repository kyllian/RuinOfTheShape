namespace RuinOfTheShape.Core;

public readonly record struct Result(bool IsOk, string Message)
{
    public static Result Ok(string message = "ok") => new(true, message);
    public static Result Fail(string message) => new(false, message);
}

