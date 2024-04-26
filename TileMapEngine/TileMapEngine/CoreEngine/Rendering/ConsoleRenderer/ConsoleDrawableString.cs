namespace TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

public struct ConsoleDrawableString(string str, ConsoleColor fgConsoleColor, ConsoleColor bgConsoleColor = ConsoleColor.Black) : IDrawable
{
    public readonly string ConsoleString = str;
    public ConsoleColor FgConsoleColor = fgConsoleColor;
    public ConsoleColor BgConsoleColor = bgConsoleColor;
}