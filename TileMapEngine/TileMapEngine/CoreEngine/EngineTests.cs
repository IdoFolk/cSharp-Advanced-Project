using TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

namespace TileMapEngine.CoreEngine;

public class EngineTests
{
    public static void RunTest()
    {
        var sceneRenderer = new ConsoleSceneRenderer();
        
        var emptyTileRenderer = new ConsoleTileRenderer();
        TileMap map = new TileMap(8,8,sceneRenderer, emptyTileRenderer);

        var renderer1 = new ConsoleTileRenderer();
        var consoleString1 = new ConsoleCharDrawableObject('$', ConsoleColor.Yellow);
        renderer1.Init(consoleString1, new Position2D(1,2));
        map[1, 2].PlaceTileObject(new TileObject(renderer1,map[1,2]));

        var renderer2 = new ConsoleTileRenderer();
        var consoleString2 = new ConsoleCharDrawableObject('#', ConsoleColor.Red);
        renderer2.Init(consoleString2, new Position2D(5,3));
        map[5, 3].PlaceTileObject(new TileObject(renderer2,map[5,3]));
        
        var renderer3 = new ConsoleTileRenderer();
        var consoleString3 = new ConsoleCharDrawableObject('%', ConsoleColor.Green);
        renderer3.Init(consoleString3, new Position2D(2,4));
        map[2, 4].PlaceTileObject(new TileObject(renderer3,map[2,4]));
        
        for (int i = 0; i < map.Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < map.Tiles.GetLength(1); j++)
            {
                map[j,i].DrawTile();
            }

            Console.WriteLine();
        }
    }
}