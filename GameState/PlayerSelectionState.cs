using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleOfTheShip1.GameObject;
using SplashKitSDK;

namespace BattleOfTheShip1.GameState
{
    public class PlayerSelectionState : EnvironmentState
    {
        private List<Ship> _ships;
        private int _selectedShipIndex;
        private SoundEffect _boomSound;

        public PlayerSelectionState()
        {
            _ships = new List<Ship>();
            _selectedShipIndex = 0;

            // Create three ships with different positions
            _ships.Add(new Ship(450 - 60, 400, "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\images\\ship1_logo_resized.png"));
            _ships.Add(new Ship(750 - 60, 400, "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\images\\ship2_logo_resized.png"));
            _ships.Add(new Ship(1050 - 60, 400, "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\images\\ship3_logo_resized.png"));
        }

        public override void LoadContent()
        {
            base.LoadContent();
            // Load ship content
            foreach (var ship in _ships)
            {
                ship.LoadContent();
            }

            _boomSound = SplashKit.LoadSoundEffect("boomSound", "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\sounds\\boom.mp3");
        }

        public override void HandleInput()
        {
            if (SplashKit.KeyTyped(KeyCode.LeftKey))
            {
                _selectedShipIndex = (_selectedShipIndex - 1 + _ships.Count) % _ships.Count;
            }
            else if (SplashKit.KeyTyped(KeyCode.RightKey))
            {
                _selectedShipIndex = (_selectedShipIndex + 1) % _ships.Count;
            }
            else if (SplashKit.KeyTyped(KeyCode.ReturnKey))
            {
                _boomSound.Play();
                // Change to gameplay state with the selected ship
                GameManager.Instance.ChangeState(new SinglePlayerState(_ships[_selectedShipIndex]));
            }
            else if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                Environment.Exit(0);
            }
        }

        public override void Draw()
        {
            base.Draw();
            // Draw the ship selection prompt
            SplashKit.DrawText("SELECT YOUR SHIP", Color.White, SplashKit.FontNamed("MenuFont"), 40, 950 - SplashKit.TextWidth("SELECT YOUR SHIP", "Arial", 40), 100);

            // Draw the ships
            for (int i = 0; i < _ships.Count; i++)
            {
                bool isSelected = (i == _selectedShipIndex);
                _ships[i].ChangeBorderColor(i, isSelected);
                _ships[i].Draw();
            }
        }
    }
}
