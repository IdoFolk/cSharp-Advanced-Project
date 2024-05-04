namespace TileMapEngine.CoreEngine;

public static class GameManager
{
    public static event Action<TileObject.TileObject>? OnSelectCommand;
    public static event Action<TileObject.TileObject>? OnDeselectCommand;
    public static event Action<TileObject.TileObject>? OnMoveCommand;
    public static TileMap? TileMap { get; private set; }
    private static TileObject.TileObject? SelectedTileObject { get; set; }
    private static List<Tile>? CurrentHighlightedTiles { get; set; }

    private static IGameLoopManager? _gameLoopManager;

    public static void InitTileMap(TileMap? tileMap, IGameLoopManager? gameLoopManager)
    {
        TileMap = tileMap;
        CurrentHighlightedTiles = new List<Tile>();
        _gameLoopManager = gameLoopManager;
        _gameLoopManager?.Init(tileMap);
    }

    public static void HighlightTile(Tile tile)
    {
        tile.HighlightTile(false);
        CurrentHighlightedTiles?.Add(tile);
    }

    public static IGameLoopManager? GetGameLoopManager() => _gameLoopManager;
    
    public static void AddObjectToTileMap(TileObject.TileObject tileObject, Position2D position2D)
    {
        TileMap?[position2D]?.PlaceTileObject(tileObject);
    }
    
    public static bool TrySelect(Position2D position, bool highlightPossibleMoveTiles = true)
    {
        if (SelectedTileObject != null)
        {
            ClearSelectedObject();
        }
        
        if (TileMap != null && !TileMap.CheckTileObjectInPosition(position)) return false;

        SelectedTileObject = TileMap?[position]?.CurrentTileObject;
        if (SelectedTileObject == null)
        {
            return false;
        }

        foreach (var possibleMove in SelectedTileObject.Movement.GetPossibleMoves())
        {
            if (TileMap != null && (!TileMap.CheckTileExistsInPosition(possibleMove) || !highlightPossibleMoveTiles)) continue;

            var tile = TileMap?[possibleMove];
            if (tile == null)
            {
                continue;
            }

            if (!SelectedTileObject.CheckPossibleMoveTileCallback(tile))
            {
                continue;
            }

            var tileObject = tile.CurrentTileObject;
            if (tileObject != null)
            {
                SelectedTileObject.HandleOtherTileObjectInPossibleMoveCallback(tileObject);
                continue;
            }

            HighlightTile(tile);
        }

        OnSelectCommand?.Invoke(SelectedTileObject);
        return true;
    }

    public static bool TryDeselect()
    {
        if (SelectedTileObject == null) return false;

        OnDeselectCommand?.Invoke(SelectedTileObject);
        ClearSelectedObject();
        return true;
    }

    public static bool TryMove(Position2D position)
    {
        if (TileMap != null && !TileMap.CheckTileExistsInPosition(position)) return false;

        if (SelectedTileObject == null) return false;

        var tile = TileMap?[position];

        if (tile == null) return false;
        
        if (!SelectedTileObject.TryMove(tile)) return false;

        ClearSelectedObject();
        OnMoveCommand?.Invoke(SelectedTileObject);
        return true;
    }

    public static void RefreshGameViewport(bool clear) => _gameLoopManager?.RefreshGameViewport(clear);
    public static void StopGameLoop() => _gameLoopManager?.StopGameLoop();

    private static void ClearSelectedObject()
    {
        SelectedTileObject = null;
        if (CurrentHighlightedTiles == null) return;
        
        foreach (var highlightedTile in CurrentHighlightedTiles)
        {
            highlightedTile?.ResetHighlight(false);
        }

        CurrentHighlightedTiles.Clear();
    }
}