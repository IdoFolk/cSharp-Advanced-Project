using ConsoleRenderer.ConsoleCommands;
using ConsoleRenderer.ConsoleRenderer;
using TileMapEngine;
using TileMapEngine.CoreEngine;

namespace ConsoleRenderer;

public class ConsoleGameLoopManager : IGameLoopManager
{
    public TileMapEngine.CoreEngine.TileObject.TileObject CurrentSelectedTileObject;
    private ConsoleGameRenderer _gameRenderer;
    
    private ConsoleCommandsManager _consoleCommandsManager;

    private bool _isRunning;

    public void Init(TileMap? tileMap)
    {
        ConfigTileMap(tileMap);
        ConfigConsoleCommands();
    }

    public void AssignCheckersPattern(TileMap? tileMap, ConsoleColor oddColor, ConsoleColor evenColor)
    {
        _gameRenderer.AssignCheckersPattern(tileMap, oddColor, evenColor);
    }

    public void StartGameLoop()
    {
        _isRunning = true;

        while (_isRunning)
        {
            _consoleCommandsManager.HandleUserInput();
        }
    }

    public void StopGameLoop() => _isRunning = false;

    public ConsoleCommandsManager GetConsoleCommandsManager() => _consoleCommandsManager;

    public void RefreshGameViewport(bool clearConsole = false)
    {
        _gameRenderer.RefreshTileMapDraw(GameManager.TileMap, clearConsole);
    }

    public void SetSelectedTileObject(TileMapEngine.CoreEngine.TileObject.TileObject tileObject)
    {
        CurrentSelectedTileObject = tileObject;
    }
    
    private void ConfigTileMap(TileMap? tileMap)
    {
        _gameRenderer = new ConsoleGameRenderer();
        _gameRenderer.InitGameRenderer(tileMap);
    }

    private void ConfigConsoleCommands()
    {
        _consoleCommandsManager = new ConsoleCommandsManager();
        _consoleCommandsManager.Init();
    }
}