using System.Numerics;
using Renderer.Rendering;
using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace ChessGame.Pieces;

public abstract class ChessGamePiece : TileObject
{
    protected ChessGamePiece(ITileRenderer renderer, List<MovePattern> movePatterns, Tile? tile) : base(renderer, tile,
        movePatterns)
    {
    }

    protected ChessGamePiece(ITileRenderer tileRenderer, Tile? currentTile) : base(tileRenderer, currentTile)
    {
    }

    protected ChessGamePiece(ITileRenderer tileRenderer, Tile? currentTile, List<MovePattern> movePatterns) : base(
        tileRenderer, currentTile, movePatterns)
    {
    }
    
    public void Init(ITileRenderer renderer, IDrawable drawable, Position2D position2D)
    {
        renderer.Init(drawable, new Vector2(position2D.X, position2D.Y));
        
        GameManager.AddObjectToTileMap(this, position2D);
    }
}