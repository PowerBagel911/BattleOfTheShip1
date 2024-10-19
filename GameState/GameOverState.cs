using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShip1.GameState
{
     public class GameOverState : EnvironmentState
    {
        private int _score;
        private string _message;
        private string _gameOverText;
        private string _scoreText;
        private string _instructionText;

        public GameOverState(int finalScore)
        {
            _score = finalScore;
            _message = "Game Over! Your score was: " + _score;
            _scoreText = $"Final Score: {_score}";
            _gameOverText = "GAME OVER";
            _instructionText = "Press Enter to go to Main Menu, Esc to Quit";
        }

        public override void LoadContent()
        {
            base.LoadContent();
            SplashKit.LoadFont("GameOverFont", "arial.ttf");
        }

        public override void HandleInput()
        {
            if (SplashKit.KeyTyped(KeyCode.ReturnKey))
            {
                GameManager.Instance.ChangeState(new MainMenuState());
            }
            else if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                Environment.Exit(0);
            }
        }

        public override void Draw()
        {
            base.Draw();
            

            SplashKit.DrawText(_gameOverText, Color.Red, SplashKit.FontNamed("GameOverFont"), 50,
                (SplashKit.ScreenWidth() - SplashKit.TextWidth(_gameOverText, "GameOverFont", 50)) / 2, 100);
            SplashKit.DrawText(_scoreText, Color.White, SplashKit.FontNamed("GameOverFont"), 30,
                (SplashKit.ScreenWidth() - SplashKit.TextWidth(_scoreText, "GameOverFont", 30)) / 2, 200);
            SplashKit.DrawText(_instructionText, Color.White, SplashKit.FontNamed("GameOverFont"), 20,
                (SplashKit.ScreenWidth() - SplashKit.TextWidth(_instructionText, "GameOverFont", 20)) / 2, 300);
        }
    }
}
