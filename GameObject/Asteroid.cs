using System;
using SplashKitSDK;

namespace BattleOfTheShip1.GameObject
{
    public class Asteroid
    {
        private double _x;
        private double _y;
        private double _angle;
        private Bitmap _bitmap;
        private const double SPEED = 4.0;
        private static SplashKitSDK.Timer _spawnTimer = new SplashKitSDK.Timer("AsteroidSpawnTimer");
        private static Random _random = new Random();

        public Bitmap Bitmap => _bitmap;
        public double X => _x;
        public double Y => _y;

        static Asteroid()
        {
            _spawnTimer.Start();
        }

        public Asteroid()
        {
            _x = SplashKit.Rnd(SplashKit.ScreenWidth());
            _y = SplashKit.Rnd(SplashKit.ScreenHeight());
            _angle = SplashKit.Rnd(360);
            LoadContent();
        }

        public static bool ShouldSpawn()
        {
            if (_spawnTimer.Ticks > _random.Next(2000, 4000))
            {
                _spawnTimer.Reset();
                return true;
            }
            return false;
        }

        public void LoadContent()
        {
            _bitmap = SplashKit.LoadBitmap("Asteroid", "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\images\\asteroid.png");
        }

        public void Update()
        {
            _x += SPEED * Math.Cos(_angle * (Math.PI / 180.0));
            _y += SPEED * Math.Sin(_angle * (Math.PI / 180.0));

            // Wrap around screen edges
            if (_x < 0)
            {
                _x = SplashKit.ScreenWidth();
            }
            else if (_x > SplashKit.ScreenWidth())
            {
                _x = 0;
            }

            if (_y < 0)
            {
                _y = SplashKit.ScreenHeight();
            }
            else if (_y > SplashKit.ScreenHeight())
            {
                _y = 0;
            }
        }

        public void Draw()
        {
            SplashKit.DrawBitmap(_bitmap, _x, _y, SplashKit.OptionRotateBmp(_angle * Math.PI/180.0));
        }

        public bool IsOutOfBounds()
        {
            return _x < 0 || _x > SplashKit.ScreenWidth() || _y < 0 || _y > SplashKit.ScreenHeight();
        }
    }
}
