using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace BattleOfTheShip1.GameObject
{
    public class Heart
    {
        private Bitmap _bitmap;
        private double _x;
        private double _y;
        

        public Heart(double x, double y)
        {
            _x = x;
            _y = y;   
        }

        public void LoadContent()
        {
            _bitmap = SplashKit.LoadBitmap("Heart", "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\images\\heart.png");
        }

        public void Draw()
        {
            SplashKit.DrawBitmap(_bitmap, _x, _y, SplashKit.OptionScaleBmp(0.25, 0.25));
        }
    }
}

