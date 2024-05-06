using ChessGame.Pieces;
using TileMapEngine.CoreEngine;

namespace ChessGame;

public class ChessPlayer(PlayerColor playerColor, string playerName) : Actor(playerName)
{
    public PlayerColor PlayerColor { get; } = playerColor;

    public bool GetIsInCheck()
    {
        var myKing = TileObjects.Find(tileObject => tileObject.GetType() == typeof(King)) as King;
        if (myKing == null)
        {
            throw new Exception($"No King piece found for {ActorName}.");
        }


        return true;
    }
}

public enum PlayerColor
{
    White,
    Black
}