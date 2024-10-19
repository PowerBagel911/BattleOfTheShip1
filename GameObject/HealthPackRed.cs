using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;


namespace BattleOfTheShip1.GameObject
{
    public class HealthPackRed
    {
        private Bitmap _bitmap;
        private double _x;
        private double _y;
        private double _angle;  // Attribute for rotation angle
        private const double ROTATION_SPEED = 10.0;  // Speed of rotation (degrees per frame)
        private static SplashKitSDK.Timer _spawnTimer = new SplashKitSDK.Timer("HealthPackGreenSpawnTimer");
        private static Random _random = new Random();
        static HealthPackRed()
        {
            _spawnTimer.Start();
        }


        public HealthPackRed()
        {
            _x = SplashKit.Rnd(SplashKit.ScreenWidth() - 50);  // Random position on screen
            _y = SplashKit.Rnd(SplashKit.ScreenHeight() - 50);
            _angle = 0;  // Start with 0 degrees rotation
        }

        public void LoadContent()
        {
            _bitmap = SplashKit.LoadBitmap("HealthPackRed", "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\images\\health_red_resized.png");  // Load the ammo reload image
        }

        public void Update()
        {
            _angle += ROTATION_SPEED;  // Increment the angle for rotation

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
            // Draw the bitmap with a rotation option
            SplashKit.DrawBitmap(_bitmap, _x, _y, SplashKit.OptionRotateBmp(_angle * Math.PI / 180));
        }

        public bool IsCollected(Ship ship)
        {
            if (SplashKit.BitmapCollision(ship.Bitmap, ship.X, ship.Y, _bitmap, _x, _y))
            {
                ship.RestoreHealth(2);
                return true;
            }
            return false;
        }

        public static bool ShouldSpawn()
        {
            // Spawn ammo reload every 10-20 seconds
            if (_spawnTimer.Ticks > _random.Next(10000, 20000))
            {
                _spawnTimer.Reset();
                return true;
            }
            return false;
        }
    }
}
