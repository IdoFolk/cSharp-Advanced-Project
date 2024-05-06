using TileMapEngine.CoreEngine.Objects;

namespace TileMapEngine.CoreEngine;

public static class GameManager
{
    public static event Action<TileObject>? OnSelectCommand;
    public static event Action<TileObject>? OnDeselectCommand;
    public static event Action<TileObject>? OnMoveCommand;
    public static TileMap? TileMap { get; private set; }
    private static TileObject? SelectedTileObject { get; set; }
    private static List<Tile>? CurrentHighlightedTiles { get; set; }

    private static IGameLoopManager? _gameLoopManager;

    private static bool _shouldAdvanceTurn;

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
    
    public static bool GetShouldAdvanceTurn()
    {
        if (_shouldAdvanceTurn)
        {
            _shouldAdvanceTurn = false;
            return true;
        }

        return false;
    }
    
    public static void AddObjectToTileMap(TileObject tileObject, Position2D position2D)
    {
        TileMap?[position2D]?.PlaceTileObject(tileObject);
    }
    
    public static bool TrySelect(Actor playingActor, Position2D position,
        bool highlightPossibleMoveTiles = true)
    {
        if (SelectedTileObject != null)
        {
            ClearSelectedObject();
        }
        
        if (TileMap != null && !TileMap.CheckTileObjectInPosition(position)) return false;

        var tileObjectToSelect = TileMap?[position]?.CurrentTileObject;

        if (tileObjectToSelect == null)
        {
            return false;
        }

        if (tileObjectToSelect.OwnerActor != playingActor)
        {
            return false;
        }
        
        SelectedTileObject = tileObjectToSelect;
        _gameLoopManager?.SetSelectedTileObject(SelectedTileObject);

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
    public static bool GetIsAnySelected() => SelectedTileObject != null;

    private static void ClearSelectedObject()
    {
        SelectedTileObject = null;
        _gameLoopManager.SetSelectedTileObject(SelectedTileObject);
        if (CurrentHighlightedTiles == null) return;
        
        foreach (var highlightedTile in CurrentHighlightedTiles)
        {
            highlightedTile?.ResetHighlight(false);
        }

        CurrentHighlightedTiles.Clear();
    }
}