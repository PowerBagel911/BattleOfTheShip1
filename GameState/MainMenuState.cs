using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShip1.GameState
{
    public class MainMenuState : EnvironmentState
    {
        private List<string> _menuItems;
        private int _selectedIndex;
        

        public MainMenuState()
        {
            _menuItems = new List<string>
            {
                "Single Player",
                "Multiplayer",
                "Exit"
            };
            _selectedIndex = 0;
        }

        

        public override void Initialize()
        {
            base.Initialize();
            // Additional menu-specific initialization if needed
        }

        public override void LoadContent()
        {
            base.LoadContent();
            // Load any specific content, such as fonts
            SplashKit.LoadFont("MenuFont", "arial.ttf");

          
           
        }

        public override void HandleInput()
        {
            if (SplashKit.KeyTyped(KeyCode.DownKey))
            {
                _selectedIndex = (_selectedIndex + 1) % _menuItems.Count;
            }
            else if (SplashKit.KeyTyped(KeyCode.UpKey))
            {
                _selectedIndex = (_selectedIndex - 1 + _menuItems.Count) % _menuItems.Count;
            }
            else if (SplashKit.KeyTyped(KeyCode.ReturnKey))
            {
                RizzSound.Play();
                switch (_selectedIndex)
                {
                    case 0:
                        GameManager.Instance.ChangeState(new PlayerSelectionState());
                        Console.WriteLine("Single Player");
                        break;
                    case 1:
                        //GameManager.Instance.ChangeState(new MultiplayerState());
                        Console.WriteLine("Multiplayer");
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            // Draw the menu items

            SplashKit.DrawText("BATTLE OF THE SHIP V2", Color.White, "Arial", 60, (SplashKit.ScreenWidth() - SplashKit.TextWidth("BATTLE OF THE SHIPS V2", "Arial", 60)) / 2, 100);
            for (int i = 0; i < _menuItems.Count; i++)
            {
                Color textColor = i == _selectedIndex ? Color.Yellow : Color.White;
                SplashKit.DrawText(_menuItems[i], textColor, "Arial", 48, (SplashKit.ScreenWidth() - SplashKit.TextWidth(_menuItems[i], "Arial", 48)) / 2, 300 + i * 50);
            }
        }





        public override void UnloadContent()
        {
        }
    }
}
