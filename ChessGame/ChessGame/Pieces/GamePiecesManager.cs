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
        var consoleString1 = new ConsoleDrawableString("$", ConsoleColor.Yellow);
        var renderer1 = new ConsoleTileRenderer();
        var movePatterns = new List<MovePattern>();
        movePatterns.Add(new MovePattern([Movement.Forward], true));
        
        var pieceConfig = new TileObjectConfig(renderer1, consoleString1, new Vector2(2, 5), movePatterns);
        
        GameManager.AddTileObject(pieceConfig);


        GameManager.RefreshGameViewport(true);
    }
}