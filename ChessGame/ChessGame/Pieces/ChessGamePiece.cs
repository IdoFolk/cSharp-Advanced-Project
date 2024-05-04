using System.Numerics;
using ConsoleRenderer.ConsoleRenderer;
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

public class Pawn : ChessGamePiece
{
    private static ConsoleDrawableString _drawableString = new("P");
    private static readonly List<MovePattern> MovePatterns = [
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Forward], false),
    ];
    
    public Pawn(ITileRenderer renderer, Tile tile, ConsoleColor color) : base(renderer, MovePatterns, tile)
    {
        _drawableString.FgConsoleColor = color;
        var position = tile.Position;
        Init(renderer, _drawableString, position);
    }
}

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

public class Rook : ChessGamePiece
{
    private static ConsoleDrawableString _drawableString = new("R");
    private static readonly List<MovePattern> MovePatterns = [
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Forward], true),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Back], true),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Right], true),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Left], true),
    ];
    
    public Rook(ITileRenderer renderer, Tile tile, ConsoleColor color) : base(renderer, MovePatterns, tile)
    {
        _drawableString.FgConsoleColor = color;
        var position = tile.Position;
        Init(renderer, _drawableString, position);
    }
}

public class Knight : ChessGamePiece
{
    private static ConsoleDrawableString _drawableString = new("N");
    private static readonly List<MovePattern> MovePatterns = [
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Left, TileMapEngine.CoreEngine.TileObject.Movement.ForwardLeft], false),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Left, TileMapEngine.CoreEngine.TileObject.Movement.BackLeft], false),
        
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Forward, TileMapEngine.CoreEngine.TileObject.Movement.ForwardLeft], false),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Forward, TileMapEngine.CoreEngine.TileObject.Movement.ForwardRight], false),
        
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Right, TileMapEngine.CoreEngine.TileObject.Movement.ForwardRight], false),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Right, TileMapEngine.CoreEngine.TileObject.Movement.BackRight], false),
        
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Back, TileMapEngine.CoreEngine.TileObject.Movement.BackLeft], false),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Back, TileMapEngine.CoreEngine.TileObject.Movement.BackRight], false),
    ];
    
    public Knight(ITileRenderer renderer, Tile tile, ConsoleColor color) : base(renderer, MovePatterns, tile)
    {
        _drawableString.FgConsoleColor = color;
        var position = tile.Position;
        Init(renderer, _drawableString, position);
    }
}