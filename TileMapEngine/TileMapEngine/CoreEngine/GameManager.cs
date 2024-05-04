using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace TileMapEngine;

public static class GameManager
{
    public static event Action<TileObject> OnSelectCommand;
    public static event Action<TileObject> OnDeselectCommand;
    public static event Action<TileObject> OnMoveCommand;
    public static TileMap TileMap { get; private set; }
    private static TileObject? SelectedTileObject { get; set; }
    private static List<Tile> CurrentHighlightedTiles { get; set; }

    private static IGameLoopManager _gameLoopManager;
    public static void Init(TileMap tileMap, IGameLoopManager gameLoopManager)
    {
        TileMap = tileMap;
        CurrentHighlightedTiles = new List<Tile>();
        _gameLoopManager = gameLoopManager;
        _gameLoopManager.Init(tileMap);
    }

    public static IGameLoopManager GetGameLoopManager() => _gameLoopManager;

    public static bool TrySelect(Position2D position, bool highlightPossibleMoveTiles = true)
    {
        if (!TileMap.CheckTileObjectInPosition(position)) return false;
        
        SelectedTileObject = TileMap[position]?.CurrentTileObject;
        if (SelectedTileObject == null)
        {
            return false;
        }
            
        foreach (var possibleMove in SelectedTileObject.Movement.GetPossibleMoves())
        {
            if (!TileMap.CheckTileExistsInPosition(possibleMove) || !highlightPossibleMoveTiles) continue;
            
            TileMap[possibleMove]?.HighlightTile(false);
            CurrentHighlightedTiles.Add(TileMap[possibleMove]);
        }
        
        OnSelectCommand?.Invoke(SelectedTileObject);
        // TODO We need to refresh the screen
        return true;

    }
    public static bool TryDeselect()
    {
        if (SelectedTileObject == null) return false;
        
        OnDeselectCommand?.Invoke(SelectedTileObject);
        ClearSelectedObject();
        // TODO We need to refresh the screen
        return true;

    }
    public static bool TryMove(Position2D position)
    {
        if (!TileMap.CheckTileExistsInPosition(position)) return false;

        if (SelectedTileObject == null || !SelectedTileObject.TryMove(TileMap[position])) return false;
        
        ClearSelectedObject();
        OnMoveCommand?.Invoke(SelectedTileObject);
            
        return true;
    }
    public static void RefreshGameViewport(bool clear) => _gameLoopManager.RefreshGameViewport(clear);
    public static void StopGameLoop() => _gameLoopManager.StopGameLoop();
    private static void ClearSelectedObject()
    {
        SelectedTileObject = null;
        foreach (var highlightedTile in CurrentHighlightedTiles)
        {
            highlightedTile.ResetHighlight(false);
        }
        
        CurrentHighlightedTiles.Clear();
    }

}