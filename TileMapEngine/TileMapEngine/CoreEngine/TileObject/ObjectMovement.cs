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

    private static Position2D MovePositionByMovement(Movement movement, Position2D newPosition)
    {
        newPosition += movement switch
        {
            Movement.Left => new Position2D(-1, 0),
            Movement.Right => new Position2D(1, 0),
            Movement.Forward => new Position2D(0, -1),
            Movement.Back => new Position2D(0, 1),
            Movement.ForwardRight => new Position2D(1, -1),
            Movement.ForwardLeft => new Position2D(-1, -1),
            Movement.BackRight => new Position2D(1, 1),
            Movement.BackLeft => new Position2D(-1, 1),
            _ => throw new ArgumentOutOfRangeException(nameof(movement), movement, null)
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

public readonly struct MovePattern(Movement[] movement, bool isDirection)
{
    public Movement[] Movement { get; } = movement;
    public bool IsDirection { get; } = isDirection;
}

public enum Movement
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