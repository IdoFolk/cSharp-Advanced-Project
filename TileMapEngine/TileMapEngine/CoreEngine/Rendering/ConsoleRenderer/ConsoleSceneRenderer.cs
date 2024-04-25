namespace TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

public class ConsoleSceneRenderer : ISceneRenderer
{
    public void Initialize(int width, int height)
    {
        Console.Clear();
        Console.SetWindowSize(width * 10, height * 10);
        Console.CursorVisible = false;
    }

    public void RefreshScreen()
    {
        // Redraws the screen or flushes any buffered output
        Console.SetCursorPosition(0, 0);
    }
}