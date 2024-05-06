using ChessGame.Pieces;
using ConsoleRenderer;
using TileMapEngine.CoreEngine;

namespace ChessGame;

public class ChessGame
{
    private GamePiecesManager _gamePiecesManager;
    private ChessPlayer _whitePlayer;
    private ChessPlayer _blackPlayer;

    public static readonly int BoardSize = 8;

    public void RunChessGame()
    {
        ConfigTileMap();

        ConfigGameConsoleCommands();

        ConfigPlayers();
        
        ConfigGameRules();

        StartGame();
    }

    private void ConfigTileMap()
    {
        var tileMap = new TileMap(BoardSize, BoardSize);

        var consoleGameLoop = new ConsoleGameLoopManager();
        GameManager.InitTileMap(tileMap, consoleGameLoop);
        consoleGameLoop.AssignCheckersPattern(tileMap, ConsoleColor.Cyan, ConsoleColor.DarkBlue);
    }

    private void ConfigGameConsoleCommands()
    {
        new ChessConsoleCommands().Init();
    }
    
    private void ConfigPlayers()
    {
        _gamePiecesManager = new GamePiecesManager();
        
        var whitePieces = _gamePiecesManager.CreateAndGetWhitePlayerPieces();
        _whitePlayer = new ChessPlayer(whitePieces, PlayerColor.White, "White Player");
        
        var blackPieces = _gamePiecesManager.CreateAndGetBlackPlayerPieces();
        _blackPlayer = new ChessPlayer(blackPieces, PlayerColor.Black, "Black Player");
    }

    private void ConfigGameRules()
    {
        // determine the game's win condition
        // determine the game's flow (turns and their logic)
    }

    private void StartGame()
    {
        var gameLoopManager = GameManager.GetGameLoopManager();
        gameLoopManager?.RefreshGameViewport(true);
        gameLoopManager?.StartGameLoop();
    }
}