using ChessGame.Pieces;
using ConsoleRenderer;
using TileMapEngine.CoreEngine;

namespace ChessGame;

public class ChessGame
{
    private GamePiecesManager _gamePiecesManager;

    public static readonly int BoardSize = 8;

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
        var tileMap = new TileMap(BoardSize, BoardSize);

        var consoleGameLoop = new ConsoleGameLoopManager();
        GameManager.InitTileMap(tileMap, consoleGameLoop);
        consoleGameLoop.AssignCheckersPattern(tileMap, ConsoleColor.White, ConsoleColor.Black);
    }

    private void ConfigGameConsoleCommands()
    {
        new ChessConsoleCommands().Init();
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