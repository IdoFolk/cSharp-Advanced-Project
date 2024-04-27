using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace TileMapEngine;

public static class GameManager
{
    public static event Action<TileObject> OnSelect;
    public static event Action<TileObject> OnDeselect;
    public static event Action<TileObject> OnMove;
    public static TileMap TileMap { get; private set; }
    public static TileObject? SelectedTileObject { get; private set; }

    public static void Init(TileMap tileMap)
    {
        TileMap = tileMap;
    }

    public static bool TrySelect(Position2D position)
    {
        if (TileMap.CheckTileObjectInPosition(position))
        {
            SelectedTileObject = TileMap[position]?.CurrentTileObject;
            OnSelect?.Invoke(SelectedTileObject);
            return true;
        }

        return false;
    }
    public static bool TryDeselect()
    {
        if (SelectedTileObject != null)
        {
            OnDeselect?.Invoke(SelectedTileObject);
            SelectedTileObject = null;
            return true;
        }

        return false;
    }
    public static bool TryMove(Position2D position)
    {
        if (!TileMap.CheckTileExistsInPosition(position)) return false;
        
        if (SelectedTileObject != null && SelectedTileObject.TryMove(TileMap[position]))
        {
            OnMove?.Invoke(SelectedTileObject);
            return true;
        }
        return false;
    }
}