using TileMapEngine.CoreEngine.ConsoleCommands;

namespace TileMapEngine.CoreEngine;

public static class ConsoleGameLoopManager
{
    public static event Action<TileObject> OnSelectedTileObjectChanged;
    
    public static TileObject CurrentSelectedTileObject;
    
    private static ConsoleCommandsManager _consoleCommandsManager;

    private static bool _isRunning;

    public static void Init()
    {
        ConfigConsoleCommands();
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

    public static void SetSelectedTileObject(TileObject tileObject)
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