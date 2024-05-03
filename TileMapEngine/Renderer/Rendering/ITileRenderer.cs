using System.Numerics;

namespace Renderer.Rendering;

public interface ITileRenderer
{
    public void Init(IDrawable drawable, Vector2 position2D);
    public void Draw(int rowsOffset, bool isTileObject = false);
    public ITileRenderer Clone();
    public void ChangeColor(object fgColor, object? bgColor = default);
    public void UpdatePosition(Vector2 newPosition);
    public void Highlight(bool redraw = true);
    public void ResetHighlight(bool redraw = true);
}

public interface IDrawable
{
}