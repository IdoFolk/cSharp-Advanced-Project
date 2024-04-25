namespace TileMapEngine.CoreEngine.Rendering;

public interface ITileRenderer
{
    public void Init(IDrawable drawable, Position2D position2D);
    public void Draw();
    public ITileRenderer Clone();
}

public interface IDrawable
{
}