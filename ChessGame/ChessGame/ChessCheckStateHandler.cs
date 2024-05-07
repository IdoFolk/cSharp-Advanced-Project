using ChessGame.Pieces;
using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.Objects;

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
        var opponent = player.PlayerColor == PlayerColor.White ? _blackPlayer : _whitePlayer;

        foreach (var tileObject in opponent.TileObjects)
        {
            if (tileObject is not ChessGamePiece chessPiece)
            {
                throw new Exception($"tile object {tileObject} is not a chess game piece.");
            }
            
            foreach (var possibleMove in chessPiece.Movement.GetPossibleMoves())
            {
                if (possibleMove == newPosition)
                {
                    return IsInCheckNotFrom(player, chessPiece);
                }
                if (player.PlayerKing.Position == possibleMove) return true;
            }
        }

        return false;
    }

    private static bool IsInCheckNotFrom(ChessPlayer player, ChessGamePiece threateningPiece)
    {
        ChessPlayer opponent = player.PlayerColor == PlayerColor.White ? _blackPlayer : _whitePlayer;

        foreach (var chessPiece in opponent.TileObjects)
        {
            if (chessPiece == threateningPiece) continue;
            
            foreach (var possibleMove in (chessPiece as ChessGamePiece).Movement.GetPossibleMoves())
            {
                if (player.PlayerKing.Position == possibleMove) return true;
            }
        }

        return false;
    }
}