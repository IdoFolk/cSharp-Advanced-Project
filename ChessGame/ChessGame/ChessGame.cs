using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.ConsoleCommands;
using TileMapEngine.CoreEngine.Rendering;
using TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

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
        gameRenderer.AssignCheckersPattern(_tileMap, 
            ConsoleColor.White,
            ConsoleColor.Black);
        
        // TODO remove below, just for testing
        var renderer1 = new ConsoleTileRenderer();
        var consoleString1 = new ConsoleDrawableString("$", ConsoleColor.Yellow);
        renderer1.Init(consoleString1, new Position2D(1, 2));
        _tileMap[1, 2].PlaceTileObject(new TileObject(renderer1, _tileMap[1, 2]));

        var renderer2 = new ConsoleTileRenderer();
        var consoleString2 = new ConsoleDrawableString("#", ConsoleColor.Red);
        renderer2.Init(consoleString2, new Position2D(5, 3));
        _tileMap[5, 3].PlaceTileObject(new TileObject(renderer2, _tileMap[5, 3]));

        var renderer3 = new ConsoleTileRenderer();
        var consoleString3 = new ConsoleDrawableString("%", ConsoleColor.Green);
        renderer3.Init(consoleString3, new Position2D(2, 4));
        _tileMap[2, 4].PlaceTileObject(new TileObject(renderer3, _tileMap[2, 4]));
        
        gameRenderer.RefreshTileMapDraw(_tileMap);
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