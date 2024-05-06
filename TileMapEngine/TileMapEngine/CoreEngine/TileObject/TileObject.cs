using System.Numerics;
using Renderer.Rendering;

namespace TileMapEngine.CoreEngine.TileObject;

public class TileObject : ICloneable
{
    public event Action? OnMove;
    public ObjectMovement Movement { get; private set; }
    public Position2D Position => CurrentTile.Position;
    public Tile? CurrentTile { get; set; }
    private ITileRenderer TileRenderer { get; }
    
    public Actor OwnerActor { get; }

    protected TileObject(ITileRenderer tileRenderer, Tile? currentTile, Actor ownerActor)
    {
        TileRenderer = tileRenderer;
        CurrentTile = currentTile;
        OwnerActor = ownerActor;
        Movement = new ObjectMovement(this);
    }

    protected TileObject(ITileRenderer tileRenderer, Tile? currentTile, List<MovePattern> movePatterns, Actor ownerActor)
    {
        TileRenderer = tileRenderer;
        CurrentTile = currentTile;
        OwnerActor = ownerActor;
        Movement = new ObjectMovement(this, movePatterns);
    }

    public bool TryMove(Tile? newTile)
    {
        if (newTile == null || !Movement.GetPossibleMoves().Contains(newTile.Position)) return false;

        if (!CheckPossibleMoveTileCallback(newTile)) return false;
        
        OnMove?.Invoke();
        newTile.PlaceTileObject(this);
        CurrentTile = newTile;
        TileRenderer.UpdatePosition(new Vector2(Position.X, Position.Y));

        return true;

    }

    public void DrawTileObject(int rowsOffset = 0) => TileRenderer.Draw(rowsOffset,true);

    public object Clone()
    {
        return new TileObject(TileRenderer,CurrentTile, [..Movement.MovePatterns], OwnerActor);
    }

    public virtual void HandleOtherTileObjectInPossibleMoveCallback(TileObject tileObject)
    {
        // To be overriden by inheritors
    }

    public virtual bool CheckPossibleMoveTileCallback(Tile tile)
    {
        return true; // To be overriden by inheritors
    }
}