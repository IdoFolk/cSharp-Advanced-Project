using TileMapEngine.CoreEngine;

namespace ChessGame;

public class ChessPlayer(PlayerColor playerColor, string playerName) : Actor(playerName)
{
    public PlayerColor PlayerColor { get; } = playerColor;
}

public enum PlayerColor
{
    White,
    Black
}