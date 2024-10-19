using SplashKitSDK;
using System;
using BattleOfTheShip1.GameState;


namespace BattleOfTheShip1
{
    public class Program
    {
        public static void Main()
        {
            // Create the window
            Window gameWindow = new Window("Battle of the Ships V2", 1500, 1000);

            // Load the default font
            SplashKit.LoadFont("Arial", "arial.ttf");


            // Initialize GameManager and set the initial state
            GameManager gameManager = GameManager.Instance;
            gameManager.ChangeState(new MainMenuState());

            // Run the game loop
            gameManager.Run();
        }
    }

}
