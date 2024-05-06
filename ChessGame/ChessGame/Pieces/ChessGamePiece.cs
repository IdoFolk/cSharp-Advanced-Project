using System.Numerics;
using ConsoleRenderer.ConsoleRenderer;
using Renderer.Rendering;
using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.Objects;

namespace ChessGame.Pieces;

public abstract class ChessGamePiece(ITileRenderer renderer, List<MovePattern> movePatterns, Tile? tile, Actor owner)
    : TileObject(renderer, tile,
        movePatterns, owner)
{

    protected Position2D StartingPosition2D;
    protected void Init(ITileRenderer renderer, IDrawable drawable, Position2D position2D)
    {
        StartingPosition2D = position2D;
        
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
        new MovePattern([MovementType.Back], false),
        new MovePattern([MovementType.BackLeft], false),
        new MovePattern([MovementType.Left], false),
        new MovePattern([MovementType.ForwardLeft], false),
        new MovePattern([MovementType.Forward], false),
        new MovePattern([MovementType.ForwardRight], false),
        new MovePattern([MovementType.Right], false),
        new MovePattern([MovementType.BackRight], false),
    ];

    public King(ITileRenderer renderer, Tile tile, ConsoleColor color, Actor owner) : base(renderer, MovePatterns, tile, owner)
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
        new MovePattern([MovementType.Back], true),
        new MovePattern([MovementType.BackLeft], true),
        new MovePattern([MovementType.Left], true),
        new MovePattern([MovementType.ForwardLeft], true),
        new MovePattern([MovementType.Forward], true),
        new MovePattern([MovementType.ForwardRight], true),
        new MovePattern([MovementType.Right], true),
        new MovePattern([MovementType.BackRight], true),
    ];

    public Queen(ITileRenderer renderer, Tile tile, ConsoleColor color, Actor owner) : base(renderer, MovePatterns, tile, owner)
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

public class WhitePawn : ChessGamePiece
{
    private static ConsoleDrawableString _drawableString = new("P");

    private static readonly List<MovePattern> MovePatterns =
    [
        new MovePattern([MovementType.Forward], false),
        new MovePattern([MovementType.Forward, MovementType.Forward], false),
        new MovePattern([MovementType.ForwardLeft], false),
        new MovePattern([MovementType.ForwardRight], false),
    ];

    public WhitePawn(ITileRenderer renderer, Tile tile, ConsoleColor color, Actor owner) : base(renderer, MovePatterns, tile, owner)
    {
        _drawableString.FgConsoleColor = color;
        var position = tile.Position;
        Init(renderer, _drawableString, position);
    }

    protected override void HandleOtherChessPieceInPossibleMoveCallback(ChessGamePiece otherPiece)
    {
        var tile = otherPiece.CurrentTile;

        if (tile?.Position.X != Position.X && tile?.Position.Y == Position.Y - 1) // Means the other tile isn't at Movement.Forward direction, so it's a diagonal eat
        {
            GameManager.HighlightTile(tile);
        }
    }

    protected override bool CheckIfTileIsPossibleMoveCallback(Tile tile)
    {
        if (tile.CurrentTileObject == null &&
            tile.Position.Y == Position.Y - 2 &&
            Position == StartingPosition2D)
        {
            return true;
        }
        
        if (tile.CurrentTileObject != null &&
            tile.Position.X != Position.X) // Means the other tile isn't at Movement.Forward direction, so it's a diagonal eat
        {
            return true;
        }

        return tile.CurrentTileObject == null &&
               tile.Position.Y == Position.Y - 1 &&
               tile.Position.X == Position.X;
    }
}

public class BlackPawn : ChessGamePiece
{
    private static ConsoleDrawableString _drawableString = new("P");

    private static readonly List<MovePattern> MovePatterns =
    [
        new MovePattern([MovementType.Back], false),
        new MovePattern([MovementType.Back, MovementType.Back], false),
        new MovePattern([MovementType.BackLeft], false),
        new MovePattern([MovementType.BackRight], false),
    ];

    public BlackPawn(ITileRenderer renderer, Tile tile, ConsoleColor color, Actor owner) : base(renderer, MovePatterns, tile, owner)
    {
        _drawableString.FgConsoleColor = color;
        var position = tile.Position;
        Init(renderer, _drawableString, position);
    }

    protected override void HandleOtherChessPieceInPossibleMoveCallback(ChessGamePiece otherPiece)
    {
        var tile = otherPiece.CurrentTile;

        if (tile?.Position.X != Position.X && tile?.Position.Y == Position.Y + 1) // Means the other tile isn't at Movement.Forward direction, so it's a diagonal eat
        {
            GameManager.HighlightTile(tile);
        }
    }

    protected override bool CheckIfTileIsPossibleMoveCallback(Tile tile)
    {
        if (tile.CurrentTileObject == null &&
            tile.Position.Y == Position.Y + 2 &&
            Position == StartingPosition2D)
        {
            return true;
        }
        
        if (tile.CurrentTileObject != null &&
            tile.Position.X != Position.X) // Means the other tile isn't at Movement.Forward direction, so it's a diagonal eat
        {
            return true;
        }

        return tile.CurrentTileObject == null &&
               tile.Position.Y == Position.Y + 1 &&
               tile.Position.X == Position.X;
    }
}

public class Bishop : ChessGamePiece
{
    private static ConsoleDrawableString _drawableString = new("B");

    private static readonly List<MovePattern> MovePatterns =
    [
        new MovePattern([MovementType.BackLeft], true),
        new MovePattern([MovementType.ForwardLeft], true),
        new MovePattern([MovementType.BackRight], true),
        new MovePattern([MovementType.ForwardRight], true),
    ];

    public Bishop(ITileRenderer renderer, Tile tile, ConsoleColor color, Actor owner) : base(renderer, MovePatterns, tile, owner)
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
        new MovePattern([MovementType.Forward], true),
        new MovePattern([MovementType.Back], true),
        new MovePattern([MovementType.Right], true),
        new MovePattern([MovementType.Left], true),
    ];

    public Rook(ITileRenderer renderer, Tile tile, ConsoleColor color, Actor owner) : base(renderer, MovePatterns, tile, owner)
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
            MovementType.Left, MovementType.ForwardLeft
        ], false),
        new MovePattern(
            [MovementType.Left, MovementType.BackLeft],
            false),

        new MovePattern(
        [
            MovementType.Forward,
            MovementType.ForwardLeft
        ], false),
        new MovePattern(
        [
            MovementType.Forward,
            MovementType.ForwardRight
        ], false),

        new MovePattern(
        [
            MovementType.Right,
            MovementType.ForwardRight
        ], false),
        new MovePattern(
        [
            MovementType.Right, MovementType.BackRight
        ], false),

        new MovePattern(
            [MovementType.Back, MovementType.BackLeft],
            false),
        new MovePattern(
            [MovementType.Back, MovementType.BackRight],
            false),
    ];

    public Knight(ITileRenderer renderer, Tile tile, ConsoleColor color, Actor owner) : base(renderer, MovePatterns, tile, owner)
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