using ConsoleRenderer.ConsoleRenderer;
using TileMapEngine.CoreEngine;

namespace ChessGame.Pieces;

public class GamePiecesManager
{
    public void Init()
    {
        for (int i = 0; i < 8; i++)
        {
            var tile = GameManager.TileMap?[i, 6];
            if (tile != null)
            {
                new Pawn(new ConsoleTileRenderer(), tile, ConsoleColor.Yellow);
            }
        }
        
        var tile07 = GameManager.TileMap?[0, 7];
        if (tile07 != null)
        {
            new Rook(new ConsoleTileRenderer(), tile07, ConsoleColor.Yellow);
        }
        
        var tile17 = GameManager.TileMap?[1, 7];
        if (tile17 != null)
        {
            new Knight(new ConsoleTileRenderer(), tile17, ConsoleColor.Yellow);
        }
        
        var tile27 = GameManager.TileMap?[2, 7];
        if (tile27 != null)
        {
            new Bishop(new ConsoleTileRenderer(), tile27, ConsoleColor.Yellow);
        }
        
        var tile37 = GameManager.TileMap?[3, 7];
        if (tile37 != null)
        {
            new Queen(new ConsoleTileRenderer(), tile37, ConsoleColor.DarkRed);
        }
        
        var tile47 = GameManager.TileMap?[4, 7];
        if (tile47 != null)
        {
            new King(new ConsoleTileRenderer(), tile47, ConsoleColor.Red);
        }
        
        var tile57 = GameManager.TileMap?[5, 7];
        if (tile57 != null)
        {
            new Bishop(new ConsoleTileRenderer(), tile57, ConsoleColor.Yellow);
        }
        
        var tile67 = GameManager.TileMap?[6, 7];
        if (tile67 != null)
        {
            new Knight(new ConsoleTileRenderer(), tile67, ConsoleColor.Yellow);
        }
        
        var tile77 = GameManager.TileMap?[7, 7];
        if (tile77 != null)
        {
            new Rook(new ConsoleTileRenderer(), tile77, ConsoleColor.Yellow);
        }

        GameManager.RefreshGameViewport(true);
    }
}