namespace TileMapEngine.CoreEngine;

public interface ICommandHandler
{
    public void Select();
    public void Deselect();
    public void Move();
}