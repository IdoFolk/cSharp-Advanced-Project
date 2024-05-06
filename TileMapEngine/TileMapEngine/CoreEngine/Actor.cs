namespace TileMapEngine.CoreEngine;

public class Actor(List<TileObject.TileObject> tileObjects)
{
    public List<TileObject.TileObject> TileObjects { get; } = tileObjects;
}