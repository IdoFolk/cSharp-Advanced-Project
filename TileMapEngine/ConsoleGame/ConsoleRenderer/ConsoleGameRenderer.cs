using Renderer.Rendering;
using TileMapEngine.CoreEngine;

namespace ConsoleRenderer.ConsoleRenderer;

public class ConsoleGameRenderer : IGameRenderer<TileMap,ConsoleColor>
{

    public void InitGameRenderer(TileMap? tileMap)
    {
        Console.Clear();
        
        var tileDrawable = new ConsoleDrawableString("[ ]", ConsoleColor.White);
        var tileRenderer = new ConsoleTileRenderer();
        tileMap.AssignRendererToTiles(tileRenderer, tileDrawable);
        
        Console.CursorVisible = false;

        foreach (var mapTile in tileMap)
        {
            mapTile.DrawTile(0);
        }

        Console.WriteLine("\n");
    }

    public void RefreshTileMapDraw(TileMap? tileMap, bool clearConsole = false)
    {
        if (clearConsole)
        {
            Console.Clear();
        }
        
        var rowsOffset = Console.GetCursorPosition().Top;

        if (OperatingSystem.IsWindows() && Console.BufferHeight < rowsOffset + tileMap.GetHeight())
        {
            Console.SetBufferSize(Console.BufferWidth, rowsOffset + tileMap.GetHeight());
        }
        
        foreach (var mapTile in tileMap)
        {
            // if (mapTile.Position.X == 0)
            // {
            //     
            // }
            mapTile.DrawTile(rowsOffset);
        }

        Console.WriteLine();
    }

    public void AssignCheckersPattern(TileMap? tileMap, ConsoleColor oddColor, ConsoleColor evenColor,
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