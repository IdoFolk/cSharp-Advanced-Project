namespace TileMapEngine.CoreEngine.Rendering.Console;

public class ConsoleSceneRenderer : ISceneRenderer
{
    public void Initialize(int width, int height)
    {
        System.Console.Clear();
        System.Console.SetWindowSize(width * 10, height * 10);
        System.Console.CursorVisible = false;
    }

    public void RefreshScreen()
    {
        // Redraws the screen or flushes any buffered output
        System.Console.SetCursorPosition(0, 0);
    }
}