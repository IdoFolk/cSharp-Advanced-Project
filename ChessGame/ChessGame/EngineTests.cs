using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.Rendering;
using TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

namespace ChessGame;

public class EngineTests
{
    public static void RunTest()
    {
        var map = new TileMap(8, 8);
        var gameRenderer = GameRendererManager.GetGameRenderer<ConsoleColor>(RendererType.Console);
        gameRenderer.InitGameRenderer(map);
        gameRenderer.AssignCheckersPattern(map, ConsoleColor.White, ConsoleColor.Black);
        
        var renderer1 = new ConsoleTileRenderer();
        var consoleString1 = new ConsoleDrawableString("$", ConsoleColor.Yellow);
        renderer1.Init(consoleString1, new Position2D(1, 2));
        map[1, 2].PlaceTileObject(new TileObject(renderer1, map[1, 2]));

        var renderer2 = new ConsoleTileRenderer();
        var consoleString2 = new ConsoleDrawableString("#", ConsoleColor.Red);
        renderer2.Init(consoleString2, new Position2D(5, 3));
        map[5, 3].PlaceTileObject(new TileObject(renderer2, map[5, 3]));

        var renderer3 = new ConsoleTileRenderer();
        var consoleString3 = new ConsoleDrawableString("%", ConsoleColor.Green);
        renderer3.Init(consoleString3, new Position2D(2, 4));
        map[2, 4].PlaceTileObject(new TileObject(renderer3, map[2, 4]));


        gameRenderer.RefreshTileMapDraw(map);
    }
}