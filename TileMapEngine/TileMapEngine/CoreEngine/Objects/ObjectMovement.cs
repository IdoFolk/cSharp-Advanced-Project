namespace TileMapEngine.CoreEngine.Objects;

public class ObjectMovement(TileObject owner, List<MovePattern> movePatterns)
{
    public MovePattern[] MovePatterns => movePatterns.ToArray();

    public ObjectMovement(TileObject owner) : this(owner, [])
    {
    }

    /// <summary>
    /// Gets the possible moves for the TileObject.
    /// </summary>
    /// <returns>
    /// An enumerable collection of Position2D representing the possible moves.
    /// </returns>
    public IEnumerable<Position2D> GetPossibleMoves() 
    {
        List<Position2D> possibleMoves = [];
        foreach (var movePattern in movePatterns)
        {
            if (movePattern.IsDirection)
                possibleMoves.AddRange(GetMovesByDirection(movePattern, owner.Position));
            else
                possibleMoves.Add(GetMovesByMovePattern(movePattern, owner.Position));
        }

        return possibleMoves.ToArray();
    }

    /// <summary>
    /// Gets the available moves by a specified direction and position.
    /// </summary>
    /// <param name="movePattern">The move pattern representing the direction.</param>
    /// <param name="position">The starting position.</param>
    /// <returns>A list of available positions based on the specified direction and starting position.</returns>
    public static List<Position2D> GetMovesByDirection(MovePattern movePattern, Position2D position)
    {
        var newPosition = position;
        List<Position2D> availablePositions = [];
        var tileMap = TileMapManager.TileMap;
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

    /// <summary>
    /// Gets the moves for the TileObject based on the given MovePattern and position.
    /// </summary>
    /// <param name="movePattern">The MovePattern to be used.</param>
    /// <param name="position">The position of the TileObject.</param>
    /// <returns>
    /// A Position2D representing the moves based on the MovePattern and position.
    /// </returns>
    public static Position2D GetMovesByMovePattern(MovePattern movePattern, Position2D position)
    {
        return movePattern.Movement.Aggregate(position,
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