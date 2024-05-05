namespace TileMapEngine.CoreEngine;

public readonly struct Position2D 
{
    public int X { get; }
    public int Y { get; }

    public Position2D(int value) : this(value, value)
    {
        Console.WriteLine();
    }

    public Position2D(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Position2D operator +(Position2D p1, Position2D p2)
    {
        return new Position2D(
            p1.X + p2.X,
            p1.Y + p2.Y
        );
    }

    public static Position2D operator -(Position2D p1, Position2D p2)
    {
        return new Position2D(
            p1.X - p2.X,
            p1.Y - p2.Y
        );
    }
    public static bool operator ==(Position2D p1, Position2D p2)
    {
        return p1.Equals(p2);
    }

    public static bool operator !=(Position2D p1, Position2D p2)
    {
        return !p1.Equals(p2);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Position2D pos)
        {
            return (pos.X == X && pos.Y == Y);
        }
        return false;
    }

    public override string ToString() => $"{X},{Y}";
    public override int GetHashCode() => HashCode.Combine(X, Y);
}