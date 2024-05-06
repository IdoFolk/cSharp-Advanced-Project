using TileMapEngine.CoreEngine.Objects;

namespace TileMapEngine.CoreEngine;

public class Actor(string actorName)
{
    public string ActorName { get; set; } = actorName;
    public List<TileObject> TileObjects { get; private set; } = [];

    public void AddTileObjects(List<TileObject> objects) => TileObjects.AddRange(objects);
}