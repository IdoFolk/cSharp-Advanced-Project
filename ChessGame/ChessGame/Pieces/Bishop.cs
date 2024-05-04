using ConsoleRenderer.ConsoleRenderer;
using Renderer.Rendering;
using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace ChessGame.Pieces;

public class Bishop : ChessGamePiece
{
    private static ConsoleDrawableString _drawableString = new("B");
    private static readonly List<MovePattern> MovePatterns = [
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.BackLeft], true),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.ForwardLeft], true),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.BackRight], true),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.ForwardRight], true),
    ];
    
    public Bishop(ITileRenderer renderer, Tile tile, ConsoleColor color) : base(renderer, MovePatterns, tile)
    {
        _drawableString.FgConsoleColor = color;
        var position = tile.Position;
        Init(renderer, _drawableString, position);
    }
}