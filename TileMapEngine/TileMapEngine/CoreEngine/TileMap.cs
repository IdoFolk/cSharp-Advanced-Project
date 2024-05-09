using System.Collections;
using System.Numerics;
using Renderer.Rendering;

namespace TileMapEngine.CoreEngine;

/// <summary>
/// Represents a tile map containing a grid of tiles.
/// </summary>
public class TileMap : IEnumerable<Tile>
{
    private readonly Tile?[,] _tiles;
    public int Size => _tiles.Length;

    #region Constructors

    public TileMap(int columns, int rows)
    {
        _tiles = new Tile?[rows, columns];
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                _tiles[j, i] = new Tile(j, i);
            }
        }
    }

    public TileMap(Tile?[,] tileMatrix) => _tiles = tileMatrix;

    #endregion

    #region Public Methods

    public int GetWidth() => _tiles.GetLength(0);
    public int GetHeight() => _tiles.GetLength(1);

    public bool CheckTileExistsInPosition(Position2D position)
    {
        return position.X >= 0 && position.X < _tiles.GetLength(0) &&
               position.Y >= 0 && position.Y < _tiles.GetLength(1);
    }

    public bool CheckTileExistsInPosition(Position2D position, out Tile? tile)
    {
        if (position.X >= 0 && position.X < _tiles.GetLength(0) &&
            position.Y >= 0 && position.Y < _tiles.GetLength(1))
        {
            tile = _tiles[position.X, position.Y];
            return true;
        }

        tile = null;
        return false;
    }

    public bool CheckTileObjectInPosition(Position2D position)
    {
        return CheckTileExistsInPosition(position) && this[position].CurrentTileObject != null;
    }

    public void AssignRendererToTiles(ITileRenderer tileRenderer, IDrawable drawable)
    {
        foreach (var tile in _tiles)
        {
            var renderer = tileRenderer.Clone();
            renderer.Init(drawable, new Vector2(tile.Position.X, tile.Position.Y));
            tile.AssignRenderer(renderer);
        }
    }

    #endregion

    #region Indexers

    public Tile? this[int index]
    {
        get
        {
            if (index < 0 || index >= _tiles.Length)
                throw new IndexOutOfRangeException();
            var x = index / _tiles.GetLength(0);
            var y = index % _tiles.GetLength(0);
            return _tiles[x, y];
        }
        set
        {
            if (index < 0 || index >= _tiles.Length)
                throw new IndexOutOfRangeException();
            var x = index / _tiles.GetLength(0);
            var y = index % _tiles.GetLength(0);
            _tiles[x, y] = value;
        }
    }

    public Tile? this[int x, int y]
    {
        get => _tiles[x, y];
        set => _tiles[x, y] = value;
    }

    public Tile? this[Position2D position]
    {
        get => _tiles[position.X, position.Y];
        set => _tiles[position.X, position.Y] = value;
    }

    #endregion

    #region IEnumerable

    public IEnumerator<Tile> GetEnumerator()
    {
        return new TileMapEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion
}

internal struct TileMapEnumerator : IEnumerator<Tile>
{
    private readonly TileMap _tileMap;
    private int _index;
    private bool _indexInRange => _index >= 0 && _index < _tileMap.Size;
    public Tile Current => _indexInRange ? _tileMap[_index] : null;
    object IEnumerator.Current => Current;

    public TileMapEnumerator(TileMap tileMap)
    {
        _tileMap = tileMap;
        _index = -1;
    }

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
        _index++;
        return _indexInRange;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }
}