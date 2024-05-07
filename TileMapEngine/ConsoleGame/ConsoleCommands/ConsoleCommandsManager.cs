using TileMapEngine.CoreEngine;

namespace ConsoleRenderer.ConsoleCommands;

public class ConsoleCommandsManager
{
    private readonly List<ConsoleCommand> _availableCommands = new();

    public void Init()
    {
        var help = new ConsoleCommand("help",
            "Show all available commands. example: /help",
            false,
            _ => HandleHelpCommand(default, false));
        AddCommand(help);

        var select = new ConsoleCommand("select",
            "Select a tile object (if exists) at index position (x,y). example: /select 0,5",
            true,
            HandleSelectCommand);
        AddCommand(select);

        var deselect = new ConsoleCommand("deselect",
            "Deselect the current tile object. example: /deselect",
            false,
            _ => HandleDeselectCommand());
        AddCommand(deselect);

        var move = new ConsoleCommand("move",
            "Move the selected tile object (if exists) to index position (x,y). example: /move 0,1",
            true,
            HandleMoveCommand);
        AddCommand(move);

        var quit = new ConsoleCommand("quit",
            "Closes the game's application. example: /quit",
            true,
            _ => HandleOnQuitCommand());
        AddCommand(quit);

        var refresh = new ConsoleCommand("refresh",
            "Clears the console and re-draws the game updated game state onto the viewport. example: /refresh",
            false,
            _ =>
            {
                TileMapManager.RefreshGameViewport(true);
                return true;
            });
        AddCommand(refresh);
    }

    private static bool TryGetPositionFromArgument(string? args, out Position2D position2D)
    {
        var splitArgs = args?.Split(',');

        if (splitArgs is { Length: 2 } &&
            int.TryParse(splitArgs[0], out var x) &&
            int.TryParse(splitArgs[1], out var y))
        {
            position2D = new Position2D(x, y);
            return true;
        }

        position2D = default;
        return false;
    }

    public void AddCommand(ConsoleCommand command) => _availableCommands.Add(command);

    private void PrintAllCommands()
    {
        foreach (var command in _availableCommands)
        {
            Console.WriteLine($"- {command}");
        }
    }

    public void HandleUserInput(Actor playingActor)
    {
        Console.WriteLine();
        Console.Write($"Waiting for command from {playingActor.ActorName}: ");
        var input = Console.ReadLine();
        var splitInput = input?.Split(' ');

        var firstWordInInput = splitInput?[0];
        var args = "";

        if (splitInput?.Length > 1)
        {
            args = splitInput[1];
        }

        foreach (var command in _availableCommands.Where(command => $"/{command.Command}" == $"{firstWordInInput}"))
        {
            HandleCommandInput(playingActor, command, args);
            return;
        }

        HandleHelpCommand();
    }

    private void HandleCommandInput(Actor playingActor, ConsoleCommand command, string args)
    {
        if (command.HasArgument)
        {
            if (command.Execute(playingActor, args)) return;

            HandleHelpCommand();
            return;
        }

        if (!command.Execute(playingActor))
        {
            HandleHelpCommand();
        }
    }

    private bool HandleHelpCommand(Actor _ = default, bool isFallback = true)
    {
        var text = "\n";
        if (isFallback)
        {
            text += "Oops! Something went wrong with your command.\n\n";
        }

        Console.WriteLine(text +
                          "Below are all the possible commands.\n" +
                          "Please refer to each command's example to learn how to use it:\n");
        PrintAllCommands();
        return true;
    }

    private bool HandleSelectCommand(CommandCallbackArguments callbackArguments)
    {
        if (!TryGetPositionFromArgument(callbackArguments.args, out var position2D))
        {
            return false;
        }

        if (!TileMapManager.TrySelect(callbackArguments.playingActor, position2D)) return false;

        Console.WriteLine($"Selected tile object at {position2D.X},{position2D.Y}\n");
        TileMapManager.RefreshGameViewport(false);
        return true;
    }

    private bool HandleDeselectCommand()
    {
        if (!TileMapManager.TryDeselect()) return false;

        Console.WriteLine("Deselected current tile object\n");
        TileMapManager.RefreshGameViewport(false);
        return true;
    }

    private static bool HandleOnQuitCommand()
    {
        TileMapManager.StopGameLoop();
        return true;
    }

    private bool HandleMoveCommand(CommandCallbackArguments commandCallbackArguments)
    {
        if (!TryGetPositionFromArgument(commandCallbackArguments.args, out var position2D))
        {
            return false;
        }

        if (!TileMapManager.TryMove(position2D)) return false;

        Console.WriteLine(
            $"Moved to {position2D.X},{position2D.Y} and ended {commandCallbackArguments.playingActor.ActorName}'s turn.\n");
        TileMapManager.RefreshGameViewport(false);
        return true;
    }
}