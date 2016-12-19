using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private int _nrOfShots;
        private float _power = 0;
        private int _upDown = 1;
        private static bool _shooting;
        private bool _fire;
        public static bool _fired;

        private List<Projectile> _projectiles = new List<Projectile>();
        private Vector2 _wind;
        private TerrainManager _terrainManager;
        public ProjectileManager(GameLogic gameLogic, TerrainManager terrainManager)
        {
            Wind = new Vector2((float)gameLogic.Wind / 20000,0);
            _fired = false;
            
            _terrainManager = terrainManager;
        }

        public void Shoot(Tank tank)
        {
            KeyboardState ks = Keyboard.GetState();

            if (tank.CurrentWeapon.CurrentAmmo > 0 && !_fired)
            {
                if (ks.IsKeyDown(Keys.Space) && !_shooting)
                {

                    _nrOfShots = tank.CurrentWeapon.ShotsFired*firerate;
                    if (_power >= 10 && _upDown == 1)
                    {
                        _upDown = -1;
                    }
                    else if (_power <= 1 && _upDown == -1)
                    {
                        _upDown = 1;
                    }

                    _power += 0.1f*_upDown;

                    Debug.WriteLine("Power: " + _power);

                }
                else if (ks.IsKeyUp((Keys.Space)) && _power != 0)
                {
                    _fire = true;
                }
                if (_fire)
                {
                    _shooting = true;
                    if (_power > 10)
                    {
                        _power = 10;
                    }
                    else if (_power < 1)
                    {
                        _power = 1;
                    }

                    if (_nrOfShots > 0 && _nrOfShots%firerate == firerate - 1)
                    {
                        Projectiles.Add(new Projectile(tank.CurrentWeapon.Type, tank.Position - new Vector2(0, 2),
                            tank, tank.CurrentWeapon.Texture, 0,
                            new Vector2((float) Math.Sin(tank.CannonRotation), -(float) Math.Cos(tank.CannonRotation)),
                            _power));
                    }

                    else if (_nrOfShots <= 0)
                    {
                        _fire = false;
                        _power = 0;
                        _upDown = 1;
                        _shooting = false;
                        //tank.CurrentWeapon.CurrentAmmo--;
                        tank.Weapons[tank.CurrentWeapon.Name].CurrentAmmo--;
                        _fired = true;
                    }
                    _nrOfShots--;
                }
            }
        }

        public void DetectCollisionProjectileTank(TankManager tankManager, Tank currentTank)
        {
            for (int i = 0; i < Projectiles.Count; i++)
            {
                for (int j = 0; j < tankManager.Tanks.Count; j++)
                {
                    if (Collision.TestIfCollision(Projectiles[i].Hitbox,tankManager.Tanks[j].Hitbox,Projectiles[i].Texture,tankManager.Tanks[j].SpriteMain) && tankManager.Tanks[j] != Projectiles[i].Owner)
                    {
                        Projectiles.RemoveAt(i);
                        goto End;
                    }
                }
            }
            End:;
        }

        public void MoveProjectiles()
        {
            foreach (var projectile in Projectiles)
            {
                projectile.Position += projectile.Power * projectile.Velocity ;

                projectile.Velocity -= new Vector2(0, -0.006f);
                projectile.Velocity += Wind;
                projectile.Rotation = (float)Math.Atan2(projectile.Velocity.Y,projectile.Velocity.X);
            }
        }

        public void MoveProjectileHitboxes()
        {
            foreach (var projectile in Projectiles)
            {
                projectile.Hitbox.X = (int)(projectile.Position.X - projectile.Texture.Width / 2);
                projectile.Hitbox.Y = (int)(projectile.Position.Y - projectile.Texture.Height / 2);
            }
        }

        public void DestroyOrNot()
        {
            for (int i = 0; i < Projectiles.Count; i++)
            {
                if (Projectiles[i].Position.Y >= _terrainManager.FindLand(Projectiles[i].Position))
                {
                    Projectiles.RemoveAt(i);
                    break;
                }
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

        public static bool IsShooting()
        {
            return _shooting;
        }
            
        public List<Projectile> Projectiles
        {
            get { return _projectiles; }
            set { _projectiles = value; }
        }

        public Vector2 Wind
        {
            get { return _wind; }
            set { _wind = value; }
        }
        

    }
}
