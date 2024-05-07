using ChessGame.Pieces;
using TileMapEngine.CoreEngine;

namespace ChessGame;

public class ChessPlayer(PlayerColor playerColor, string playerName) : Actor(playerName)
{
    public PlayerColor PlayerColor { get; } = playerColor;

    public bool GetIsInCheck()
    {
        if (TileObjects.Find(tileObject => tileObject.GetType() == typeof(King)) is not King myKing)
        {
            throw new Exception($"No King piece found for {ActorName}.");
        }

        // TODO create a ChessCheckStateHandler static class that holds both players and returns if in check state
        
        return true;
    }
}

public enum PlayerColor
{
    White,
    Black
}