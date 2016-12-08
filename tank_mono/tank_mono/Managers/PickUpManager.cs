using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    class PickUpManager
    {
        private Texture2D _textureAmmoBox;
        private Texture2D _textureFuelBarrel;
        private List<PickUp> _pickUps = new List<PickUp>();

        public PickUpManager(Texture2D TextureAmmoBox, Texture2D TextureFuelBarrel)
        {
            this.TextureAmmoBox = TextureAmmoBox;
            this.TextureFuelBarrel = TextureFuelBarrel;
        }

        public void CreatePickup(Vector2 position, string type)
        {
            if (type == "Ammo")
            {
                PickUps.Add(new PickUp(position, type, TextureAmmoBox));
                //Rotation

            }
            else
            {
                PickUps.Add(new PickUp(position, type, TextureFuelBarrel));
                //Rotation

            }
        }

        public void DetectPickup(Tank tank)
        {
            for (int i = 0; i < PickUps.Count; i++)
            {
                if (false) //if PickUp and tank collide 
                {
                    if (PickUps[i].Type == "Ammo")
                    {
                        IfPickUpAmmo(tank);
                        PickUps.RemoveAt(i);
                        goto End;
                    }
                    else if (PickUps[i].Type == "Fuel")
                    {
                        IfPickUpFuel(tank);
                        PickUps.RemoveAt(i);
                        goto End;
                    }
                }
            }
            End:;
            
        }

        private void IfPickUpAmmo(Tank tank)
        {
            Random ran = new Random();
            int decision = ran.Next(1, tank.Weapons.Count);

            tank.Weapons.Values.ElementAt(decision).CurrentAmmo += tank.Weapons.Values.ElementAt(decision).Ammo / 2;
            if (tank.Weapons.Values.ElementAt(decision).CurrentAmmo > tank.Weapons.Values.ElementAt(decision).Ammo)
            {
                tank.Weapons.Values.ElementAt(decision).CurrentAmmo = tank.Weapons.Values.ElementAt(decision).Ammo;
            }
        }

        private void IfPickUpFuel(Tank tank)
        {
                tank.CurrentFuel = tank.Fuel;
        }
        public List<PickUp> PickUps
        {
            get { return _pickUps; }
            set { _pickUps = value; }
        }

        public Texture2D TextureAmmoBox 
        {
            get { return _textureAmmoBox; }
            set { _textureAmmoBox = value; }
        }

        public Texture2D TextureFuelBarrel
        {
            get { return _textureFuelBarrel; }
            set { _textureFuelBarrel = value; }
        }

    }
}
