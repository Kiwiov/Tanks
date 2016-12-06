using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tank_mono
{
    class ProjectileManager
    {
        private int firerate = 5;
        int _nrOfShots;
        float _power = 0;
        int _upDown = 1;
        bool shooting;
        private bool fire;
        List<Projectile> _projectiles = new List<Projectile>();

        public List<Projectile> Projectiles
        {
            get { return _projectiles; }
            set { _projectiles = value; }
        }

        public void Shoot(Tank tank)
        {



            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Space) && !shooting)
            {

                _nrOfShots = tank.CurrentWeapon.ShotsFired * firerate;
                if (_power >= 10 && _upDown == 1)
                {
                    _upDown = -1;
                }
                else if (_power <= 1 && _upDown == -1)
                {
                    _upDown = 1;
                }

                _power += 0.1f * _upDown;

                Debug.WriteLine("Power: " + _power);

            }
            else if (ks.IsKeyUp((Keys.Space)) && _power != 0)
            {
                fire = true;
            }
            if (fire)
            {
                shooting = true;
                if (_power > 10)
                {
                    _power = 10;
                }

                else if (_power < 1)
                {
                    _power = 1;
                }

                if (_nrOfShots > 0 && _nrOfShots % firerate == firerate - 1)
                {
                    Projectiles.Add(new Projectile(tank.CurrentWeapon.Type, tank.Position - new Vector2(0, 2),
                    tank, tank.CurrentWeapon.Texture, 0,
                    new Vector2((float)Math.Sin(tank.CannonRotation), -(float)Math.Cos(tank.CannonRotation)),
                    _power));
                }

                else if (_nrOfShots <= 0)
                {
                    fire = false;
                    _power = 0;
                    _upDown = 1;
                    shooting = false;
                }
                _nrOfShots--;
            }
        }

        public void MoveProjectiles()
        {
            foreach (var projectile in Projectiles)
            {
                projectile.Position += projectile.Power * projectile.Velocity;

                projectile.Velocity -= new Vector2(0, -0.006f);
                projectile.Rotation = (float)Math.Atan2(projectile.Velocity.Y,projectile.Velocity.X);


            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Projectiles != null)
            {
                foreach (var projectile in Projectiles)
                {
                    spriteBatch.Draw(projectile.Texture, projectile.Position, null, color: null, rotation: projectile.Rotation + (float)Math.PI/2,scale: new Vector2(1,1), origin: new Vector2(projectile.Texture.Width / 2, projectile.Texture.Height / 2));
                }
            }

        }


    }
}
