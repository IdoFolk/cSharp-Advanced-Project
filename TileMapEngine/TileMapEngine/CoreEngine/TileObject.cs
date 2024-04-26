using Renderer.Rendering;

namespace TileMapEngine.CoreEngine;

public class TileObject : ICloneable
{
    public ObjectMovement Movement { get; private set; }
    public Tile CurrentTile { get; private set; }
    public Position2D Position => CurrentTile.Position;
    public ITileRenderer TileRenderer { get; }

    public TileObject(ITileRenderer tileRenderer, Tile currentTile)
    {
        TileRenderer = tileRenderer;
        CurrentTile = currentTile;
        Movement = new ObjectMovement(this);
    }
    public TileObject(ITileRenderer tileRenderer, Tile currentTile, List<MovePattern> movePatterns)
    {
        TileRenderer = tileRenderer;
        CurrentTile = currentTile;
        Movement = new ObjectMovement(this, movePatterns);
    }

    public void DrawTileObject() => TileRenderer.Draw(true);

    public object Clone()
    {
        return new TileObject(TileRenderer,CurrentTile,new List<MovePattern>(Movement.MovePatterns));
    }
}