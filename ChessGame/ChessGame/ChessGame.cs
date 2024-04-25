using System.Drawing;
using TileMapEngine.CoreEngine;

namespace ChessGame;

public class ChessGame
{
    public void RunChessGame()
    {
        var gameScene = ConfigScene();

        ConfigGameRules(gameScene);

        ConfigPlayers(gameScene);

        StartGame(gameScene);
    }

    private Scene ConfigScene()
    {
        // Config the scene of the game:
        // Create a new scene
        var gameScene = new Scene("gameScene");

        // config the size (8*8) and visuals of the board (cell size, color palette, etc.)
        var boardConfig = new SquareBoardConfig
        {
            FaceSize = 8,
            CellSize = 4,
            OddCellsColor = Color.White,
            EvenCellsColor = Color.Black
        };

        // create a new 8*8 board with the black and white pattern
        gameScene.AddSquareBoard(boardConfig);

        return gameScene;
    }

    private void ConfigGameRules(Scene gameScene)
    {
        // Config the game rules:
        // Create the different chess pieces types
        // determine the rules for each piece type
        // determine the game's win condition
        // determine the game's flow (turns and their logic)
    }

    private void ConfigPlayers(Scene gameScene)
    {
        // Config the players
        // create 2 players
        // determine a name and color to each player
        // add to each players their 16 pieces
    }

    private void StartGame(Scene gameScene)
    {
        // Start the game
        gameScene.Play();
    }
}