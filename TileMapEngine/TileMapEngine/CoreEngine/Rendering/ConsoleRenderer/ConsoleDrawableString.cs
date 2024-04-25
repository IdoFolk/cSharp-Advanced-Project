namespace TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

public struct ConsoleDrawableString(string str, ConsoleColor consoleColor) : IDrawable
{
    public string ConsoleString = str;
    public ConsoleColor ConsoleColor = consoleColor;
}