namespace TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

public class ConsoleTileRenderer : ITileRenderer
{
    private ConsoleCharDrawableObject _drawable;
    private Position2D _position2D;

    public void Init(IDrawable drawable, Position2D position2D)
    {
        // _drawable = (ConsoleCharDrawableObject)drawable;
        if (drawable is not ConsoleCharDrawableObject drawableChar)
        {
            throw new Exception($"drawable on {this} is not ConsoleCharObject! Aborting.");
        }

        _drawable = drawableChar;
        _position2D = position2D;
    }

    public void Draw()
    {
        var originalColor = Console.ForegroundColor;

        Console.SetCursorPosition(_position2D.X * 3, _position2D.Y);
        Console.ForegroundColor = _drawable.ConsoleColor;
        Console.Write($"[{_drawable.ConsoleChar}]");
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