namespace TileMapEngine.CoreEngine;

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

    public Position2D[] GetPossibleMoves()
    {
        List<Position2D> possibleMoves = new List<Position2D>();
        foreach (MovePattern movePattern in _movePatterns)
        {
            Position2D newPosition = _owner.Position;
            foreach (Movement movement in movePattern.Movement)
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
            }

            //TODO check if the tile is available to move to
            possibleMoves.Add(newPosition);
        }

        return possibleMoves.ToArray();
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

public struct MovePattern
{
    public Movement[] Movement { get; }

    public MovePattern(Movement[] movement)
    {
        Movement = movement;
    }
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