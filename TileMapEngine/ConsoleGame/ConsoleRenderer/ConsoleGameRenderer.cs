using Renderer.Rendering;
using TileMapEngine.CoreEngine;

namespace ConsoleRenderer.ConsoleRenderer;

public class ConsoleGameRenderer : IGameRenderer<TileMap, ConsoleColor>
{
    private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public void InitGameRenderer(TileMap? tileMap)
    {
        if (OperatingSystem.IsWindows() && Console.BufferHeight < tileMap?.GetHeight() + 1)
        {
            if (tileMap != null) Console.SetBufferSize(Console.BufferWidth, tileMap.GetHeight() + 1);
        }

        Console.Clear();

        var tileDrawable = new ConsoleDrawableString("[ ]", ConsoleColor.White);
        var tileRenderer = new ConsoleTileRenderer();
        tileMap?.AssignRendererToTiles(tileRenderer, tileDrawable);

        Console.CursorVisible = false;

        if (tileMap == null) return;

        DrawMapGuidelines(tileMap, 0);
        foreach (var mapTile in tileMap)
        {
            mapTile.DrawTile();
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

        if (OperatingSystem.IsWindows() && Console.BufferHeight < rowsOffset + tileMap?.GetHeight() + 1)
        {
            if (tileMap != null) Console.SetBufferSize(Console.BufferWidth, rowsOffset + tileMap.GetHeight() + 1);
        }

        if (tileMap == null)
        {
            return;
        }

        DrawMapGuidelines(tileMap, rowsOffset);
        foreach (var mapTile in tileMap)
        {
            mapTile.DrawTile(rowsOffset);
        }

        Console.WriteLine();
    }

    public void AssignCheckersPattern(TileMap? tileMap, ConsoleColor oddColor, ConsoleColor evenColor,
        ConsoleColor bgColor = default)
    {
        if (tileMap == null) return;

        foreach (var tile in tileMap)
        {
            if ((tile.Position.X + tile.Position.Y) % 2 == 0)
            {
                tile.TileRenderer.ChangeColor(evenColor, bgColor);
                continue;
            }

            tile.TileRenderer.ChangeColor(oddColor, bgColor);
        }

        RefreshTileMapDraw(tileMap, true);
    }

    private static void DrawMapGuidelines(TileMap tileMap, int rowsOffset)
    {
        if (tileMap == null)
        {
            throw new Exception("Tile map is null.");
        }

        if (tileMap.GetWidth() > Letters.Length)
        {
            throw new Exception("The provided tileMap length exceeds the max letter count.");
        }

        for (var i = 0; i < tileMap.GetWidth(); i++)
        {
            // We add 2 because we want an offset of 1 from the leftmost column, and another 1 to draw it above
            // the tile object (in the center of the tile)
            Console.SetCursorPosition((i * 3) + 2, rowsOffset);

            Console.Write(Letters[i]);
        }

        for (var i = 0; i < tileMap.GetHeight(); i++)
        {
            // We add 1 so it gets above the tilemap
            Console.SetCursorPosition(0, rowsOffset + i + 1);

            Console.Write(i + 1);
        }
    }
}