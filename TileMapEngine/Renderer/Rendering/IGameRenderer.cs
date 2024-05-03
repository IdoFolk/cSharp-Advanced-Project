using System.Collections;

namespace Renderer.Rendering;

public interface IGameRenderer<TEnumerable,TColor> where TEnumerable : IEnumerable
{
    public void InitGameRenderer(TEnumerable tileMap);
    public void RefreshTileMapDraw(TEnumerable tileMap, bool clearCurrentDraw);
    public void AssignCheckersPattern(TEnumerable tileMap, TColor oddColor, TColor evenColor, TColor? bgColor = default);
}

public enum RendererType
{
    Console
}