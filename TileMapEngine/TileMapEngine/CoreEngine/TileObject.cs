namespace TileMapEngine;

public class TileObject : ICloneable
{
    public ObjectMovement Movement { get; private set; }
    public Tile CurrentTile { get; private set; }
    public Position2D Position => CurrentTile.Position;
    public char Render { get; } //temp need to replace this with a render class/struct

    public TileObject(char render, Tile currentTile)
    {
        Render = render;
        CurrentTile = currentTile;
        Movement = new ObjectMovement(this);
    }
    public TileObject(char render, Tile currentTile, List<MovePattern> movePatterns)
    {
        Render = render;
        CurrentTile = currentTile;
        Movement = new ObjectMovement(this, movePatterns);
    }

    public object Clone()
    {
        return new TileObject(Render,CurrentTile,new List<MovePattern>(Movement.MovePatterns));
    }
}