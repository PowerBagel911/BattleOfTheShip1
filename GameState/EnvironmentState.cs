using BattleOfTheShip1.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace BattleOfTheShip1.GameState
{
    public abstract class EnvironmentState : IGameState
    {
        private List<Star> _stars;
        private int _starCount = 200;

        private SoundEffect _hitSound;
        private SoundEffect _laserSound;
        private SoundEffect _reloadSound;
        private SoundEffect _boomSound;
        private SoundEffect _emptySound;
        private Music _backgroundMusic;
        private SoundEffect _rizzSound;
        private SoundEffect _healSound;

        public SoundEffect HitSound => _hitSound;
        public SoundEffect LaserSound => _laserSound;
        public SoundEffect ReloadSound => _reloadSound;
        public SoundEffect BoomSound => _boomSound;
        public SoundEffect EmptySound => _emptySound;
        public Music BackgroundMusic => _backgroundMusic;
        public SoundEffect RizzSound => _rizzSound;
        public SoundEffect HealSound => _healSound;

        public EnvironmentState()
        {
            _stars = new List<Star>();
            for (int i = 0; i < _starCount; i++)
            {
                double x = SplashKit.Rnd(SplashKit.ScreenWidth());
                double y = SplashKit.Rnd(SplashKit.ScreenHeight());
                _stars.Add(new Star(x, y));
            }
        }
        public virtual void Initialize()
        {

        }
        public virtual void Update()
        {
            foreach (Star star in _stars)
            {
                star.Update();
            }
        }
        public virtual void Draw()
        {
            foreach (Star star in _stars)
            {
                star.Draw();
            }
        }
        public abstract void HandleInput();
        public virtual void LoadContent()
        {
            _hitSound = SplashKit.LoadSoundEffect("HitSound", "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\sounds\\hit.mp3");
            _laserSound = SplashKit.LoadSoundEffect("LaserSound", "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\sounds\\laser.mp3");
            
            _reloadSound = SplashKit.LoadSoundEffect("ReloadSound", "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\sounds\\reload.mp3");
            _boomSound = SplashKit.LoadSoundEffect("BoomSound", "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\sounds\\boom.mp3");
            _emptySound = SplashKit.LoadSoundEffect("EmptySound", "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\sounds\\empty.mp3");
            _rizzSound = SplashKit.LoadSoundEffect("rizzSound", "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\sounds\\rizz.mp3");
            _healSound = SplashKit.LoadSoundEffect("healSound", "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\sounds\\heal.mp3");  


        }
        public virtual void UnloadContent()
        {
        }
    }
}
