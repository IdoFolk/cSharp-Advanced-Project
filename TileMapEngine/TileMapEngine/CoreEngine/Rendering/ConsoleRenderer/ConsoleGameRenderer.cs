namespace TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

public class ConsoleGameRenderer : IGameRenderer<ConsoleColor>
{
    public void InitGameRenderer(TileMap tileMap)
    {
        var sceneRenderer = new ConsoleSceneRenderer();
        sceneRenderer.Initialize();

        var tileDrawable = new ConsoleDrawableString("[ ]", ConsoleColor.White);
        var tileRenderer = new ConsoleTileRenderer();
        tileMap.AssignRendererToTiles(tileRenderer, tileDrawable);
        
        
        RefreshTileMapDraw(tileMap);
    }

    public void RefreshTileMapDraw(TileMap tileMap)
    {
        foreach (var mapTile in tileMap)
        {
            mapTile.DrawTile();
        }
        
        Console.WriteLine("\n");
    }

    public void AssignCheckersPattern(TileMap tileMap, ConsoleColor oddColor, ConsoleColor evenColor)
    {
        foreach (var tile in tileMap)
        {
            if ((tile.Position.X + tile.Position.Y) % 2 == 0)
            {
                tile.TileRenderer.ChangeColor(evenColor);
                continue;
            }
            tile.TileRenderer.ChangeColor(oddColor);
        }
        
        RefreshTileMapDraw(tileMap);
    }
}