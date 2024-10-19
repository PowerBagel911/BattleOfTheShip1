using System;
using System.Collections.Generic;
using BattleOfTheShip1.GameObject;
using SplashKitSDK;

namespace BattleOfTheShip1.GameState
{
    public class SinglePlayerState : EnvironmentState
    {
        private Ship _playerShip;
        private List<Bullet> _bullets;
        private List<Asteroid> _asteroids;
        private const int MaxAsteroids = 5;
        private List<Heart> _hearts;
        private List<HealthPackGreen> _healthPackGreens;
        private List<HealthPackRed> _healthPackReds;
        private int _score; // Score counter
        private Bitmap _heartBitmap;
        private Music _backgroundMusic;
        private SplashKitSDK.Timer _gameTimer;  // Timer to track the duration of the game
        private const int MaxGameDuration = 20000; // Max duration in milliseconds (2 minute)

        private List<AmmoReload> _ammoReloads;

        public SinglePlayerState(Ship selectedShip)
        {
            // Create a new instance of the selected ship in the middle of the screen
            _playerShip = new Ship((SplashKit.ScreenWidth() - selectedShip.Width) / 2, (SplashKit.ScreenHeight() - selectedShip.Height) / 2, selectedShip.ImagePath);
            
            _score = 0;

            _bullets = new List<Bullet>();
            _asteroids = new List<Asteroid>();
            _ammoReloads = new List<AmmoReload>();  // Initialize the list of ammo reloads
            _healthPackGreens = new List<HealthPackGreen>();
            _healthPackReds = new List<HealthPackRed>();
            _heartBitmap = SplashKit.LoadBitmap("Heart", "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\images\\heart.png");
            _backgroundMusic = SplashKit.LoadMusic("LaserSound", "C:\\Users\\Admin\\Desktop\\Swinburne\\BattleOfTheShip\\media\\sounds\\mario.mp3");
            // Initialize hearts list
            _hearts = new List<Heart>();
            for (int i = 0; i < 5; i++)  // Start with 5 hearts
            {
                _hearts.Add(new Heart(20 + i * 40, 20));
            }
            _backgroundMusic.Play(1);

            // Start the game timer
            _gameTimer = new SplashKitSDK.Timer("GameTimer");
            _gameTimer.Start();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            // Load content for the player ship
            _playerShip.LoadContent();
            foreach (Heart heart in _hearts)
            {
                heart.LoadContent();
            }

            

            // Load font for score display
            SplashKit.LoadFont("ScoreFont", "arial.ttf");

        }

        public override void HandleInput()
        {
            // Handle player movement
            if (SplashKit.KeyDown(KeyCode.LeftKey))
            {
                _playerShip.RotateLeft();
            }
            if (SplashKit.KeyDown(KeyCode.RightKey))
            {
                _playerShip.RotateRight();
            }
            if (SplashKit.KeyDown(KeyCode.UpKey))
            {
                _playerShip.MoveForward();
            }
            if (SplashKit.KeyDown(KeyCode.DownKey))
            {
                _playerShip.MoveBackward();
            }
            if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                SplashKit.StopMusic();
                GameManager.Instance.ChangeState(new MainMenuState());
            }

            // Handle shooting
            if (SplashKit.KeyTyped(KeyCode.SpaceKey))
            {
                if (_playerShip.Ammo > 0)
                {
                    LaserSound.Play();
                    _bullets.Add(_playerShip.Shoot());
                }
                else
                {
                    EmptySound.Play();
                }
            }
        }

        

        public override void Update()
        {
            base.Update();
            // Update player ship position or other game logic
            _playerShip.Update();

            if (_gameTimer.Ticks > MaxGameDuration - 1000)
            {
                SplashKit.StopMusic();
                GameManager.Instance.ChangeState(new GameOverState(_score));
                return;
            }

            // Update health packs
            foreach (HealthPackGreen healthPackGreen in _healthPackGreens)
            {
                healthPackGreen.Update();
            }

            foreach (HealthPackRed healthPackRed in _healthPackReds)
            {
                healthPackRed.Update();
            }

            // Check if we need to spawn a new asteroid
            if (_asteroids.Count < MaxAsteroids && Asteroid.ShouldSpawn())
            {
                _asteroids.Add(new Asteroid());
            }

            if (_healthPackGreens.Count == 0 && HealthPackGreen.ShouldSpawn())
            {
                HealthPackGreen newHealthPackGreen = new HealthPackGreen();
                newHealthPackGreen.LoadContent();
                _healthPackGreens.Add(newHealthPackGreen);
            }

            if (_healthPackReds.Count == 0 && HealthPackRed.ShouldSpawn())
            {
                HealthPackRed newHealthPackRed = new HealthPackRed();
                newHealthPackRed.LoadContent();
                _healthPackReds.Add(newHealthPackRed);
            }

            // Check if we need to spawn a new ammo reload
            if (_ammoReloads.Count == 0 && AmmoReload.ShouldSpawn())
            {
                AmmoReload newAmmoReload = new AmmoReload();
                newAmmoReload.LoadContent();
                _ammoReloads.Add(newAmmoReload);
            }

            // Handle ship-asteroid collision
            for (int i = _asteroids.Count - 1; i >= 0; i--)
            {
                Asteroid asteroid = _asteroids[i];
                if (SplashKit.BitmapCollision(_playerShip.Bitmap, _playerShip.X, _playerShip.Y, asteroid.Bitmap, asteroid.X, asteroid.Y))
                {
                    BoomSound.Play();
                    _playerShip.TakeDamage();
                    _asteroids.RemoveAt(i);
                    if (_hearts.Count > 0)
                    {
                        // Remove the last heart visually when the ship takes damage
                        _hearts.RemoveAt(_hearts.Count - 1);
                    }
                    if (_hearts.Count == 0)
                    {
                        SplashKit.StopMusic();
                        GameManager.Instance.ChangeState(new GameOverState(_score));
                    }
                    break;  // Stop checking after the first collision
                }
            }

            // Handle bullet-asteroid collision detection
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = _bullets[i];
                if (bullet == null) continue; // Skip null bullets
                for (int j = _asteroids.Count - 1; j >= 0; j--)
                {
                    Asteroid asteroid = _asteroids[j];
                    if (SplashKit.BitmapCollision(bullet.Bitmap, bullet.X, bullet.Y, asteroid.Bitmap, asteroid.X, asteroid.Y))
                    {
                        HitSound.Play();
                        _bullets.RemoveAt(i);
                        _asteroids.RemoveAt(j);
                        _score++; // Increase score when an asteroid is destroyed
                        break;
                    }
                }
            }

