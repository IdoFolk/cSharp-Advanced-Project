namespace TileMapEngine;

public class Tile : IComparable<Tile>
{
    public Position2D Position { get; }
    public TileObject CurrentTileObject { get; private set; }

    public Tile(Position2D position) => Position = position;
    public int CompareTo(Tile other)
    {
        if (Position.Y > other.Position.Y) return 1;
        else if (Position.Y < other.Position.Y) return -1;
        else
        {
            if (Position.X > other.Position.X) return 1;
            else if (Position.X < other.Position.X) return -1;
            else return 0;
        }
    }
}