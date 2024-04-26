namespace TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

public class ConsoleGameRenderer : IGameRenderer<ConsoleColor>
{
    private RenderingUpdatesHandler? _renderingUpdatesHandler;

    public void InitGameRenderer(TileMap tileMap)
    {
        var sceneRenderer = new ConsoleSceneRenderer();
        sceneRenderer.Initialize();

        var tileDrawable = new ConsoleDrawableString("[ ]", ConsoleColor.White);
        var tileRenderer = new ConsoleTileRenderer();
        tileMap.AssignRendererToTiles(tileRenderer, tileDrawable);

        _renderingUpdatesHandler = new RenderingUpdatesHandler();
        _renderingUpdatesHandler.Init(this, tileMap);

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

    public void AssignCheckersPattern(TileMap tileMap, ConsoleColor oddColor, ConsoleColor evenColor,
        ConsoleColor bgColor = default)
    {
        foreach (var tile in tileMap)
        {
            if ((tile.Position.X + tile.Position.Y) % 2 == 0)
            {
                tile.TileRenderer.ChangeColor(evenColor, bgColor);
                continue;
            }

            tile.TileRenderer.ChangeColor(oddColor, bgColor);
        }

        RefreshTileMapDraw(tileMap);
    }
}