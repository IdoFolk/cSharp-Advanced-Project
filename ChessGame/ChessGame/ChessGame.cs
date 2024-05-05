using ChessGame.Pieces;
using ConsoleRenderer;
using ConsoleRenderer.ConsoleCommands;
using TileMapEngine.CoreEngine;

namespace ChessGame;

public class ChessGame
{
    private GamePiecesManager _gamePiecesManager;
    public void RunChessGame()
    {
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

        var consoleGameLoop = new ConsoleGameLoopManager();
        GameManager.InitTileMap(tileMap, consoleGameLoop);
        consoleGameLoop.AssignCheckersPattern(tileMap, ConsoleColor.White, ConsoleColor.Black);
    }

    private void ConfigGameConsoleCommands()
    {
        if (GameManager.GetGameLoopManager() is not ConsoleGameLoopManager consoleGameLoop)
        {
            return;
        }
        
        var commandsManager = consoleGameLoop.GetConsoleCommandsManager();
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
            "Clears the console and re-draws the game updated game state onto the viewport. example: /refresh",
            false,
            _ =>
            {
                GameManager.RefreshGameViewport(true);
                return true;
            });
        commandsManager.AddCommand(refresh);
    }

    private void ConfigGamePieces()
    {
        _gamePiecesManager = new GamePiecesManager();
        _gamePiecesManager.Init();
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
        GameManager.GetGameLoopManager()?.StartGameLoop();
    }
}