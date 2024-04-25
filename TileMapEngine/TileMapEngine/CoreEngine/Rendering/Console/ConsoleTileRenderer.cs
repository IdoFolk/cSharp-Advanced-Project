namespace TileMapEngine.CoreEngine.Rendering.Console;

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
        if (_drawable is not ConsoleStringDrawableObject drawableChar)
        {
            throw new Exception($"drawable on {this} is not ConsoleCharObject! Aborting.");
        }

        System.Console.SetCursorPosition(_position2D.X * 3, _position2D.Y);
        System.Console.ForegroundColor = drawableChar.ConsoleColor;
        System.Console.Write($"[{drawableChar.ConsoleString}]");
    }

    public ITileRenderer Clone()
    {
        var clone = new ConsoleTileRenderer();
        clone.Init(_drawable, _position2D);
        return clone;
    }
}