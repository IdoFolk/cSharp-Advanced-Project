using ChessGame.Pieces;
using ConsoleRenderer;
using TileMapEngine.CoreEngine;

namespace ChessGame;

public class ChessGame
{
    private ConsoleGameLoopManager _gameLoopManager;
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

        _gameLoopManager = new ConsoleGameLoopManager();
        TileMapManager.InitTileMap(tileMap, _gameLoopManager);
        _gameLoopManager.AssignCheckersPattern(tileMap, ConsoleColor.Cyan, ConsoleColor.DarkBlue);
    }

    private void ConfigGameConsoleCommands()
    {
        new ChessConsoleCommands().Init(_gameLoopManager);
    }
    
    private void ConfigPlayers()
    {
        _gamePiecesManager = new GamePiecesManager();
        
        _whitePlayer = new ChessPlayer(PlayerColor.White, "White Player");
        var whitePieces = _gamePiecesManager.CreateAndGetWhitePlayerPieces(_whitePlayer);
        _whitePlayer.AddTileObjects(whitePieces);
        
        _blackPlayer = new ChessPlayer(PlayerColor.Black, "Black Player");
        var blackPieces = _gamePiecesManager.CreateAndGetBlackPlayerPieces(_blackPlayer);
        _blackPlayer.AddTileObjects(blackPieces);
        
        ChessGamePiece.OnPieceEaten += piece => piece.OwnerActor.RemoveObject(piece);
    }

    private void ConfigGameRules()
    {
        // determine the game's win condition
        // determine the game's flow (turns and their logic)
    }

    private void StartGame()
    {
        _gameLoopManager.RefreshGameViewport(true);
        _gameLoopManager.StartTwoPlayersGameLoop(_whitePlayer, _blackPlayer);
    }
}