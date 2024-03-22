using TileMapEngine;

class Program
{
    public static void Main(string[] args)
    {
        TileMap tileMap = new TileMap(6, 6);
        foreach (var tile in tileMap)
        {
            if(tile.Position.X == 0) Console.WriteLine();
            Console.Write(tile);
        }
        
    }
}