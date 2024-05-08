using TileMapEngine.CoreEngine;

namespace ConsoleRenderer.ConsoleCommands;

public readonly struct ConsoleCommand(
    string command, 
    string description, 
    bool hasArgument, 
    Predicate<CommandCallbackArguments> callback)
{
    public readonly string Command = command;
    public readonly bool HasArgument = hasArgument;

    public override string ToString()
    {
        return $"{Command}\t{description}";
    }

    public bool Execute(Actor playingActor, string arg = "")
    {
        var callbackArg = new CommandCallbackArguments() { playingActor = playingActor, args = arg};
        return callback(callbackArg); // Execute the callback with the argument
    }
}

public struct CommandCallbackArguments
{
    public string args;
    public Actor playingActor;
}