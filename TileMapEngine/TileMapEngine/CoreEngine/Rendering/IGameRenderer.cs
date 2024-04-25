namespace TileMapEngine.CoreEngine.Rendering;

public interface IGameRenderer<in TColor>
{
    public void InitGameRenderer(TileMap tileMap);
    public void RefreshTileMapDraw(TileMap tileMap);
    public void AssignCheckersPattern(TileMap tileMap, TColor oddColor, TColor evenColor);
}

public enum RendererType
{
    Console
}