            // Handle ammo reload collection
            for (int i = _ammoReloads.Count - 1; i >= 0; i--)
            {
                AmmoReload ammoReload = _ammoReloads[i];
                if (ammoReload.IsCollected(_playerShip))
                {
                    ReloadSound.Play();
                    _playerShip.ReloadAmmo();  // Reload ship ammo
                    _ammoReloads.RemoveAt(i);  // Remove ammo reload after it has been collected
                }
            }

            // Handle health pack green collection
            for (int i = _healthPackGreens.Count - 1; i >= 0; i--)
            {
                HealthPackGreen healthPackGreen = _healthPackGreens[i];
                if (healthPackGreen.IsCollected(_playerShip))
                {
                    HealSound.Play();
                    _healthPackGreens.RemoveAt(i);  // Remove health pack green after it has been collected
                    // Add a new heart for the green health pack if less than 5 hearts already
                    if (_hearts.Count < 5)
                    {
                        _hearts.Add(new Heart(20 + _hearts.Count * 40, 20));
                    }
                }
            }

            // Handle health pack red collection
            for (int i = _healthPackReds.Count - 1; i >= 0; i--)
            {
                HealthPackRed healthPackRed = _healthPackReds[i];
                if (healthPackRed.IsCollected(_playerShip))
                {
                    HealSound.Play();
                    _healthPackReds.RemoveAt(i);  // Remove health pack red after it has been collected
                                                  // Add 2 hearts when a red health pack is collected

                    if (_hearts.Count < 4)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            _hearts.Add(new Heart(20 + _hearts.Count * 40, 20));
                        }
                    }

                    
                }
            }



            // Update bullets
            foreach (Bullet bullet in _bullets)
            {
                if (bullet != null)
                {
                    bullet.Update();
                }
            }

            // Update ammo reloads
            foreach (AmmoReload ammoReload in _ammoReloads)
            {
                ammoReload.Update();
            }

            // Update asteroids
            foreach (Asteroid asteroid in _asteroids)
            {
                asteroid.Update();
            }

            // Remove bullets that are out of bounds
            _bullets.RemoveAll(b => b != null && b.IsOutOfBounds());
        }

        private void DrawScore()
        {
            // Draw the score on the screen
            string scoreText = $"Score: {_score}";
            SplashKit.DrawText(scoreText, Color.White, SplashKit.FontNamed("ScoreFont"), 30, 160, 70);
        }

        private void DrawTimeRemaining()
        {
            int timeRemaining = (MaxGameDuration - (int)_gameTimer.Ticks) / 1000; // Calculate time remaining in seconds
            string timeText = $"Time Left: {timeRemaining}s";
            SplashKit.DrawText(timeText, Color.White, SplashKit.FontNamed("ScoreFont"), 30, 20, 150);
        }

        private void DrawAmmo()
        {
            string ammoText = $"Ammo: {_playerShip.Ammo}";
            SplashKit.DrawText(ammoText, Color.White, SplashKit.FontNamed("ScoreFont"), 30, 160, 30);
        }

        public override void Draw()
        {
            base.Draw();
            // Draw the player ship
            _playerShip.Draw();

            DrawScore();
            DrawAmmo();
            DrawTimeRemaining();

            // Draw ammo reloads
            foreach (AmmoReload ammoReload in _ammoReloads)
            {
                ammoReload.Draw();
            }

            // Draw bullets
            foreach (Bullet bullet in _bullets)
            {
                if (bullet != null)
                {
                    bullet.Draw();
                }
            }

            // Draw asteroids
            foreach (Asteroid asteroid in _asteroids)
            {
                asteroid.Draw();
            }

            // Draw hearts
            foreach (Heart heart in _hearts)
            {
                heart.Draw();
            }

            // Draw health packs
            foreach (HealthPackGreen healthPackGreen in _healthPackGreens)
            {
                healthPackGreen.Draw();
            }

            foreach (HealthPackRed healthPackRed in _healthPackReds)
            {
                healthPackRed.Draw();
            }
        }
    }
}