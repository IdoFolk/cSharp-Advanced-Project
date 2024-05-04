using System.Numerics;
using Renderer.Rendering;
using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace ChessGame.Pieces;

public abstract class ChessGamePiece(ITileRenderer renderer, List<MovePattern> movePatterns, Tile? tile)
    : TileObject(renderer, tile,
        movePatterns)
{
    protected void Init(ITileRenderer renderer, IDrawable drawable, Position2D position2D)
    {
        renderer.Init(drawable, new Vector2(position2D.X, position2D.Y));
        
        GameManager.AddObjectToTileMap(this, position2D);
    }
}