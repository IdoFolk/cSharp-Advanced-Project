using System.Numerics;

namespace Renderer.Rendering;

public interface ITileRenderer
{
    public void Init(IDrawable drawable, Vector2 position2D);
    public void Draw(bool isTileObject = false);
    public ITileRenderer Clone();
    void ChangeColor(object fgColor, object? bgColor = default);
}

public interface IDrawable
{
}