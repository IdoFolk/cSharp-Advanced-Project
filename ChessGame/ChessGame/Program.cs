using TileMapEngine.Engine;

namespace ChessGame;

class Program
{
    public static void Main(string[] args)
    {
        RunChessGame();
    }

    private static void RunChessGame()
    {
        ConfigScene();

        ConfigGameRules();

        ConfigPlayers();

        StartGame();
    }

    private static void ConfigScene()
    {
        // Config the scene of the game:
        // Create a new scene
        Scene gameScene = new Scene("gameScene");
        
        // create a new 8*8 board with the black and white pattern
        gameScene.CreateSquareBoard(8, true);
        // config the visuals of the board (cell size, color palette, etc.)
    }
    private static void ConfigGameRules()
    {
        // Config the game rules:
        // Create the different chess pieces types
        // determine the rules for each piece type
        // determine the game's win condition
        // determine the game's flow (turns and their logic)
    }
    private static void ConfigPlayers()
    {
        // Config the players
        // create 2 players
        // determine a name and color to each player
        // add to each players their 16 pieces
    }
    
    private static void StartGame()
    {
        // Start the game
    }
}