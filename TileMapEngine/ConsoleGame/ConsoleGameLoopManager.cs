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
    private bool _shouldMoveToNextTurn;

    public void Init(TileMap? tileMap)
    {
        ConfigTileMap(tileMap);
        ConfigConsoleCommands();

        TileMapManager.OnTileObjectSelected += SetSelectedTileObject;
        TileMapManager.OnDeselected += ClearCurrentSelectedObject;
        TileMapManager.OnTileObjectMoved += HandleOnTileObjectMoved;
    }

    private void HandleOnTileObjectMoved(TileObject obj)
    {
        _shouldMoveToNextTurn = true;
    }

    public void AssignCheckersPattern(TileMap? tileMap, ConsoleColor oddColor, ConsoleColor evenColor)
    {
        _gameRenderer.AssignCheckersPattern(tileMap, oddColor, evenColor);
    }
    
    public void StartTwoPlayersGameLoop(Actor? firstActor, Actor? secondActor)
    {
        if (firstActor == null || secondActor == null)
        {
            throw new Exception("One or more of the assigned actors are null.");
        }
        
        _isRunning = true;
        var currentTurnActor = firstActor;

        while (_isRunning)
        {
            _consoleCommandsManager.HandleUserInput(currentTurnActor);

            if (!_shouldMoveToNextTurn)
            {
                continue;
            }
            
            _shouldMoveToNextTurn = false;
            
            if (currentTurnActor == firstActor)
            {
                currentTurnActor = secondActor;
                continue;
            }

            currentTurnActor = firstActor;
        }
    }

    public void StopGameLoop()
    {
        _isRunning = false;
        
    }

    public ConsoleCommandsManager GetConsoleCommandsManager() => _consoleCommandsManager;

    public void RefreshGameViewport(bool clearConsole = false)
    {
        _gameRenderer.RefreshTileMapDraw(TileMapManager.TileMap, clearConsole);
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
    
    private void ClearCurrentSelectedObject()
    {
        CurrentSelectedTileObject = null;
    }
}