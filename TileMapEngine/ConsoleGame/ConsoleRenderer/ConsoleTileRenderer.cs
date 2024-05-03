using System.Numerics;
using Renderer.Rendering;

namespace ConsoleRenderer.ConsoleRenderer;

public class ConsoleTileRenderer : ITileRenderer
{
    private ConsoleDrawableString _drawable;
    private Vector2 _position2D;

    public void Init(IDrawable drawable, Vector2 position)
    {
        if (drawable is not ConsoleDrawableString drawableChar)
        {
            throw new Exception($"drawable on {this} is not ConsoleCharObject! Aborting.");
        }

        _drawable = drawableChar;
        _position2D = position;
    }

    public void Draw(int rowsOffset, bool isTileObject = false)
    {
        var fgOriginalColor = Console.ForegroundColor;
        var bgOriginalColor = Console.BackgroundColor;

        Console.ForegroundColor = _drawable.FgConsoleColor;
        Console.BackgroundColor = _drawable.BgConsoleColor;
        
        var objectDeviation = isTileObject ? 1 : 0;
        
        Console.SetCursorPosition((int)_position2D.X * 3 + objectDeviation, (int)_position2D.Y + rowsOffset);
        
        Console.Write($"{_drawable.ConsoleString}");
        
        Console.ForegroundColor = fgOriginalColor;
        Console.BackgroundColor = bgOriginalColor;
    }

    public void Highlight(object color)
    {
        
    }

    public ITileRenderer Clone()
    {
        var clone = new ConsoleTileRenderer();
        clone.Init(_drawable, _position2D);
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
        _position2D = newPosition;
    }
}