using TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

namespace TileMapEngine.CoreEngine.Rendering;

public class RenderingUpdatesHandler()
{
    private ConsoleGameRenderer _owner;
    private TileMap _tileMap;

    public void Init(ConsoleGameRenderer owner, TileMap tileMap)
    {
        _owner = owner;
        _tileMap = tileMap;
        ConsoleGameLoopManager.OnSelectedTileObjectChanged += HandleOnSelectedTileObjectChanged;
    }

    private void HandleOnSelectedTileObjectChanged(TileObject selectedTileObject)
    {
        _owner.RefreshTileMapDraw(_tileMap);
    }
}