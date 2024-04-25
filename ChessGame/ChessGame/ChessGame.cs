using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.ConsoleCommands;
using TileMapEngine.CoreEngine.Rendering;

namespace ChessGame;

public class ChessGame
{
    private TileMap _tileMap;
    private ConsoleCommandsManager _consoleCommandsManager;

    public void RunChessGame()
    {
        ConfigTileMap();


        ConfigGameRules();

        ConfigPlayers();
        
        ConfigConsoleCommands();

        StartGame();
    }
    
    private void ConfigTileMap()
    {
        _tileMap = new TileMap(8, 8);
        var gameRenderer = GameRendererManager.GetGameRenderer<ConsoleColor>(RendererType.Console);
        gameRenderer.InitGameRenderer(_tileMap);
        gameRenderer.AssignCheckersPattern(_tileMap, ConsoleColor.White, ConsoleColor.Black);
    }
    
    private void ConfigConsoleCommands()
    {
        _consoleCommandsManager = new ConsoleCommandsManager();
        _consoleCommandsManager.Init();
    }

    private void ConfigGameRules()
    {
        // Config the game rules:
        // Create the different chess pieces types
        // determine the rules for each piece type
        // determine the game's win condition
        // determine the game's flow (turns and their logic)
    }

    private void ConfigPlayers()
    {
        // Config the players
        // create 2 players
        // determine a name and color to each player
        // add to each players their 16 pieces
    }

    private void StartGame()
    {
        _consoleCommandsManager.WaitForInput();
    }
}