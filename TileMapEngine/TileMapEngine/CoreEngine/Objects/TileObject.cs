using System.Numerics;
using Renderer.Rendering;

namespace TileMapEngine.CoreEngine.Objects;

public abstract class TileObject : ICloneable
{
    /// <summary>
    /// Event that is raised when an object is moved to another tile.
    /// </summary>
    public event Action? OnMove;
    public ObjectMovement Movement { get; private set; }
    public Position2D Position => CurrentTile.Position;
    public Tile? CurrentTile { get; set; }
    private ITileRenderer TileRenderer { get; }
    public Actor OwnerActor { get; }
    
    protected TileObject(ITileRenderer tileRenderer, Tile? currentTile, List<MovePattern> movePatterns)
    {
        TileRenderer = tileRenderer;
        CurrentTile = currentTile;
        Movement = new ObjectMovement(this, movePatterns);
    }

    protected TileObject(ITileRenderer tileRenderer, Tile? currentTile, List<MovePattern> movePatterns, Actor ownerActor)
    {
        TileRenderer = tileRenderer;
        CurrentTile = currentTile;
        OwnerActor = ownerActor;
        Movement = new ObjectMovement(this, movePatterns);
    }

    /// <summary>
    /// Tries to move the TileObject to the specified Tile.
    /// </summary>
    /// <returns>Returns true if the TileObject was successfully moved, false otherwise.</returns>
    public bool TryMove(Tile? newTile)
    {
        if (newTile == null || !Movement.GetPossibleMoves().Contains(newTile.Position)) return false;

        if (!CheckPossibleMoveTileCallback(newTile)) return false;

        OnMoveCallback(newTile);
        OnMove?.Invoke();
        newTile.PlaceTileObject(this);
        CurrentTile = newTile;
        TileRenderer.UpdatePosition(new Vector2(Position.X, Position.Y));

        return true;

    }

    /// <summary>
    /// Draws the tile object on the screen.
    /// </summary>
    /// <param name="rowsOffset">The number of rows to offset the drawing.</param>
    public void DrawTileObject(int rowsOffset = 0) => TileRenderer.Draw(rowsOffset,true);

    /// <summary>
    /// Handles the interaction with another <see cref="TileObject"/> when attempting a possible move.
    /// </summary>
    /// <param name="tileObject">The other <see cref="TileObject"/> being interacted with.</param>
    public abstract void HandleOtherTileObjectInPossibleMoveCallback(TileObject tileObject);

    /// <summary>
    /// Callback method to check if a tile is a possible move for a tile object.
    /// </summary>
    /// <param name="tile">The tile to be checked.</param>
    /// <returns>True if the tile is a possible move, false otherwise.</returns>
    public abstract bool CheckPossibleMoveTileCallback(Tile tile);

    /// <summary>
    /// Callback method invoked when the TileObject is moved to a new Tile.
    /// </summary>
    /// <param name="newTile">The new Tile to which the TileObject is moved.</param>
    public abstract void OnMoveCallback(Tile newTile);

    /// <summary>
    /// Creates a deep copy of the ChessGamePiece object.
    /// </summary>
    /// <returns>The cloned ChessGamePiece object.</returns>
    public abstract object Clone();

}