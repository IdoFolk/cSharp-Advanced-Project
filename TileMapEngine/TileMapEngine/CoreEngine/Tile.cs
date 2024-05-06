using Renderer.Rendering;
using TileMapEngine.CoreEngine.Objects;

namespace TileMapEngine.CoreEngine;

public class Tile : IComparable<Tile>
{
    public event Action<TileObject> OnTileObjectLand;
    public Position2D Position { get; }
    public TileObject? CurrentTileObject { get; private set; }
    public ITileRenderer TileRenderer { get; private set; }

    public Tile(Position2D position)
    {
        Position = position;
    }

    public Tile(int x,int y)
    {
        Position = new Position2D(x, y);
    }

    public void AssignRenderer(ITileRenderer renderer) => TileRenderer = renderer;

    public void PlaceTileObject(TileObject tileObject)
    {
        CurrentTileObject = tileObject;
        CurrentTileObject.OnMove += RemoveTileObject;
        OnTileObjectLand?.Invoke(tileObject);
    }

    private void RemoveTileObject()
    {
        CurrentTileObject.OnMove -= RemoveTileObject;
        CurrentTileObject = null;
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

    public void DrawTile(int rowsOffset = 0)
    {
        TileRenderer.Draw(rowsOffset);
        
        if (CurrentTileObject != null)
        {
            CurrentTileObject.DrawTileObject(rowsOffset);
        }
    }

    public void HighlightTile(bool redraw)
    {
        TileRenderer.Highlight(redraw);
    }

    public override string ToString() => $"[{Position.ToString()}]";

    public void ResetHighlight(bool redraw)
    {
        TileRenderer.ResetHighlight(redraw);
    }
}