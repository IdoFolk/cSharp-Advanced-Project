namespace TileMapEngine;

public class EngineTests
{
    public static void RunTest()
    {
        TileMap map = new TileMap(8,8);
        map[1, 2].PlaceTileObject(new TileObject('$',map[1,2]));
        map[5, 3].PlaceTileObject(new TileObject('&',map[5,3]));
        map[2, 4].PlaceTileObject(new TileObject('%',map[2,4]));
        for (int i = 0; i < map.Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < map.Tiles.GetLength(1); j++)
            {
                if (map[j,i].CurrentTileObject == null)
                {
                    Console.Write("[ ]");
                }
                else Console.Write($"[{map[j,i].CurrentTileObject.Render}]");
            }

            Console.WriteLine();
        }
    }
}