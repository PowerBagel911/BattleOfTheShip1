using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace BattleOfTheShip1.GameObject
{
    public class Bullet
    {
        private double _x;
        private double _y;
        private double _angle;
        private Bitmap _bitmap;
        private const double SPEED = 10.0;

        public Bullet(double x, double y, double angle)
        {
            _x = x;
            _y = y;
            _angle = angle;
            LoadContent();
        }

        public Bitmap Bitmap => _bitmap;
        public double X => _x;
        public double Y => _y;

        public void LoadContent()
        {
            _bitmap = SplashKit.LoadBitmap("Bullet", "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\images\\bullet_resized.png");
        }

        public void Update()
        {
            _x += SPEED * Math.Sin(_angle * (Math.PI / 180.0));
            _y -= SPEED * Math.Cos(_angle * (Math.PI / 180.0));
        }



        public void Draw()
        {
            SplashKit.DrawBitmap(_bitmap, _x, _y, SplashKit.OptionRotateBmp(_angle * (Math.PI / 180.0)));
        }

        public bool IsOutOfBounds()
        {
            return _x < 0 || _x > SplashKit.ScreenWidth() || _y < 0 || _y > SplashKit.ScreenHeight();
        }
    }
}
