using System.Numerics;
using Renderer.Rendering;

namespace ConsoleRenderer.ConsoleRenderer;

public class ConsoleTileRenderer : ITileRenderer
{
    private ConsoleDrawableString _drawable;
    private Vector2 _tileMapPosition2D;
    private Vector2 _screenPosition;

    private bool _isHighlighted;
    public void Init(IDrawable drawable, Vector2 position)
    {
        if (drawable is not ConsoleDrawableString drawableChar)
        {
            throw new Exception($"drawable on {this} is not ConsoleCharObject! Aborting.");
        }

        _drawable = drawableChar;
        _tileMapPosition2D = position;
    }

    public void Draw(int rowsOffset, bool isTileObject = false)
    {
        var objectDeviation = isTileObject ? 1 : 0;

        _screenPosition = new Vector2((_tileMapPosition2D.X * 3) + objectDeviation,
            _tileMapPosition2D.Y + rowsOffset );
        
        if (_isHighlighted)
        {
            RedrawWithDifferentColors(_drawable.HighlightColor);
            return;
        }
        
        DrawAtPosition(_drawable.FgConsoleColor, _drawable.BgConsoleColor);
    }

    public ITileRenderer Clone()
    {
        var clone = new ConsoleTileRenderer();
        clone.Init(_drawable, _tileMapPosition2D);
        return clone;
    }

    public void ChangeColor(object fgColor, object? bgColor = default)
    {
        if (fgColor is not ConsoleColor fgConsoleColor)
        {
            throw new Exception($"fgColor: {fgColor} is not a ConsoleColor! Aborting.");
        }
        
        if (bgColor is not ConsoleColor bgConsoleColor)
        {
            bgConsoleColor = ConsoleColor.Black; // Changing to console's default background color
        }

        _drawable.FgConsoleColor = fgConsoleColor;
        _drawable.BgConsoleColor = bgConsoleColor;
    }

    public void UpdatePosition(Vector2 newPosition)
    {
        _tileMapPosition2D = newPosition;
    }

    public void Highlight(bool redraw)
    {
        _isHighlighted = true;
        if (redraw)
        {
            RedrawWithDifferentColors(_drawable.HighlightColor);
        }
    }

    public void ResetHighlight(bool redraw)
    {
        _isHighlighted = false;
        if (redraw)
        {
            RedrawWithDifferentColors(_drawable.FgConsoleColor);
        }
    }
    
    private void RedrawWithDifferentColors(object fgColor, object? bgColor = default)
    {
        if (fgColor is not ConsoleColor fgConsoleColor)
        {
            throw new Exception($"fgColor: {fgColor} is not a ConsoleColor! Aborting.");
        }
        
        if (bgColor is not ConsoleColor bgConsoleColor)
        {
            bgConsoleColor = ConsoleColor.Black; // Changing to console's default background color
        }
        
        var originalCursorPosition = Console.GetCursorPosition();
        DrawAtPosition(fgConsoleColor, bgConsoleColor);
        Console.SetCursorPosition(originalCursorPosition.Left, originalCursorPosition.Top);
    }

    private void DrawAtPosition(ConsoleColor fgConsoleColor, ConsoleColor bgConsoleColor)
    {
        var fgOriginalColor = Console.ForegroundColor;
        var bgOriginalColor = Console.BackgroundColor;

        Console.ForegroundColor = fgConsoleColor;
        Console.BackgroundColor = bgConsoleColor;
        
        DrawAtCachedScreenPosition();
        
        Console.ForegroundColor = fgOriginalColor;
        Console.BackgroundColor = bgOriginalColor;
    }

    private void DrawAtCachedScreenPosition()
    {
        // We add 1 because of the board guides (numbers and letters)
        Console.SetCursorPosition((int)_screenPosition.X + 1, (int)_screenPosition.Y + 1);
        
        Console.Write($"{_drawable.ConsoleString}");
    }
}