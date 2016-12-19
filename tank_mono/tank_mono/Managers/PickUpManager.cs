﻿using System;
using System.Collections.Generic;
using System.IO;
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
        Random _ran = new Random();

        public PickUpManager(Texture2D TextureAmmoBox, Texture2D TextureFuelBarrel)
        {
            this.TextureAmmoBox = TextureAmmoBox;
            this.TextureFuelBarrel = TextureFuelBarrel;
        }

        private Vector2 RandomLocation()
        {
            Vector2 vector = new Vector2(_ran.Next(Game1.width),300);
            return vector;
        }
        public void CreatePickup(string type)
        {
            if (type == "Random")
            {
                if (_ran.Next(2) == 1)
                {
                    PickUps.Add(new PickUp(RandomLocation(), "Ammo", TextureAmmoBox));
                }
                else
                {
                    PickUps.Add(new PickUp(RandomLocation(), "Fuel", TextureFuelBarrel));
                }

                
                //Rotation
            }
            else if (type == "Ammo")
            {
                PickUps.Add(new PickUp(RandomLocation(), type, TextureAmmoBox));
                //Rotation

            }
            else if (type == "Fuel")
            {
                PickUps.Add(new PickUp(RandomLocation(), type, TextureFuelBarrel));
                //Rotation

            }
        }

        public void DetectPickup(Tank tank)
        {
            for (int i = 0; i < PickUps.Count; i++)
            {
                if (Collision.TestIfCollision(tank.Hitbox,PickUps[i].Hitbox,tank.SpriteMain,PickUps[i].Texture)) 
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
            TryAgain:
            int decision = ran.Next(1, tank.Weapons.Count);

            
            if (tank.Weapons.Values.ElementAt(decision).CurrentAmmo != tank.Weapons.Values.ElementAt(decision).Ammo)
            {
                tank.Weapons.Values.ElementAt(decision).CurrentAmmo += tank.Weapons.Values.ElementAt(decision).Ammo / 2;
                if (tank.Weapons.Values.ElementAt(decision).CurrentAmmo > tank.Weapons.Values.ElementAt(decision).Ammo)
                {
                    tank.Weapons.Values.ElementAt(decision).CurrentAmmo = tank.Weapons.Values.ElementAt(decision).Ammo;
                }
            }
            else
            {
                foreach (var weapon in tank.Weapons)
                {
                    if (weapon.Value.CurrentAmmo != weapon.Value.Ammo)
                    {
                        goto TryAgain;
                    }
                }
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var pickUp in PickUps)
            {
                spriteBatch.Draw(pickUp.Texture, pickUp.Position, null, rotation: 0, origin: new Vector2(pickUp.Texture.Width / 2, pickUp.Texture.Height / 2 ));
                //spriteBatch.Draw(pickUp.Texture, new Vector2(pickUp.Hitbox.X, pickUp.Hitbox.Y), pickUp.Hitbox, Color.Blue);

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
