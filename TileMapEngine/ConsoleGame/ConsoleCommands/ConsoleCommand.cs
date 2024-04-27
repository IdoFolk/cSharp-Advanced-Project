namespace ConsoleRenderer.ConsoleCommands;

public readonly struct ConsoleCommand(string command, string description, bool hasArgument, Action<string> callback)
{
    public readonly string Command = command;
    public readonly bool HasArgument = hasArgument;

    public override string ToString()
    {
        return $"{Command}\t{description}";
    }

    public void Execute(string arg = "")
    {
        callback(arg); // Execute the callback with the argument
    }
}