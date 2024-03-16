using System.Collections;

namespace TileMapEngine;

public class TileMap : IEnumerable<Tile>
{
    public List<Tile> Tiles { get; private set; }

    public TileMap(List<Tile> tiles)
    {
        tiles.Sort();
        Tiles = tiles;
    }

    #region IEnumerable

    public IEnumerator<Tile> GetEnumerator()
    {
        return new TileMapEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion
}

internal struct TileMapEnumerator : IEnumerator<Tile>
{
    public void Dispose()
    {
        // TODO release managed resources here
    }

    public bool MoveNext()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public Tile Current { get; }

    object IEnumerator.Current => Current;
}