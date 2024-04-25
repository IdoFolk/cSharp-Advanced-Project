namespace TileMapEngine.CoreEngine.Rendering;

public interface ISceneRenderer
{
    void Initialize(int width, int height);
    void RefreshScreen();
}