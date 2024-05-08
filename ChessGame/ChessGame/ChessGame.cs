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

        ConsoleGameLoopManager.OnTurnEnded += HandleOnTurnEnded;

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
        var whiteKing = whitePieces.Find((piece) => piece.GetType() == typeof(King));
        _whitePlayer.Init(whiteKing as King);
        
        _blackPlayer = new ChessPlayer(PlayerColor.Black, "Black Player");
        var blackPieces = GamePiecesConfig.CreateAndGetBlackPlayerPieces(_blackPlayer);
        _blackPlayer.AddTileObjects(blackPieces);
        var blackKing = blackPieces.Find((piece) => piece.GetType() == typeof(King));
        _blackPlayer.Init(blackKing as King);
        
        ChessCheckStateHandler.Init(_whitePlayer,_blackPlayer);

        ChessGamePiece.OnPieceEaten += (piece,otherPiece) =>
        {
            Console.WriteLine($"{piece.OwnerActor.ActorName}'s {piece.Name} was eaten by {otherPiece.Name}");
            piece.OwnerActor.RemoveObject(piece);
        };
    }

    private void StartGameLoop()
    {
        _gameLoopManager?.RefreshGameViewport(true);
        
        _gameLoopManager?.StartTwoPlayersGameLoop(_whitePlayer, _blackPlayer);
        // A while loop starts above, nothing below will run in runtime!
    }

    private void HandleOnTurnEnded(Actor actor)
    {
        if (actor is not ChessPlayer player)
        {
            throw new Exception($"Actor {actor} is not of type ChessPlayer.");
        }

        if (ChessCheckStateHandler.GetIsInCheckMate(player))
        {
            EndGame(player);
        }

    }

    private void EndGame(ChessPlayer player)
    {
        var text = player == _whitePlayer
            ? $"Black player wins by Checkmate! Game Over."
            : "White player wins by Checkmate! Game Over.";
        
        Console.WriteLine(text);
        
        ConsoleGameLoopManager.OnTurnEnded -= HandleOnTurnEnded;
        _gameLoopManager?.StopGameLoop();
    }
}