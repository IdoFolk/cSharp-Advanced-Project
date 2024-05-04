namespace TileMapEngine.CoreEngine;

public interface IGameLoopManager
{
    public void Init(TileMap? tileMap);
    public void StartGameLoop();
    public void StopGameLoop();
    public void RefreshGameViewport(bool clearConsole = false);
    public void SetSelectedTileObject(TileMapEngine.CoreEngine.TileObject.TileObject tileObject);
}