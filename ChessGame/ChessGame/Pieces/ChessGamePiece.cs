using Renderer.Rendering;
using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace ChessGame.Pieces;

public class ChessGamePiece : TileObject
{
    public ChessGamePiece(TileObjectConfig tileObjectConfig, Tile? tile) : base(tileObjectConfig.TileRenderer, tile,
        tileObjectConfig.MovePatterns)
    {
    }

    protected ChessGamePiece(ITileRenderer tileRenderer, Tile? currentTile) : base(tileRenderer, currentTile)
    {
    }

    protected ChessGamePiece(ITileRenderer tileRenderer, Tile? currentTile, List<MovePattern> movePatterns) : base(
        tileRenderer, currentTile, movePatterns)
    {
    }

    public void Init(TileObjectConfig tileObjectConfig)
    {
        tileObjectConfig.TileRenderer.Init(tileObjectConfig.Drawable, tileObjectConfig.Position);
        
        var position = new Position2D((int)tileObjectConfig.Position.X, (int)tileObjectConfig.Position.Y);
        
        GameManager.AddObjectToTileMap(this, position);
    }
}