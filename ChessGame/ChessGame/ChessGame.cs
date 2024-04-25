using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.ConsoleCommands;
using TileMapEngine.CoreEngine.Rendering;

namespace ChessGame;

public class ChessGame
{
    private TileMap _tileMap;

    public void RunChessGame()
    {
        ConsoleGameLoopManager.Init();
        
        ConfigTileMap();

        ConfigGameConsoleCommands();

        ConfigGameRules();

        ConfigPlayers();
        
        StartGame();
    }

    private void ConfigTileMap()
    {
        _tileMap = new TileMap(8, 8);
        var gameRenderer = GameRendererManager.GetGameRenderer<ConsoleColor>(RendererType.Console);
        gameRenderer.InitGameRenderer(_tileMap);
        gameRenderer.AssignCheckersPattern(_tileMap, ConsoleColor.White, ConsoleColor.Black);
    }
    
    private void ConfigGameConsoleCommands()
    {
        var commandsManager = ConsoleGameLoopManager.GetConsoleCommandsManager();
        var moves = new ConsoleCommand("moves",
            "Shows the possible moves of the selected unit (if possible). example: /moves",
            false,
            _ => Console.WriteLine($"Showing possible moves of selected unit"));
        commandsManager.AddCommand(moves);
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
        ConsoleGameLoopManager.StartGameLoop();
    }
}