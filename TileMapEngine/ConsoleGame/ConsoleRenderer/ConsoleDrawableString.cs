using Renderer.Rendering;

namespace ConsoleRenderer.ConsoleRenderer;

public struct ConsoleDrawableString(
    string str,
    ConsoleColor fgConsoleColor,
    ConsoleColor bgConsoleColor = ConsoleColor.Black,
    ConsoleColor highlightColor = ConsoleColor.Green) : IDrawable
{
    public readonly string ConsoleString = str;
    public ConsoleColor FgConsoleColor = fgConsoleColor;
    public ConsoleColor BgConsoleColor = bgConsoleColor;
    public ConsoleColor HighlightColor = highlightColor;
}