using ChessGame.Pieces;
using TileMapEngine.CoreEngine;

namespace ChessGame;

public static class ChessCheckStateHandler
{
    private static ChessPlayer _whitePlayer;
    private static ChessPlayer _blackPlayer;
    
    public static void Init(ChessPlayer whitePlayer,ChessPlayer blackPlayer)
    {
        _whitePlayer = whitePlayer;
        _blackPlayer = blackPlayer;
    }
    
    public static bool IsInCheck(ChessPlayer player)
    {
        ChessPlayer opponent = player.PlayerColor == PlayerColor.White ? _blackPlayer : _whitePlayer;

        foreach (var chessPiece in opponent.TileObjects)
        {
            foreach (var possibleMove in (chessPiece as ChessGamePiece).Movement.GetPossibleMoves())
            {
                if (player.PlayerKing.Position == possibleMove) return true;
            }
        }

        return false;
    }
    public static bool IsInCheckAtSpecificKingPosition(ChessPlayer player, Position2D kingPosition)
    {
        ChessPlayer opponent = player.PlayerColor == PlayerColor.White ? _blackPlayer : _whitePlayer;

        foreach (var chessPiece in opponent.TileObjects)
        {
            foreach (var possibleMove in (chessPiece as ChessGamePiece).Movement.GetPossibleMoves())
            {
                if (kingPosition == possibleMove) return true;
            }
        }

        return false;
    }
    public static bool IsInCheckAfterMove(ChessPlayer player, Position2D newPosition)
    {
        ChessPlayer opponent = player.PlayerColor == PlayerColor.White ? _blackPlayer : _whitePlayer;

        foreach (var chessPiece in opponent.TileObjects)
        {
            foreach (var possibleMove in (chessPiece as ChessGamePiece).Movement.GetPossibleMoves())
            {
                if (possibleMove == newPosition) return false;
                if (player.PlayerKing.Position == possibleMove) return true;
            }
        }

        return false;
    }
    
    
}