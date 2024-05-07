using ChessGame.Pieces;
using TileMapEngine.CoreEngine;

namespace ChessGame;

public class ChessPlayer(PlayerColor playerColor, string playerName) : Actor(playerName)
{
    public PlayerColor PlayerColor { get; } = playerColor;
    public King PlayerKing { get; private set; }

    public void Init(King king)
    {
        PlayerKing = king;
    }
    
    public bool GetIsInCheckMate()
    {
        // foreach (var tileObject in TileObjects)
        // {
        //     if (tileObject is not ChessGamePiece chessPiece)
        //     {
        //         throw new Exception("This tile object is not ChessPiece");
        //     }
        //
        //     foreach (var possibleMove in chessPiece.Movement.GetPossibleMoves())
        //     {
        //         if(!TileMapManager.TileMap.CheckTileExistsInPosition(possibleMove, out var tile)) continue;
        //         if (chessPiece.CheckPossibleMoveTileCallback(tile))
        //         {
        //             return false;
        //         }
        //         
        //     }
        //
        //     return true;
        // }

        
        
        return false;
    }

}

public enum PlayerColor
{
    White,
    Black
}