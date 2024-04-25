using System.Collections;

namespace TileMapEngine;

public class TileMap : IEnumerable<Tile>
{
    public readonly Tile[,] Tiles;

    #region Constructors

    public TileMap(int columns, int rows)
    {
        Tiles = new Tile[rows,columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Tiles[j,i] = new Tile(j,i);
            }
        }
    }
    public TileMap(Tile[,] tileMatrix) => Tiles = tileMatrix;

    #endregion

    #region Public Methods

    public void SetTileObjects()
    {
        
    }
    
    #endregion
    
    #region Indexers

    public Tile this[int index]
    {
        get
        {
            if (index < 0 || index >= Tiles.Length)
                throw new IndexOutOfRangeException();
            var x = index / Tiles.GetLength(0);
            var y = index % Tiles.GetLength(0);
            return Tiles[y,x];
        }
        set
        {
            if (index < 0 || index >= Tiles.Length)
                throw new IndexOutOfRangeException();
            var x = index / Tiles.GetLength(0);
            var y = index % Tiles.GetLength(0);
            Tiles[y,x] = value;
        }
    }

    public Tile this[int x,int y]
    {
        get => Tiles[y,x];
        set => Tiles[y,x] = value;
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
    private bool _indexInRange => _index >= 0 && _index < _tileMap.Tiles.Length;
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