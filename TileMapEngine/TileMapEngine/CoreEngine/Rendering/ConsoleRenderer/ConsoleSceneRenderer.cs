namespace TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

public class ConsoleSceneRenderer : ISceneRenderer
{
    public void Initialize()
    {
        Console.Clear();
        //Console.SetWindowSize(width * 10, height * 10);
        Console.CursorVisible = false;
    }
}