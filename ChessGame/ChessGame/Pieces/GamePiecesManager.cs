using ConsoleRenderer.ConsoleRenderer;
using TileMapEngine.CoreEngine;

namespace ChessGame.Pieces;

public class GamePiecesManager
{
    public void Init()
    {
        // for (int i = 0; i < 8; i++)
        // {
        //     var tile = GameManager.TileMap?[i, 5];
        //     if (tile != null)
        //     {
        //         var pawn = new Pawn(new ConsoleTileRenderer(), tile, ConsoleColor.Green);
        //     }
        // }
        var tile25 = GameManager.TileMap?[2, 5];
        if (tile25 != null)
        {
            var pawn = new Pawn(new ConsoleTileRenderer(), tile25, ConsoleColor.Yellow);
        }
        
        var tile46 = GameManager.TileMap?[4, 6];
        if (tile46 != null)
        {
            var bishop = new Bishop(new ConsoleTileRenderer(), tile46, ConsoleColor.Yellow);
        }

        GameManager.RefreshGameViewport(true);
    }
}