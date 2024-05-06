using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace ChessGame;

public class ChessPlayer(List<TileObject> tileObjects, PlayerColor playerColor, string playerName) : Actor(tileObjects, playerName)
{
    public PlayerColor PlayerColor { get; } = playerColor;
}

public enum PlayerColor
{
    White,
    Black
}