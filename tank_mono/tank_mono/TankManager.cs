using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    class TankManager
    {
        private Texture2D _heavyTankMain;
        private Texture2D _lightTankMain;
        private Texture2D _standardTankMain;
        private Texture2D _Cannon;

        private List<Tank> _tanks;

        public TankManager(Texture2D HeavyTankBody, Texture2D StandardTankBody, Texture2D LightTankBody, Texture2D TankCannon)
        {
            _heavyTankMain = HeavyTankBody;
            _lightTankMain = LightTankBody;
            _standardTankMain = StandardTankBody;
            _Cannon = TankCannon;
        }

        public void CreateTank(Vector2 Position, string TankType, Texture2D HeavyTankBody, Texture2D StandardTankBody, Texture2D LightTankBody, Texture2D TankCannon, Color Colour, bool IsBot)
        {
            Tanks.Add(new Tank(Position, TankType, HeavyTankBody, StandardTankBody, LightTankBody, TankCannon, Colour, IsBot));
        }
        

        public List<Tank> Tanks
        {
            get { return _tanks; }
            set { _tanks = value; }
        }





    }
}
