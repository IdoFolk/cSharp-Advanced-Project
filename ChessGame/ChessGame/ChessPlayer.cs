using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace ChessGame;

public class ChessPlayer(List<TileObject> tileObjects, PlayerColor playerColor, string playerName) : Actor(tileObjects)
{
    public string PlayerName { get; set; } = playerName;
    public PlayerColor PlayerColor { get; } = playerColor;
}

public enum PlayerColor
{
    White,
    Black
}