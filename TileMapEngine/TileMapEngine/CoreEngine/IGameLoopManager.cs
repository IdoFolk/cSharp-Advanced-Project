namespace TileMapEngine.CoreEngine;

public interface IGameLoopManager
{
    public void Init(TileMap? tileMap);
    public void StopGameLoop();
    public void RefreshGameViewport(bool clearConsole = false);
}