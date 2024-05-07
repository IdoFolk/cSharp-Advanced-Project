using TileMapEngine.CoreEngine.Objects;

namespace TileMapEngine.CoreEngine;

public class Actor(string actorName)
{
    public string ActorName { get; set; } = actorName;
    public List<TileObject> TileObjects { get; protected set; } = [];

    public void AddTileObjects(List<TileObject> objects) => TileObjects.AddRange(objects);

    public void RemoveObject(TileObject tileObject) => TileObjects.Remove(tileObject);
}