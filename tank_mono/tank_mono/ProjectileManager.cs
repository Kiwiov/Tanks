using System;
using System.Collections.Generic;
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
        private List<Projectile> _projectiles = new List<Projectile>();

        public List<Projectile> Projectiles
        {
            get { return _projectiles; }
            set { _projectiles = value; }
        }

        public void Shoot(Tank tank)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Space))
            {
                for (int i = 0; i < tank.CurrentWeapon.ShotsFired; i++)
                {
                    Projectiles.Add(new Projectile(tank.CurrentWeapon.Type, tank.Position - new Vector2(0, 2), tank, tank.CurrentWeapon.Texture, 0));
                }
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Projectiles != null)
            {
                foreach (var projectile in Projectiles)
                {
                    spriteBatch.Draw(projectile.Texture, projectile.Position, null, color: null, rotation: projectile.Rotation, origin: new Vector2(projectile.Texture.Width / 2, projectile.Texture.Height / 2));
                }
            }
            
        }


    }
}
