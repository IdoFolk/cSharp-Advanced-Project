using Renderer.Rendering;
using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace ChessGame.Pieces;

public class ChessGamePiece : TileObject
{
    public ChessGamePiece(ITileRenderer tileRenderer, Tile? currentTile) : base(tileRenderer, currentTile)
    {
    }

    public ChessGamePiece(ITileRenderer tileRenderer, Tile? currentTile, List<MovePattern> movePatterns) : base(
        tileRenderer, currentTile, movePatterns)
    {
    }
    
    
}