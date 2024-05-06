namespace TileMapEngine.CoreEngine;

public class Actor(string actorName)
{
    public string ActorName { get; set; } = actorName;
    public List<TileObject.TileObject> TileObjects { get; private set; } = [];

    public void AddTileObjects(List<TileObject.TileObject> objects) => TileObjects.AddRange(objects);
}