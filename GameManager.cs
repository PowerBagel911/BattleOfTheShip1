using BattleOfTheShip1.GameState;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShip1
{
    public class GameManager
    {
        private static GameManager _instance;
        private IGameState _currentState;

        private GameManager()
        {
        }

        // Singleton pattern
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        public void ChangeState(IGameState newState)
        {
            if (_currentState != null)
            {
                _currentState.UnloadContent();
            }
            _currentState = newState;
            _currentState.Initialize();
            _currentState.LoadContent();
        }

        // Run method to manage the game loop
        public void Run()
        {
            // Main game loop
            while (true)
            {
                SplashKit.ProcessEvents();
                _currentState.HandleInput();
                SplashKit.ClearScreen(Color.Black);
                _currentState.LoadContent();
                _currentState.Update();
                _currentState.Draw();
                SplashKit.RefreshScreen(60);
            }
        }
    }
}
