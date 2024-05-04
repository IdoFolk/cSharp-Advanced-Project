using System.Numerics;
using ConsoleRenderer.ConsoleRenderer;
using Renderer.Rendering;
using TileMapEngine.CoreEngine;
using TileMapEngine.CoreEngine.TileObject;

namespace ChessGame.Pieces;

public class GamePiecesManager
{
    public void Init()
    {
        for (int i = 0; i < 8; i++)
        {
            var tile = GameManager.TileMap?[i, 5];
            if (tile != null)
            {
                var pawn = new Pawn(new ConsoleTileRenderer(), tile, ConsoleColor.Green);
            }
        }

        GameManager.RefreshGameViewport(true);
    }
}