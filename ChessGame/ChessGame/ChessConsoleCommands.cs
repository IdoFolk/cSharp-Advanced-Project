using ConsoleRenderer;
using ConsoleRenderer.ConsoleCommands;
using TileMapEngine.CoreEngine;

namespace ChessGame;

public class ChessConsoleCommands
{
    public void Init(ConsoleGameLoopManager consoleGameLoop)
    {
        var commandsManager = consoleGameLoop.GetConsoleCommandsManager();

        var chessSelect = new ConsoleCommand("gselect",
            "Identical to the /select command but with the guides convention. example: /gselect H,8",
            true,
            HandleChessSelectCommand);
        commandsManager.AddCommand(chessSelect);

        var chessMove = new ConsoleCommand("gmove",
            "Identical to the /move command but with the guides convention. example: /gmove A,1",
            true,
            HandleChessMoveCommand);
        commandsManager.AddCommand(chessMove);
    }

    private bool HandleChessSelectCommand(CommandCallbackArguments commandCallbackArguments)
    {
        if (!GetPositionFromGuidesArgs(commandCallbackArguments.args, out var position2D)) return false;

        if (!TileMapManager.TrySelect(commandCallbackArguments.playingActor, position2D)) return false;

        var splitArgs = commandCallbackArguments.args.Split(',');
        Console.WriteLine($"Selected tile object at {splitArgs[0]},{splitArgs[1]}\n");
        TileMapManager.RefreshGameViewport(false);
        return true;
    }

    private bool HandleChessMoveCommand(CommandCallbackArguments commandCallbackArguments)
    {
        if (!TileMapManager.GetIsAnySelected())
        {
            return false;
        }

        if (!GetPositionFromGuidesArgs(commandCallbackArguments.args, out var position2D)) return false;

        if (!TileMapManager.TryMove(position2D)) return false;

        var splitArgs = commandCallbackArguments.args.Split(',');
        Console.WriteLine(
            $"Moved to {splitArgs[0]},{splitArgs[1]} and ended {commandCallbackArguments.playingActor.ActorName}'s turn.\n");

        TileMapManager.RefreshGameViewport(false);
        return true;
    }

    private static bool GetPositionFromGuidesArgs(string args, out Position2D position2D)
    {
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var splitArgs = args.Split(',');
        if (splitArgs.Length != 2 || splitArgs[0].Length != 1 || splitArgs[1].Length != 1)
        {
            position2D = default;
            return false;
        }

        var letterArg = splitArgs[0][0];

        var x = -1;
        for (var i = 0; i < ChessGame.BoardSize; i++)
        {
            if (letters[i] != letterArg) continue;

            x = i;
            break;
        }

        if (x == -1)
        {
            position2D = default;
            return false;
        }

        if (!int.TryParse(splitArgs[1], out var y))
        {
            position2D = default;
            return false;
        }

        y -= 1; // We subtract 1 because it's according to guides and not indecies
        position2D = new Position2D(x, y);
        return true;
    }
}