namespace TileMapEngine;

public class TileObject : ICloneable
{
    public ObjectMovement Movement { get; private set; }
    public Tile CurrentTile { get; private set; }
    public Position2D Position => CurrentTile.Position;

    public TileObject()
    {
        Movement = new ObjectMovement(this);
    }
    public TileObject(List<MovePattern> movePatterns)
    {
        Movement = new ObjectMovement(this, movePatterns);
    }

    public object Clone()
    {
        return new TileObject(new List<MovePattern>(Movement.MovePatterns));
    }
}