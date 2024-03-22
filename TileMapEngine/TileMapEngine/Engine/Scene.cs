using System.Drawing;

namespace TileMapEngine.Engine;

public class Scene
{
    public string Name { get; }
    
    public Scene(string name)
    {
        Name = name;
    }
    
    public void Play()
    {
        // TODO run the simulation
    }

    public void AddSquareBoard(SquareBoardConfig boardConfig)
    {
        // TODO create the board using CoreEngine
    }

    public override string ToString()
    {
        return $"{Name}";
    }
}

public struct SquareBoardConfig
{
    public int CellSize;
    public Color OddCellsColor;
    public Color EvenCellsColor;
}