using ConsoleRenderer.ConsoleRenderer;
using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace ChessGame.Pieces;

public class GamePiecesManager
{
    public List<TileObject> CreateAndGetWhitePlayerPieces()
    {
        var pieces = new List<TileObject>();
        for (var i = 0; i < 8; i++)
        {
            var tile = GameManager.TileMap?[i, 6];
            if (tile != null)
            {
                pieces.Add(new Pawn(new ConsoleTileRenderer(), tile, ConsoleColor.White));
            }
        }
        
        var tile07 = GameManager.TileMap?[0, 7];
        if (tile07 != null)
        {
            pieces.Add(new Rook(new ConsoleTileRenderer(), tile07, ConsoleColor.White));
        }
        
        var tile17 = GameManager.TileMap?[1, 7];
        if (tile17 != null)
        {
            pieces.Add(new Knight(new ConsoleTileRenderer(), tile17, ConsoleColor.White));
        }
        
        var tile27 = GameManager.TileMap?[2, 7];
        if (tile27 != null)
        {
            pieces.Add(new Bishop(new ConsoleTileRenderer(), tile27, ConsoleColor.White));
        }
        
        var tile37 = GameManager.TileMap?[3, 7];
        if (tile37 != null)
        {
            pieces.Add(new Queen(new ConsoleTileRenderer(), tile37, ConsoleColor.White));
        }
        
        var tile47 = GameManager.TileMap?[4, 7];
        if (tile47 != null)
        {
            pieces.Add(new King(new ConsoleTileRenderer(), tile47, ConsoleColor.White));
        }
        
        var tile57 = GameManager.TileMap?[5, 7];
        if (tile57 != null)
        {
            pieces.Add(new Bishop(new ConsoleTileRenderer(), tile57, ConsoleColor.White));
        }
        
        var tile67 = GameManager.TileMap?[6, 7];
        if (tile67 != null)
        {
            pieces.Add(new Knight(new ConsoleTileRenderer(), tile67, ConsoleColor.White));
        }
        
        var tile77 = GameManager.TileMap?[7, 7];
        if (tile77 != null)
        {
            pieces.Add(new Rook(new ConsoleTileRenderer(), tile77, ConsoleColor.White));
        }

        return pieces;
    }
    
    public List<TileObject> CreateAndGetBlackPlayerPieces()
    {
        var pieces = new List<TileObject>();
        for (var i = 0; i < 8; i++)
        {
            var tile = GameManager.TileMap?[i, 1];
            if (tile != null)
            {
                pieces.Add(new Pawn(new ConsoleTileRenderer(), tile, ConsoleColor.DarkCyan));
            }
        }
        
        var tile00 = GameManager.TileMap?[0, 0];
        if (tile00 != null)
        {
            pieces.Add(new Rook(new ConsoleTileRenderer(), tile00, ConsoleColor.DarkCyan));
        }
        
        var tile10 = GameManager.TileMap?[1, 0];
        if (tile10 != null)
        {
            pieces.Add(new Knight(new ConsoleTileRenderer(), tile10, ConsoleColor.DarkCyan));
        }
        
        var tile20 = GameManager.TileMap?[2, 0];
        if (tile20 != null)
        {
            pieces.Add(new Bishop(new ConsoleTileRenderer(), tile20, ConsoleColor.DarkCyan));
        }
        
        var tile30 = GameManager.TileMap?[3, 0];
        if (tile30 != null)
        {
            pieces.Add(new Queen(new ConsoleTileRenderer(), tile30, ConsoleColor.DarkCyan));
        }
        
        var tile40 = GameManager.TileMap?[4, 0];
        if (tile40 != null)
        {
            pieces.Add(new King(new ConsoleTileRenderer(), tile40, ConsoleColor.DarkCyan));
        }
        
        var tile50 = GameManager.TileMap?[5, 0];
        if (tile50 != null)
        {
            pieces.Add(new Bishop(new ConsoleTileRenderer(), tile50, ConsoleColor.DarkCyan));
        }
        
        var tile60 = GameManager.TileMap?[6, 0];
        if (tile60 != null)
        {
            pieces.Add(new Knight(new ConsoleTileRenderer(), tile60, ConsoleColor.DarkCyan));
        }
        
        var tile70 = GameManager.TileMap?[7, 0];
        if (tile70 != null)
        {
            pieces.Add(new Rook(new ConsoleTileRenderer(), tile70, ConsoleColor.DarkCyan));
        }

        return pieces;
    }
}