using ConsoleRenderer.ConsoleCommands;
using ConsoleRenderer.ConsoleRenderer;
using TileMapEngine.CoreEngine;
using TileObject = TileMapEngine.CoreEngine.Objects.TileObject;


namespace ConsoleRenderer;

public class ConsoleGameLoopManager : IGameLoopManager
{
    public TileObject CurrentSelectedTileObject;
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
    
    public void StartTwoPlayersGameLoop(Actor firstActor, Actor secondActor)
    {
        _isRunning = true;
        var currentTurnActor = firstActor;

        while (_isRunning)
        {
            _consoleCommandsManager.HandleUserInput(currentTurnActor);

            if (!GameManager.GetShouldAdvanceTurn())
            {
                continue;
            }
            
            if (currentTurnActor == firstActor)
            {
                currentTurnActor = secondActor;
                continue;
            }

            currentTurnActor = firstActor;
        }
    }

    public void StopGameLoop() => _isRunning = false;

    public ConsoleCommandsManager GetConsoleCommandsManager() => _consoleCommandsManager;

    public void RefreshGameViewport(bool clearConsole = false)
    {
        _gameRenderer.RefreshTileMapDraw(GameManager.TileMap, clearConsole);
    }

    public void SetSelectedTileObject(TileObject tileObject)
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