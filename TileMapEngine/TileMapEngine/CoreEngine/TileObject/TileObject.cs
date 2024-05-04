using System.Numerics;
using Renderer.Rendering;

namespace TileMapEngine.CoreEngine.TileObject;

public class TileObject : ICloneable
{
    public event Action? OnMove;
    public ObjectMovement Movement { get; private set; }
    public Position2D Position => CurrentTile.Position;
    private Tile? CurrentTile { get; set; }
    private ITileRenderer TileRenderer { get; }

    protected TileObject(ITileRenderer tileRenderer, Tile? currentTile)
    {
        TileRenderer = tileRenderer;
        CurrentTile = currentTile;
        Movement = new ObjectMovement(this);
    }

    protected TileObject(ITileRenderer tileRenderer, Tile? currentTile, List<MovePattern> movePatterns)
    {
        TileRenderer = tileRenderer;
        CurrentTile = currentTile;
        Movement = new ObjectMovement(this, movePatterns);
    }

    public bool TryMove(Tile? newTile)
    {
        if (Movement.GetPossibleMoves().Contains(newTile.Position))
        {
            OnMove?.Invoke();
            newTile.PlaceTileObject(this);
            CurrentTile = newTile;
            TileRenderer.UpdatePosition(new Vector2(Position.X, Position.Y));

            return true;
        }

        return false;
    }

    public void DrawTileObject(int rowsOffset = 0) => TileRenderer.Draw(rowsOffset,true);

    public object Clone()
    {
        return new TileObject(TileRenderer,CurrentTile,new List<MovePattern>(Movement.MovePatterns));
    }
}