using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace TileMapEngine;

public static class GameManager
{
    public static event Action<TileObject> OnSelectCommand;
    public static event Action<TileObject> OnDeselectCommand;
    public static event Action<TileObject> OnMoveCommand;
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
            foreach (var possibleMove in SelectedTileObject.Movement.GetPossibleMoves())
            {
                if (TileMap.CheckTileExistsInPosition(possibleMove))
                {
                    TileMap[possibleMove].HighlightTile(ConsoleColor.Cyan);
                }
            }
            OnSelectCommand?.Invoke(SelectedTileObject);
            // TODO We need to refresh the screen
            return true;
        }

        return false;
    }
    public static bool TryDeselect()
    {
        if (SelectedTileObject != null)
        {
            OnDeselectCommand?.Invoke(SelectedTileObject);
            SelectedTileObject = null;
            // TODO We need to refresh the screen
            return true;
        }

        return false;
    }
    public static bool TryMove(Position2D position)
    {
        if (!TileMap.CheckTileExistsInPosition(position)) return false;
        
        if (SelectedTileObject != null && SelectedTileObject.TryMove(TileMap[position]))
        {
            OnMoveCommand?.Invoke(SelectedTileObject);
            // TODO We need to refresh the screen
            return true;
        }
        return false;
    }
}