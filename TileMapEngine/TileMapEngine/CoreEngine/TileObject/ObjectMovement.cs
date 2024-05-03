namespace TileMapEngine.CoreEngine.TileObject;

public class ObjectMovement
{
    public MovePattern[] MovePatterns => _movePatterns.ToArray();
    private readonly List<MovePattern> _movePatterns;
    private TileObject _owner;

    public ObjectMovement(TileObject owner)
    {
        _owner = owner;
        _movePatterns = new();
    }

    public ObjectMovement(TileObject owner, List<MovePattern> movePatterns)
    {
        _owner = owner;
        _movePatterns = movePatterns;
    }

    public Position2D[] GetPossibleMoves() //return a tile object if encountered
    {
        List<Position2D> possibleMoves = new();
        foreach (MovePattern movePattern in _movePatterns)
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
        Position2D newPosition = _owner.Position;
        List<Position2D> availablePositions = new();
        TileMap tileMap = GameManager.TileMap;
        foreach (var movement in movePattern.Movement)
        {
            do
            {
                newPosition = MovePositionByMovement(movement, newPosition);
                availablePositions.Add(newPosition);

                if (tileMap.CheckTileExistsInPosition(newPosition)) return availablePositions;
            } while (tileMap[newPosition].CurrentTileObject == null);
        }

        return availablePositions;
    }

    private Position2D GetMovesByMovePattern(MovePattern movePattern)
    {
        return movePattern.Movement.Aggregate(_owner.Position,
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
            Movement.Forward_Right => new Position2D(1, -1),
            Movement.Forward_Left => new Position2D(-1, -1),
            Movement.Back_Right => new Position2D(1, 1),
            Movement.Back_Left => new Position2D(-1, 1),
            _ => throw new ArgumentOutOfRangeException(nameof(movement), movement, null)
        };

        return newPosition;
    }

    public void AddMovePattern(MovePattern movePattern)
    {
        _movePatterns.Add(movePattern);
    }

    public void RemoveMovePattern(MovePattern movePattern)
    {
        _movePatterns.Remove(movePattern);
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
    Forward_Right,
    Forward_Left,
    Back_Right,
    Back_Left
}