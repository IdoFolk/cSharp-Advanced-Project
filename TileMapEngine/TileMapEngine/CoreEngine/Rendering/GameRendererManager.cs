using TileMapEngine.CoreEngine.Rendering.ConsoleRenderer;

namespace TileMapEngine.CoreEngine.Rendering;

public static class GameRendererManager
{
    public static IGameRenderer<TColor> GetGameRenderer<TColor>(RendererType rendererType)
    {
        return rendererType switch
        {
            RendererType.Console => (IGameRenderer<TColor>)new ConsoleGameRenderer(),
            _ => throw new ArgumentOutOfRangeException(nameof(rendererType), rendererType, null)
        };
    }
}