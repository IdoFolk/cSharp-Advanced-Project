namespace TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

public class ConsoleTileRenderer : ITileRenderer
{
    private ConsoleDrawableString _drawable;
    private Position2D _position2D;

    public void Init(IDrawable drawable, Position2D position2D)
    {
        if (drawable is not ConsoleDrawableString drawableChar)
        {
            throw new Exception($"drawable on {this} is not ConsoleCharObject! Aborting.");
        }

        _drawable = drawableChar;
        _position2D = position2D;
    }

    public void Draw(bool isTileObject = false)
    {
        var fgOriginalColor = Console.ForegroundColor;
        var bgOriginalColor = Console.BackgroundColor;

        Console.ForegroundColor = _drawable.FgConsoleColor;
        Console.BackgroundColor = _drawable.BgConsoleColor;
        
        var objectDeviation = isTileObject ? -2 : 0;
        
        Console.SetCursorPosition(_position2D.X * 3 + objectDeviation, _position2D.Y);
        
        Console.Write($"{_drawable.ConsoleString}");
        
        Console.ForegroundColor = fgOriginalColor;
        Console.BackgroundColor = bgOriginalColor;
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
}