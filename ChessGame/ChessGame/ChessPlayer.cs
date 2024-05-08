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

}

public enum PlayerColor
{
    White,
    Black
}