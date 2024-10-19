using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShip1.GameState
{
    // Interface for the game state - state design pattern
    public interface IGameState
    {
        void Initialize();
        void Update();
        void Draw();
        void HandleInput();
        void LoadContent();
        void UnloadContent();
    }
}
