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
    class TankManager
    {
        private Texture2D _heavyTankMain;
        private Texture2D _lightTankMain;
        private Texture2D _standardTankMain;
        private Texture2D _heavyCannon;
        private Texture2D _standardCannon;
        private Texture2D _lightCannon;

        private List<Tank> _tanks;

        public TankManager(Texture2D HeavyTankBody, Texture2D StandardTankBody, Texture2D LightTankBody,Texture2D HeavyCannon, Texture2D StandardCannon, Texture2D LightCannon)
        {
            _heavyTankMain = HeavyTankBody;
            _lightTankMain = LightTankBody;
            _standardTankMain = StandardTankBody;
            _heavyCannon = HeavyCannon;
            _standardCannon = StandardCannon;
            _lightCannon = LightCannon;
            _tanks = new List<Tank>();
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
                        tank.Health = 200;
                        tank.Speed = 20;
                        tank.Fuel = 400;
                        tank.Armour = 200;
                        
                        //Add weapons

                        tank.SpriteMain = _heavyTankMain;
                        tank.Cannon = _heavyCannon;
                        break;
                    case "Standard":
                        tank.Health = 150;
                        tank.Speed = 40;
                        tank.Fuel = 400;
                        tank.Armour = 150;

                        //Add weapons

                        tank.SpriteMain = _standardTankMain;
                        tank.Cannon = _standardCannon;
                        break;
                    case "Light":
                        tank.Health = 100;
                        tank.Speed = 60;
                        tank.Fuel = 400;
                        tank.Armour = 100;

                        //Add weapons

                        tank.SpriteMain = _lightTankMain;
                        tank.Cannon = _lightCannon;
                        break;
                }
            }
        }

        public void MoveTank(Tank tank)
        {
            KeyboardState ks = Keyboard.GetState();
            
            if ((ks.IsKeyDown(Keys.Left) | ks.IsKeyDown(Keys.A)) && ks.IsKeyUp(Keys.Right) && ks.IsKeyUp(Keys.D) && tank.Fuel > 0)
            {
                tank.Position.X -= tank.Speed / 50;
                tank.Fuel -= 1;
            }
            if ((ks.IsKeyDown(Keys.Right) | ks.IsKeyDown(Keys.D)) && ks.IsKeyUp(Keys.Left) && ks.IsKeyUp(Keys.A) && tank.Fuel > 0)
            {
                tank.Position.X += tank.Speed / 50;
                tank.Fuel -= 1;
            }
        } 

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tank in Tanks)
            {
                spriteBatch.Draw(tank.Cannon, tank.Position, null, color: tank.Colour, rotation: tank.CannonRotation, origin: new Vector2(tank.Cannon.Width / 2, tank.Cannon.Height));
                spriteBatch.Draw(tank.SpriteMain, tank.Position, null, color: tank.Colour, rotation: tank.TankRotaion, origin: new Vector2(tank.SpriteMain.Width / 2, tank.SpriteMain.Height / 2));
            }
        }

        public List<Tank> Tanks
        {
            get { return _tanks; }
            set { _tanks = value; }
        }





    }
}
