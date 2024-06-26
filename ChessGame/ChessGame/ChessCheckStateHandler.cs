using ChessGame.Pieces;
using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.Objects;

namespace ChessGame;

public static class ChessCheckStateHandler
{
    private static ChessPlayer _whitePlayer;
    private static ChessPlayer _blackPlayer;

    public static void Init(ChessPlayer whitePlayer, ChessPlayer blackPlayer)
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
            
            if (chessPiece.Position == newPosition)
            {
                if (IsInCheckWithoutPiece(player, chessPiece))
                {
                    return true;
                }
            
                continue;
            }

            foreach (var movePattern in chessPiece.Movement.MovePatterns)
            {
                if (!movePattern.IsDirection)
                {
                    continue;
                }

                var possibleMoves = ObjectMovement.GetMovesByDirection(movePattern, chessPiece.Position);

                foreach (var possibleMove in possibleMoves)
                {
                   
                    if (possibleMove == newPosition)
                    {
                        if (IsInCheckNotFromDirection(player, chessPiece, movePattern))
                        {
                            return true;
                        }

                        return false;
                    }
                    
                    if (player.PlayerKing.Position == possibleMove) return true;
                }
                
            }
        }

        return false;
    }
    
    private static bool IsInCheckWithoutPiece(ChessPlayer player, ChessGamePiece excludedPiece)
    {
        ChessPlayer opponent = player.PlayerColor == PlayerColor.White ? _blackPlayer : _whitePlayer;

        foreach (var chessPiece in opponent.TileObjects)
        {
            if (chessPiece == excludedPiece)
            {
                continue;
            }
            
            foreach (var possibleMove in (chessPiece as ChessGamePiece).Movement.GetPossibleMoves())
            {
                if (player.PlayerKing.Position == possibleMove) return true;
            }
        }

        return false;
    }

    private static bool IsInCheckNotFromDirection(ChessPlayer attackedPlayer, ChessGamePiece chessPiece,
        MovePattern direction)
    {
        foreach (var movePattern in chessPiece.Movement.MovePatterns)
        {
            if (movePattern.Movement == direction.Movement)
            {
                continue;
            }

            var possibleMoves = ObjectMovement.GetMovesByDirection(movePattern, chessPiece.Position);

            foreach (var possibleMove in possibleMoves)
            {
                if (possibleMove == attackedPlayer.PlayerKing.Position)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool GetIsInCheckMate(ChessPlayer playerToCheck)
    {
        if (!IsInCheck(playerToCheck))
        {
            return false;
        }

        var tileObjectsToCheck = playerToCheck.PlayerColor == PlayerColor.White
            ? _whitePlayer.TileObjects
            : _blackPlayer.TileObjects;

        foreach (var tileObject in tileObjectsToCheck)
        {
            if (tileObject is not ChessGamePiece chessGamePiece)
            {
                throw new Exception("This tile object is not a chess game piece");
            }

            foreach (var position in chessGamePiece.Movement.GetPossibleMoves())
            {
                if (TileMapManager.TileMap == null || !TileMapManager.TileMap.CheckTileExistsInPosition(position, out var tile))
                {
                    continue;
                }
                if (!chessGamePiece.CheckPossibleMoveTileCallback(tile))
                {
                    continue;
                }
                if (!IsInCheckAfterMove(playerToCheck, position))
                {
                    return false;
                }
            }
        }

        return true;
    }
}