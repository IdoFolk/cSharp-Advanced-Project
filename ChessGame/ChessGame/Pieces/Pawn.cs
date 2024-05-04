using System.Numerics;
using ConsoleRenderer.ConsoleRenderer;
using Renderer.Rendering;
using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace ChessGame.Pieces;

public class Pawn : ChessGamePiece
{
    public Pawn(ITileRenderer tileRenderer, Tile? currentTile) : base(tileRenderer, currentTile)
    {
    }

    public Pawn(ITileRenderer tileRenderer, Tile? currentTile, List<MovePattern> movePatterns) : base(tileRenderer, currentTile, movePatterns)
    {
    }

    public Pawn(TileObjectConfig tileObjectConfig, Tile? tile) : base(tileObjectConfig, tile)
    {
    }

    public Pawn(Tile tile, ConsoleColor color) : base(new TileObjectConfig(), tile)
    {
        
    }
}