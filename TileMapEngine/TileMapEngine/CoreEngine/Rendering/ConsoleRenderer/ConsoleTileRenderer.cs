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
        var originalColor = Console.ForegroundColor;

        Console.ForegroundColor = _drawable.ConsoleColor;
        
        var objectDeviation = isTileObject ? -2 : 0;
        
        Console.SetCursorPosition(_position2D.X * 3 + objectDeviation, _position2D.Y);
        
        Console.Write($"{_drawable.ConsoleString}");
        
        Console.ForegroundColor = originalColor;
    }

    public ITileRenderer Clone()
    {
        var clone = new ConsoleTileRenderer();
        clone.Init(_drawable, _position2D);
        return clone;
    }

    public void ChangeColor(object color)
    {
        if (color is not ConsoleColor consoleColor)
        {
            throw new Exception($"color: {color} is not a ConsoleColor! Aborting.");
        }

        _drawable.ConsoleColor = consoleColor;
    }
}