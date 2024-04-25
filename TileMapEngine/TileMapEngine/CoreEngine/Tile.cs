using TileMapEngine.CoreEngine.Rendering;

namespace TileMapEngine.CoreEngine;

public class Tile : IComparable<Tile>
{
    public event Action<TileObject> OnTileObjectLand;
    public Position2D Position { get; }
    public TileObject CurrentTileObject { get; private set; }

    public ITileRenderer TileRenderer { get; }

    public Tile(Position2D position, ITileRenderer tileRenderer)
    {
        Position = position;
        TileRenderer = tileRenderer;
    }

    public Tile(int x,int y, ITileRenderer tileRenderer)
    {
        Position = new Position2D(x, y);
        TileRenderer = tileRenderer;
    }

    public void PlaceTileObject(TileObject tileObject)
    {
        CurrentTileObject = tileObject;
        OnTileObjectLand?.Invoke(tileObject);
    }

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

    public override string ToString() => $"[{Position.ToString()}]";

}