using Renderer.Rendering;
using TileMapEngine.CoreEngine;

namespace ConsoleRenderer.ConsoleRenderer;

public class ConsoleGameRenderer : IGameRenderer<TileMap,ConsoleColor>
{
    const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public void InitGameRenderer(TileMap? tileMap)
    {
        if (OperatingSystem.IsWindows() && Console.BufferHeight < tileMap.GetHeight() + 1)
        {
            Console.SetBufferSize(Console.BufferWidth, tileMap.GetHeight() + 1);
        }
        
        Console.Clear();

        var tileDrawable = new ConsoleDrawableString("[ ]", ConsoleColor.White);
        var tileRenderer = new ConsoleTileRenderer();
        tileMap.AssignRendererToTiles(tileRenderer, tileDrawable);
        
        Console.CursorVisible = false;

        DrawMapGuidelines(tileMap);
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

        if (OperatingSystem.IsWindows() && Console.BufferHeight < rowsOffset + tileMap.GetHeight() + 1)
        {
            Console.SetBufferSize(Console.BufferWidth, rowsOffset + tileMap.GetHeight() + 1);
        }
        
        DrawMapGuidelines(tileMap);
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

    private void DrawMapGuidelines(TileMap tileMap) 
    {
        if (tileMap.GetWidth() > letters.Length)
        {
            Console.WriteLine("tileMap length exceeds the max letter count");
            return;
        }
        for (int i = 0; i < tileMap.GetWidth(); i++)
        {
            Console.SetCursorPosition((i*3)+2,0);
            Console.Write(letters[i]);
        }
        for (int i = 0; i < tileMap.GetHeight(); i++)
        {
            Console.SetCursorPosition(0,i+1);
            Console.Write(i+1);
        }
    }
}