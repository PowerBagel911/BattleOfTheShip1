using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShip1.GameObject
{
    public class Star
    {
        private double _x;
        private double _y;
        private double _speed;
        private Color _color;

        public Star(double x, double y)
        {
            _x = x;
            _y = y;
            _speed = SplashKit.Rnd(1, 2); // Random speed between 0.5 and 2.0
            _color = Color.RGBColor(SplashKit.Rnd(256), SplashKit.Rnd(256), SplashKit.Rnd(256));
        }

        public void Update()
        {
            _y += _speed;
            if (_y > SplashKit.ScreenHeight())
            {
                _y = 0;
                _x = SplashKit.Rnd(SplashKit.ScreenWidth());
            }
        }

        public void Draw()
        {
            SplashKit.FillCircle(_color, _x, _y, 1); // Draw a small circle to represent the star
        }
    }
}
