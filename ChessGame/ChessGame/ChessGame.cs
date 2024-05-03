using System.Numerics;
using ConsoleRenderer;
using ConsoleRenderer.ConsoleCommands;
using ConsoleRenderer.ConsoleRenderer;
using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace ChessGame;

public class ChessGame
{
    public void RunChessGame()
    {
        ConsoleGameLoopManager.Init();

        ConfigTileMap();

        ConfigGameConsoleCommands();

        ConfigGamePieces();

        ConfigGameRules();

        ConfigPlayers();

        StartGame();
    }

    private void ConfigTileMap()
    {
        var tileMap = new TileMap(8, 8);

        ConsoleGameLoopManager.ConfigTileMap(tileMap, ConsoleColor.White, ConsoleColor.Black);
        // ConsoleGameLoopManager.AddTileObject()

        // TODO remove below, just for testing
        var renderer1 = new ConsoleTileRenderer();
        var consoleString1 = new ConsoleDrawableString("$", ConsoleColor.Yellow);
        renderer1.Init(consoleString1, new Vector2(2, 5));
        var movePatterns = new List<MovePattern>();
        movePatterns.Add(new MovePattern([Movement.Forward], true));
        tileMap[2, 5].PlaceTileObject(new TileObject(renderer1, tileMap[2, 5], movePatterns));


        ConsoleGameLoopManager.RefreshGameViewport(true);
    }

    private void ConfigGameConsoleCommands()
    {
        var commandsManager = ConsoleGameLoopManager.GetConsoleCommandsManager();
        var moves = new ConsoleCommand("moves",
            "Shows the possible moves of the selected unit (if possible). example: /moves",
            false,
            _ =>
            {
                Console.WriteLine($"Showing possible moves of selected unit");
                return true;
            });
        commandsManager.AddCommand(moves);

        var refresh = new ConsoleCommand("refresh",
            "Refresh the game viewport. example: /refresh",
            false,
            _ =>
            {
                ConsoleGameLoopManager.RefreshGameViewport();
                return true;
            });
        commandsManager.AddCommand(refresh);
    }

    private void ConfigGamePieces()
    {
        // Config the game rules:
        // Create the different chess pieces types
        // determine the rules for each piece type
    }

    private void ConfigGameRules()
    {
        // determine the game's win condition
        // determine the game's flow (turns and their logic)
    }

    private void ConfigPlayers()
    {
        // Config the players
        // create 2 players
        // determine a name and color to each player
        // add to each players their 16 pieces
    }

    private void StartGame()
    {
        ConsoleGameLoopManager.StartGameLoop();
    }
}