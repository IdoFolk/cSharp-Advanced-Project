namespace TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

public struct ConsoleCharDrawableObject(char character, ConsoleColor consoleColor) : IDrawable
{
    public char ConsoleChar = character;
    public ConsoleColor ConsoleColor = consoleColor;
}