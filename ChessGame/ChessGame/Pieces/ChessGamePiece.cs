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

    public override void HandleOtherTileObjectInPossibleMoveCallback(TileObject tileObject)
    {
        base.HandleOtherTileObjectInPossibleMoveCallback(tileObject);
        if (tileObject is ChessGamePiece otherPiece)
        {
            // TODO handle according to actor once we have it
            HandleOtherChessPieceInPossibleMoveCallback(otherPiece);
        }
    }

    public override bool CheckPossibleMoveTileCallback(Tile tile)
    {
        return CheckIfTileIsPossibleMoveCallback(tile);
    }

    protected abstract void HandleOtherChessPieceInPossibleMoveCallback(ChessGamePiece otherPiece);
    protected abstract bool CheckIfTileIsPossibleMoveCallback(Tile tile);
}

public class King : ChessGamePiece
{
    private static ConsoleDrawableString _drawableString = new("K");

    private static readonly List<MovePattern> MovePatterns =
    [
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Back], false),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.BackLeft], false),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Left], false),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.ForwardLeft], false),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Forward], false),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.ForwardRight], false),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Right], false),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.BackRight], false),
    ];

    public King(ITileRenderer renderer, Tile tile, ConsoleColor color) : base(renderer, MovePatterns, tile)
    {
        _drawableString.FgConsoleColor = color;
        var position = tile.Position;
        Init(renderer, _drawableString, position);
    }

    protected override void HandleOtherChessPieceInPossibleMoveCallback(ChessGamePiece otherPiece)
    {
        GameManager.HighlightTile(otherPiece.CurrentTile);
    }

    protected override bool CheckIfTileIsPossibleMoveCallback(Tile tile)
    {
        return true;
    }
}

public class Queen : ChessGamePiece
{
    private static ConsoleDrawableString _drawableString = new("Q");

    private static readonly List<MovePattern> MovePatterns =
    [
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Back], true),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.BackLeft], true),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Left], true),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.ForwardLeft], true),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Forward], true),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.ForwardRight], true),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Right], true),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.BackRight], true),
    ];

    public Queen(ITileRenderer renderer, Tile tile, ConsoleColor color) : base(renderer, MovePatterns, tile)
    {
        _drawableString.FgConsoleColor = color;
        var position = tile.Position;
        Init(renderer, _drawableString, position);
    }

    protected override void HandleOtherChessPieceInPossibleMoveCallback(ChessGamePiece otherPiece)
    {
        GameManager.HighlightTile(otherPiece.CurrentTile);
    }

    protected override bool CheckIfTileIsPossibleMoveCallback(Tile tile)
    {
        return true;
    }
}

public class Pawn : ChessGamePiece
{
    private static ConsoleDrawableString _drawableString = new("P");

    private static readonly List<MovePattern> MovePatterns =
    [
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.Forward], false),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.ForwardLeft], false),
        new MovePattern([TileMapEngine.CoreEngine.TileObject.Movement.ForwardRight], false),
    ];

    public Pawn(ITileRenderer renderer, Tile tile, ConsoleColor color) : base(renderer, MovePatterns, tile)
    {
        _drawableString.FgConsoleColor = color;
        var position = tile.Position;
        Init(renderer, _drawableString, position);
    }

    protected override void HandleOtherChessPieceInPossibleMoveCallback(ChessGamePiece otherPiece)
    {
        var tile = otherPiece.CurrentTile;
        
        if (tile?.Position.X != Position.X) // Means the other tile isn't at Movement.Forward direction, so it's a diagonal eat
        {
            GameManager.HighlightTile(tile);
        }
    }

    protected override bool CheckIfTileIsPossibleMoveCallback(Tile tile)
    {
        if (tile.CurrentTileObject != null &&
            tile.Position.X != Position.X) // Means the other tile isn't at Movement.Forward direction, so it's a diagonal eat
        {
            return true;
        }

        if (tile.CurrentTileObject == null &&
            tile.Position.Y == Position.Y - 1 &&
            tile.Position.X == Position.X)
        {
            return true;
        }

        return false;
    }
}

public class Bishop : ChessGamePiece
{
    private static ConsoleDrawableString _drawableString = new("B");

    private static readonly List<MovePattern> MovePatterns =
    [
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

    protected override void HandleOtherChessPieceInPossibleMoveCallback(ChessGamePiece otherPiece)
    {
        GameManager.HighlightTile(otherPiece.CurrentTile);
    }

    protected override bool CheckIfTileIsPossibleMoveCallback(Tile tile)
    {
        return true;
    }
}

public class Rook : ChessGamePiece
{
    private static ConsoleDrawableString _drawableString = new("R");

    private static readonly List<MovePattern> MovePatterns =
    [
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

    protected override void HandleOtherChessPieceInPossibleMoveCallback(ChessGamePiece otherPiece)
    {
        GameManager.HighlightTile(otherPiece.CurrentTile);
    }

    protected override bool CheckIfTileIsPossibleMoveCallback(Tile tile)
    {
        return true;
    }
}

public class Knight : ChessGamePiece
{
    private static ConsoleDrawableString _drawableString = new("N");

    private static readonly List<MovePattern> MovePatterns =
    [
        new MovePattern(
        [
            TileMapEngine.CoreEngine.TileObject.Movement.Left, TileMapEngine.CoreEngine.TileObject.Movement.ForwardLeft
        ], false),
        new MovePattern(
            [TileMapEngine.CoreEngine.TileObject.Movement.Left, TileMapEngine.CoreEngine.TileObject.Movement.BackLeft],
            false),

        new MovePattern(
        [
            TileMapEngine.CoreEngine.TileObject.Movement.Forward,
            TileMapEngine.CoreEngine.TileObject.Movement.ForwardLeft
        ], false),
        new MovePattern(
        [
            TileMapEngine.CoreEngine.TileObject.Movement.Forward,
            TileMapEngine.CoreEngine.TileObject.Movement.ForwardRight
        ], false),

        new MovePattern(
        [
            TileMapEngine.CoreEngine.TileObject.Movement.Right,
            TileMapEngine.CoreEngine.TileObject.Movement.ForwardRight
        ], false),
        new MovePattern(
        [
            TileMapEngine.CoreEngine.TileObject.Movement.Right, TileMapEngine.CoreEngine.TileObject.Movement.BackRight
        ], false),

        new MovePattern(
            [TileMapEngine.CoreEngine.TileObject.Movement.Back, TileMapEngine.CoreEngine.TileObject.Movement.BackLeft],
            false),
        new MovePattern(
            [TileMapEngine.CoreEngine.TileObject.Movement.Back, TileMapEngine.CoreEngine.TileObject.Movement.BackRight],
            false),
    ];

    public Knight(ITileRenderer renderer, Tile tile, ConsoleColor color) : base(renderer, MovePatterns, tile)
    {
        _drawableString.FgConsoleColor = color;
        var position = tile.Position;
        Init(renderer, _drawableString, position);
    }

    protected override void HandleOtherChessPieceInPossibleMoveCallback(ChessGamePiece otherPiece)
    {
        GameManager.HighlightTile(otherPiece.CurrentTile);
    }

    protected override bool CheckIfTileIsPossibleMoveCallback(Tile tile)
    {
        return true;
    }
}