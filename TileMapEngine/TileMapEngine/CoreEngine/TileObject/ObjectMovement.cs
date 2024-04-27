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
                possibleMoves.Add(GetMovesByMovements(movePattern));
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
            } while (tileMap[newPosition].CurrentTileObject == null);
        }

        return availablePositions;
    }

    private Position2D GetMovesByMovements(MovePattern movePattern)
    {
        Position2D newPosition = _owner.Position;
        foreach (Movement movement in movePattern.Movement)
        {
            newPosition = MovePositionByMovement(movement, newPosition);
        }

        return newPosition;
    }

    private static Position2D MovePositionByMovement(Movement movement, Position2D newPosition)
    {
        switch (movement)
        {
            case Movement.Left:
                newPosition += new Position2D(-1, 0);
                break;
            case Movement.Right:
                newPosition += new Position2D(1, 0);
                break;
            case Movement.Forward:
                newPosition += new Position2D(0, -1);
                break;
            case Movement.Back:
                newPosition += new Position2D(0, 1);
                break;
            case Movement.Forward_Right:
                newPosition += new Position2D(1, -1);
                break;
            case Movement.Forward_Left:
                newPosition += new Position2D(-1, -1);
                break;
            case Movement.Back_Right:
                newPosition += new Position2D(1, 1);
                break;
            case Movement.Back_Left:
                newPosition += new Position2D(-1, 1);
                break;
        }

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