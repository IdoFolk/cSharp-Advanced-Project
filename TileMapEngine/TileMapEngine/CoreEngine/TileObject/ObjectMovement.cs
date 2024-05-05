namespace TileMapEngine.CoreEngine.TileObject;

public class ObjectMovement(TileObject owner, List<MovePattern> movePatterns)
{
    public MovePattern[] MovePatterns => movePatterns.ToArray();

    public ObjectMovement(TileObject owner) : this(owner, [])
    {
    }

    public IEnumerable<Position2D> GetPossibleMoves() //return a tile object if encountered
    {
        List<Position2D> possibleMoves = new();
        foreach (var movePattern in movePatterns)
        {
            if (movePattern.IsDirection)
                possibleMoves.AddRange(GetMovesByDirection(movePattern));
            else
                possibleMoves.Add(GetMovesByMovePattern(movePattern));
        }

        return possibleMoves.ToArray();
    }

    private List<Position2D> GetMovesByDirection(MovePattern movePattern)
    {
        var newPosition = owner.Position;
        List<Position2D> availablePositions = [];
        var tileMap = GameManager.TileMap;
        foreach (var movement in movePattern.Movement)
        {
            do
            {
                newPosition = MovePositionByMovement(movement, newPosition);
                availablePositions.Add(newPosition);

                if (tileMap != null && !tileMap.CheckTileExistsInPosition(newPosition)) return availablePositions;
            } while (tileMap?[newPosition]?.CurrentTileObject == null);
        }

        return availablePositions;
    }

    private Position2D GetMovesByMovePattern(MovePattern movePattern)
    {
        return movePattern.Movement.Aggregate(owner.Position,
            (current, movement) => MovePositionByMovement(movement, current));
    }

    private static Position2D MovePositionByMovement(MovementType movementType, Position2D newPosition)
    {
        newPosition += movementType switch
        {
            MovementType.Left => new Position2D(-1, 0),
            MovementType.Right => new Position2D(1, 0),
            MovementType.Forward => new Position2D(0, -1),
            MovementType.Back => new Position2D(0, 1),
            MovementType.ForwardRight => new Position2D(1, -1),
            MovementType.ForwardLeft => new Position2D(-1, -1),
            MovementType.BackRight => new Position2D(1, 1),
            MovementType.BackLeft => new Position2D(-1, 1),
            _ => throw new ArgumentOutOfRangeException(nameof(movementType), movementType, null)
        };

        return newPosition;
    }

    public void AddMovePattern(MovePattern movePattern)
    {
        movePatterns.Add(movePattern);
    }

    public void RemoveMovePattern(MovePattern movePattern)
    {
        movePatterns.Remove(movePattern);
    }
}

public readonly struct MovePattern(MovementType[] movement, bool isDirection)
{
    public MovementType[] Movement { get; } = movement;
    public bool IsDirection { get; } = isDirection;
}

public enum MovementType
{
    //all possible direction a piece can go in
    Left,
    Right,
    Forward,
    Back,
    ForwardRight,
    ForwardLeft,
    BackRight,
    BackLeft
}