namespace TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

public class ConsoleTileRenderer : ITileRenderer
{
    private IDrawable _drawable;
    private Position2D _position2D;

    public void Init(IDrawable drawable, Position2D position2D)
    {
        _drawable = drawable;
        _position2D = position2D;
    }

    public void Draw()
    {
        var originalColor = Console.ForegroundColor;
        if (_drawable is not ConsoleCharDrawableObject drawableChar)
        {
            throw new Exception($"drawable on {this} is not ConsoleCharObject! Aborting.");
        }

        Console.SetCursorPosition(_position2D.X * 3, _position2D.Y);
        Console.ForegroundColor = drawableChar.ConsoleColor;
        Console.Write($"[{drawableChar.ConsoleChar}]");
        Console.ForegroundColor = originalColor;
    }

    public ITileRenderer Clone()
    {
        var clone = new ConsoleTileRenderer();
        clone.Init(_drawable, _position2D);
        return clone;
    }
}