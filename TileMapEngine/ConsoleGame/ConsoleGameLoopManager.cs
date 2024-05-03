using ConsoleRenderer.ConsoleCommands;
using ConsoleRenderer.ConsoleRenderer;
using TileMapEngine;
using TileMapEngine.CoreEngine;

namespace ConsoleRenderer;

public static class ConsoleGameLoopManager 
{
    public static event Action<TileMapEngine.CoreEngine.TileObject.TileObject> OnSelectedTileObjectChanged;
    
    public static TileMapEngine.CoreEngine.TileObject.TileObject CurrentSelectedTileObject;
    private static ConsoleGameRenderer _gameRenderer;
    
    private static ConsoleCommandsManager _consoleCommandsManager;

    private static bool _isRunning;

    public static void Init()
    {
        ConfigConsoleCommands();
    }

    public static void ConfigTileMap(TileMap tileMap, ConsoleColor oddColor, ConsoleColor evenColor)
    {
        _gameRenderer = new ConsoleGameRenderer();
        _gameRenderer.InitGameRenderer(tileMap);
        _gameRenderer.AssignCheckersPattern(tileMap, oddColor, evenColor);
        GameManager.Init(tileMap);
    }

    public static void StartGameLoop()
    {
        _isRunning = true;

        while (_isRunning)
        {
            _consoleCommandsManager.HandleUserInput();
        }
    }

    public static void StopGameLoop() => _isRunning = false;

    public static ConsoleCommandsManager GetConsoleCommandsManager() => _consoleCommandsManager;

    public static void RefreshGameViewport()
    {
        Console.Clear();
        _gameRenderer.RefreshTileMapDraw(GameManager.TileMap);
    }

    public static void SetSelectedTileObject(TileMapEngine.CoreEngine.TileObject.TileObject tileObject)
    {
        CurrentSelectedTileObject = tileObject;
        OnSelectedTileObjectChanged(CurrentSelectedTileObject);
    }

    private static void ConfigConsoleCommands()
    {
        _consoleCommandsManager = new ConsoleCommandsManager();
        _consoleCommandsManager.Init();
    }
}

public struct ConsoleTileObjectConfig
{
    
}