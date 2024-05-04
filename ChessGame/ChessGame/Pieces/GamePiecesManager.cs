using System.Numerics;
using ConsoleRenderer.ConsoleRenderer;
using TileMapEngine;
using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace ChessGame.Pieces;

public class GamePiecesManager
{
    public void Init()
    {
        var drawableString = new ConsoleDrawableString("$", ConsoleColor.Yellow);
        var consoleTileRenderer = new ConsoleTileRenderer();
        var movePatterns = new List<MovePattern>();
        movePatterns.Add(new MovePattern([Movement.Forward], true));

        var pieceConfig = new TileObjectConfig(consoleTileRenderer, drawableString, new Vector2(2, 5), movePatterns);
        var newChessPiece = new ChessGamePiece(pieceConfig,
            GameManager.TileMap?[(int)pieceConfig.Position.X, (int)pieceConfig.Position.Y]);
        
        newChessPiece.Init(pieceConfig);

        GameManager.RefreshGameViewport(true);
    }
}