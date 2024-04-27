using Renderer.Rendering;
using TileMapEngine.CoreEngine;

namespace ConsoleRenderer.ConsoleRenderer;

public class ConsoleGameRenderer : IGameRenderer<TileMap,ConsoleColor>
{

    public void InitGameRenderer(TileMap tileMap)
    {
        var tileDrawable = new ConsoleDrawableString("[ ]", ConsoleColor.White);
        var tileRenderer = new ConsoleTileRenderer();
        tileMap.AssignRendererToTiles(tileRenderer, tileDrawable);

        Console.Clear();
        //Console.SetWindowSize(width * 10, height * 10);Vector2
        Console.CursorVisible = false;

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