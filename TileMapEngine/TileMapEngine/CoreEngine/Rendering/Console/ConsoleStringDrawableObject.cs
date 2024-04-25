namespace TileMapEngine.CoreEngine.Rendering.Console;

public class ConsoleStringDrawableObject(string str, ConsoleColor consoleColor) : IDrawable
{
    public string ConsoleString = str;
    public ConsoleColor ConsoleColor = consoleColor;
}