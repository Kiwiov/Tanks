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
    public class TankManager
    {
        private Texture2D _heavyTankMain;
        private Texture2D _lightTankMain;
        private Texture2D _standardTankMain;
        private Texture2D _heavyCannon;
        private Texture2D _standardCannon;
        private Texture2D _lightCannon;
        private int _switchTimer;

        private WeaponCreator _weaponCreator;

        private List<Tank> _tanks;

        public TankManager(Texture2D HeavyTankBody, Texture2D StandardTankBody, Texture2D LightTankBody,Texture2D HeavyCannon, Texture2D StandardCannon, Texture2D LightCannon, WeaponCreator WeaponCreator)
        {
            _heavyTankMain = HeavyTankBody;
            _lightTankMain = LightTankBody;
            _standardTankMain = StandardTankBody;
            _heavyCannon = HeavyCannon;
            _standardCannon = StandardCannon;
            _lightCannon = LightCannon;
            Tanks = new List<Tank>();
            _weaponCreator = WeaponCreator;
        }

        public void CreateTank(Vector2 Position, string TankType, Color Colour, bool IsBot)
        {
            Tanks.Add(new Tank(Position, TankType, Colour, IsBot));

        }

        public void SetStats()
        {
            foreach (var tank in Tanks)
            {
                switch (tank.TankType)
                {
                    case "Heavy":
                        //stats
                        tank.Health = 200;
                        tank.Fuel = 400;
                        tank.Armour = 300;

                        tank.CurrentHealth = 200;
                        tank.CurrentFuel = 400;
                        tank.CurrentArmour = 300;

                        tank.Speed = 20;

                        //Textures
                        tank.SpriteMain = _heavyTankMain;
                        tank.Cannon = _heavyCannon;

                        //Hitbox
                        tank.Hitbox = tank.SpriteMain.Bounds;
                        break;
                    case "Standard":
                        //stats
                        tank.Health = 150;
                        tank.Fuel = 400;
                        tank.Armour = 150;
                        
                        tank.CurrentHealth = 150;
                        tank.CurrentFuel = 400;
                        tank.CurrentArmour = 150;

                        tank.Speed = 40;

                        //Textures
                        tank.SpriteMain = _standardTankMain;
                        tank.Cannon = _standardCannon;

                        //Hitbox
                        tank.Hitbox = tank.SpriteMain.Bounds;
                        break;
                    case "Light":
                        //stats
                        tank.Health = 100;
                        tank.Fuel = 400;
                        tank.Armour = 100;

                        tank.CurrentHealth = 100;
                        tank.CurrentFuel = 400;
                        tank.CurrentArmour = 100;

                        tank.Speed = 60;

                        //Textures
                        tank.SpriteMain = _lightTankMain;
                        tank.Cannon = _lightCannon;

                        //Hitbox
                        tank.Hitbox = tank.SpriteMain.Bounds;
                        break;
                }
            }
        }

        public void SetWeapons()
        {
            foreach (var tank in Tanks)
            {
                switch (tank.TankType)
                {
                    case "Heavy":
                        //weapons
                        tank.Weapons.Add(_weaponCreator.MachineGun().Name, _weaponCreator.MachineGun());
                        tank.Weapons.Add(_weaponCreator.Missile().Name, _weaponCreator.Missile());
                        tank.Weapons.Add(_weaponCreator.AntiArmour().Name, _weaponCreator.AntiArmour());

                        tank.CurrentWeapon = tank.Weapons["MachineGun"];

                        break;
                    case "Standard":
                        //weapons
                        tank.Weapons.Add(_weaponCreator.MachineGun().Name, _weaponCreator.MachineGun());
                        tank.Weapons.Add(_weaponCreator.Missile().Name, _weaponCreator.Missile());
                        tank.Weapons.Add(_weaponCreator.AntiArmour().Name, _weaponCreator.AntiArmour());

                        tank.CurrentWeapon = tank.Weapons["MachineGun"];
                        break;
                    case "Light":
                        //weapons
                        tank.Weapons.Add(_weaponCreator.MachineGun().Name, _weaponCreator.MachineGun());
                        tank.Weapons.Add(_weaponCreator.Missile().Name, _weaponCreator.Missile());
                        tank.Weapons.Add(_weaponCreator.AntiArmour().Name, _weaponCreator.AntiArmour());

                        tank.CurrentWeapon = tank.Weapons["MachineGun"];
                        break;
                }
            }
        }

        public void MoveHitbox(Tank tank)
        {
            tank.Hitbox.X = (int)(tank.Position.X - tank.SpriteMain.Width / 2);
            tank.Hitbox.Y = (int)(tank.Position.Y - tank.SpriteMain.Height / 2);
        }

        public void MoveTank(Tank tank)
        {
            KeyboardState ks = Keyboard.GetState();

            if (tank.Falling)
            {
                tank.Position.Y++;
            }

            if ((ks.IsKeyDown(Keys.Left) | ks.IsKeyDown(Keys.A)) && ks.IsKeyUp(Keys.Right) && ks.IsKeyUp(Keys.D) && tank.CurrentFuel > 0 && tank.Falling == false)
            {
                tank.Position.X -= tank.Speed / 50;
                tank.CurrentFuel -= 1;
            }
            if ((ks.IsKeyDown(Keys.Right) | ks.IsKeyDown(Keys.D)) && ks.IsKeyUp(Keys.Left) && ks.IsKeyUp(Keys.A) && tank.CurrentFuel > 0 && tank.Falling == false)
            {
                tank.Position.X += tank.Speed / 50;
                tank.CurrentFuel -= 1;
            }
            
            if ((ks.IsKeyDown(Keys.Up) | ks.IsKeyDown(Keys.W)) && ks.IsKeyUp(Keys.Down) && ks.IsKeyUp(Keys.S) && tank.Falling == false)
            {
                if (tank.TankType == "Light")
                {
                    if (tank.CannonRotation < 1.5f)
                    {
                        tank.CannonRotation += 0.01f;
                    }
                }
                else
                {
                    if (tank.CannonRotation < 1.45f)
                    {
                        tank.CannonRotation += 0.01f;
                    }
                }
            }
            if ((ks.IsKeyDown(Keys.Down) | ks.IsKeyDown(Keys.S)) && ks.IsKeyUp(Keys.Up) && ks.IsKeyUp(Keys.W) && tank.Falling == false)
            {
                if (tank.TankType == "Light")
                {
                    if (tank.CannonRotation > -1.5f)
                    {
                        tank.CannonRotation -= 0.01f;
                    }
                }
                else
                {
                    if (tank.CannonRotation > -1.45f)
                    {
                        tank.CannonRotation -= 0.01f;
                    }
                }
            }

            if (ks.IsKeyDown(Keys.Q) && _switchTimer == 0 && tank.Falling == false)
            {
                List<string> temp = new List<string>();

                foreach (var var in tank.Weapons)
                {
                    temp.Add(var.Key);
                }
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i] == tank.CurrentWeapon.Name)
                    {
                        if (i == temp.Count - 1)
                        {
                            _switchTimer = 15;
                            tank.CurrentWeapon = tank.Weapons.Values.ElementAt(0);
                        }
                        else
                        {
                            _switchTimer = 15;
                            tank.CurrentWeapon = tank.Weapons.Values.ElementAt(i + 1);
                        }
                        break;
                    }
                }
            }
            else
            {
                if (_switchTimer != 0)
                {
                    _switchTimer--;
                }

            }
        } 

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tank in Tanks)
            {
                spriteBatch.Draw(tank.Cannon, tank.Position - new Vector2(0, 2), null, color: tank.Colour, rotation: (float)tank.CannonRotation, origin: new Vector2(tank.Cannon.Width / 2, tank.Cannon.Height));
                spriteBatch.Draw(tank.SpriteMain, tank.Position, null, color: tank.Colour, rotation: tank.TankRotaion, origin: new Vector2(tank.SpriteMain.Width / 2, tank.SpriteMain.Height / 2));
                spriteBatch.Draw(tank.SpriteMain, new Vector2(tank.Position.X, tank.Position.Y), tank.Hitbox, Color.Red);
            }
        }

        public List<Tank> Tanks
        {
            get { return _tanks; }
            set { _tanks = value; }
        }
    }
}
