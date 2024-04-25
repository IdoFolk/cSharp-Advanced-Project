namespace TileMapEngine.CoreEngine.ConsoleCommands;

public struct ConsoleCommand
{
    public string Command;
    public string Description;
    public bool HasArgument;
    
    private Action<string> _callback;
    
    public ConsoleCommand(string command, string description, bool hasArgument, Action<string> callback)
    {
        Command = command;
        Description = description;
        HasArgument = hasArgument;
        _callback = callback;
    }
    
    public override string ToString()
    {
        return $"{Command}\t{Description}";
    }

    public void Execute(string arg = "")
    {
        _callback(arg); // Execute the callback with the argument
    }
}