namespace TileMapEngine.Engine;

public class Scene
{
    public string Name { get; set; }
    
    public Scene(string name)
    {
        Name = name;
    }

    public void CreateSquareBoard(int boardSize, bool isBlackWhitePattern)
    {
        throw new NotImplementedException();
    }
}