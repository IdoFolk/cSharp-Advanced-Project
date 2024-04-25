namespace TileMapEngine.CoreEngine.Rendering;

public interface ITileRenderer
{
    public void Init(IDrawable drawable, Position2D position2D);
    public void Draw(bool isTileObject = false);
    public ITileRenderer Clone();
    void ChangeColor(object color);
}

public interface IDrawable
{
}