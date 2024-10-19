using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShip1.GameObject
{
    public class Ship
    {
        private double _x;
        private double _y;
        private double _xVelocity;
        private double _yVelocity;
        private Bitmap _bitmap;
        private string _imagePath;
        private int _selectedIndex;
        private bool _isSelected = false;
        private const double ScaleFactor = 4.0;
        private double _angle = 0;
        private const double ACCELERATION = 0.1;
        private const double MAX_SPEED = 5.0;
        private int MaxHealth = 5;

        // Health Atrributes
        private int _health;
        private Bitmap _heartImage;
        private List<Point2D> _heartPositions;
        private int _ammo;

        public double X => _x;
        public double Y => _y;
        public Bitmap Bitmap => _bitmap;
        public int Ammo => _ammo;

        public double Width => _bitmap.Width;
        public double Height => _bitmap.Height;
        public string ImagePath => _imagePath;
        public int Health => _health;

        //public bool IsDestroyed
        //{
        //    get
        //    {
        //        return _health <= 0;
        //    }
        //}

        public void RestoreFullHealth()
        {
            _health = MaxHealth;
        }

        public void RestoreHealth(int amount)
        {
            _health = Math.Min(_health + amount, MaxHealth);
        }

        public Ship(double x, double y, string imagePath)
        {
            _x = x;
            _y = y;
            _imagePath = imagePath;
            _xVelocity = 0;
            _yVelocity = 0;
            _health = MaxHealth;
            _ammo = 20;

        }

        

        public void LoadContent()
        {
            _bitmap = SplashKit.LoadBitmap(_imagePath, _imagePath);
            _heartImage = SplashKit.LoadBitmap("Heart", "heart.png");
        }

        private void DrawHealthBar()
        {
            // Draw only the hearts corresponding to current health
            for (int i = 0; i < _health; i++)
            {
                SplashKit.DrawBitmap(_heartImage, _heartPositions[i].X, _heartPositions[i].Y);
            }
        }

        public void TakeDamage()
        {
            if (_health > 0)
            {
                _health--;  // Decrease health by one
            }
        }

        public void Draw()
        {
            SplashKit.DrawBitmap(_bitmap, _x, _y, SplashKit.OptionRotateBmp(_angle));
            if (_isSelected)
            {
                SplashKit.DrawRectangle(
                    Color.Red,
                    _x - _bitmap.Width/2 + 80,
                    _y - _bitmap.Height/2 + 90,
                    _bitmap.Width + 20,
                    _bitmap.Height + 20);
            }

        }

        public void Update()
        {
            // Update position based on velocity
            _x += _xVelocity;
            _y += _yVelocity;

            // Keep the ship within screen bounds
            if (_x < -_bitmap.Width)
            {
                _x = SplashKit.ScreenWidth() + _bitmap.Width;
            }
            else if (_x > SplashKit.ScreenWidth() + _bitmap.Width)
            {
                _x = -_bitmap.Width;
            }

            if (_y < -_bitmap.Height)
            {
                _y = SplashKit.ScreenHeight() + _bitmap.Height;
            }
            else if (_y > SplashKit.ScreenHeight() + _bitmap.Height)
            {
                _y = -_bitmap.Height;
            }
        }

        public Bullet Shoot()
        {
            // Calculate the center of the ship
            double centerX = _x + _bitmap.Width / 2;
            double centerY = _y + _bitmap.Height / 2;

            // Calculate the offset to the tip of the ship's nose
            double noseOffset = _bitmap.Height / 2;

            double bulletX = centerX + noseOffset * Math.Sin(_angle * (Math.PI / 180.0)) - 10;
            double bulletY = centerY - noseOffset * Math.Cos(_angle * (Math.PI / 180.0)) - 10;

            if (_ammo > 0)
            {
                _ammo--;  // Decrease ammo by one
                return new Bullet(bulletX, bulletY, _angle);
            }
            return null;
        }

        public void ReloadAmmo()
        {
            _ammo = 20;  // Restore ammo to full
        }

        public void MoveForward()
        {
            _xVelocity += ACCELERATION * Math.Sin(_angle * (Math.PI / 180.0));
            _yVelocity -= ACCELERATION * Math.Cos(_angle * (Math.PI / 180.0));

            double speed = Math.Sqrt(_xVelocity * _xVelocity + _yVelocity * _yVelocity);
            if (speed > MAX_SPEED)
            {
                _xVelocity = (_xVelocity / speed) * MAX_SPEED;
                _yVelocity = (_yVelocity / speed) * MAX_SPEED;
            }
        }

        public void MoveBackward()
        {
            _xVelocity -= ACCELERATION * Math.Sin(_angle * (Math.PI / 180.0));
            _yVelocity += ACCELERATION * Math.Cos(_angle * (Math.PI / 180.0));

            // Limit speed to maximum speed
            double speed = Math.Sqrt(_xVelocity * _xVelocity + _yVelocity * _yVelocity);
            if (speed > MAX_SPEED)
            {
                _xVelocity = (_xVelocity / speed) * MAX_SPEED;
                _yVelocity = (_yVelocity / speed) * MAX_SPEED;
            }
        }


        public void RotateLeft()
        {
            _angle -= 5;
        }
        public void RotateRight()
        {
            _angle += 5;
        }

        public void MoveUp()
        {
            _y -= 5;
        }

        public void MoveDown()
        {
            _y += 5;
        }

        public void ChangeBorderColor(int index, bool isSelected)
        {
            _selectedIndex = index;
            _isSelected = isSelected;
        }
    }

}
