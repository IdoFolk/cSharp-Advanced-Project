using ConsoleRenderer.ConsoleRenderer;
using Renderer.Rendering;
using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace ChessGame.Pieces;

public class Pawn : ChessGamePiece
{
    private static ConsoleDrawableString _drawableString = new("$", ConsoleColor.White);
    private static readonly List<MovePattern> MovePatterns = [
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Forward], true),
    ];
    
    public Pawn(ITileRenderer renderer, Tile tile, ConsoleColor color) : base(renderer, MovePatterns, tile)
    {
        _drawableString.FgConsoleColor = color;
        var position = tile.Position;
        Init(renderer, _drawableString, position);
    }
}