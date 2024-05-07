using ChessGame.Pieces;
using ConsoleRenderer;
using TileMapEngine.CoreEngine;

namespace ChessGame;

public class ChessGame
{
    private ConsoleGameLoopManager? _gameLoopManager;
    private ChessPlayer? _whitePlayer;
    private ChessPlayer? _blackPlayer;

    public static readonly int BoardSize = 8;

    public void RunChessGame()
    {
        ConfigTileMap();

        new ChessConsoleCommands().Init(_gameLoopManager);

        ConfigPlayers();

        ConsoleGameLoopManager.OnTurnStarted += HandleOnTurnStarted;

        StartGameLoop();
    }

    private void ConfigTileMap()
    {
        var tileMap = new TileMap(BoardSize, BoardSize);

        _gameLoopManager = new ConsoleGameLoopManager();
        TileMapManager.InitTileMap(tileMap, _gameLoopManager);
        _gameLoopManager.AssignCheckersPattern(tileMap, ConsoleColor.Cyan, ConsoleColor.DarkBlue);
    }

    private void ConfigPlayers()
    {
        _whitePlayer = new ChessPlayer(PlayerColor.White, "White Player");
        var whitePieces = GamePiecesConfig.CreateAndGetWhitePlayerPieces(_whitePlayer);
        _whitePlayer.AddTileObjects(whitePieces);

        _blackPlayer = new ChessPlayer(PlayerColor.Black, "Black Player");
        var blackPieces = GamePiecesConfig.CreateAndGetBlackPlayerPieces(_blackPlayer);
        _blackPlayer.AddTileObjects(blackPieces);

        ChessGamePiece.OnPieceEaten += piece => piece.OwnerActor.RemoveObject(piece);
    }

    private void StartGameLoop()
    {
        _gameLoopManager?.RefreshGameViewport(true);
        
        _gameLoopManager?.StartTwoPlayersGameLoop(_whitePlayer, _blackPlayer);
        // A while loop starts above, nothing below will run in runtime!
    }

    private void HandleOnTurnStarted(Actor actor)
    {
        if (actor is not ChessPlayer player)
        {
            throw new Exception($"Actor {actor} is not of type ChessPlayer.");
        }

        if (!player.GetIsInCheck())
        {
            return;
        }

        var text = player == _whitePlayer
            ? $"Black player wins by Checkmate! Game Over."
            : "White player wins by Checkmate! Game Over.";
        
        Console.WriteLine(text);
        
        ConsoleGameLoopManager.OnTurnStarted -= HandleOnTurnStarted;
        _gameLoopManager?.StopGameLoop();
    }
}