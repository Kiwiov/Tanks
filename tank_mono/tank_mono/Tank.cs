using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    class Tank
    {
        private Vector2 _position;
        private Color _colour;

        private string _tankType;

        private float _health;
        private float _armour;
        private float _speed;
        private float _fuel;

        private bool _isBot;
        
        private Dictionary<Weapon, int> _weapons;

        private Texture2D _spriteMain;
        private Texture2D _cannon;


        public Tank(Vector2 Position, string TankType, Texture2D HeavyTankBody, Texture2D StandardTankBody, Texture2D LightTankBody, Texture2D TankCannon, Color Colour, bool IsBot)
        {
            this.Position = Position;
            this.TankType = TankType;
            SetStats(this.TankType, HeavyTankBody, StandardTankBody, LightTankBody, TankCannon);
        }
        
        private void SetStats(string tankType, Texture2D HeavyTankBody, Texture2D StandardTankBody, Texture2D LightTankBody, Texture2D TankCannon)
        {
            switch (tankType)
            {
                case "Heavy":
                    //tank stats
                    //Add weapons
                    SpriteMain = HeavyTankBody;
                    Cannon = TankCannon;
                    break;
                case "Standard":
                    //tank stats
                    //Add weapons
                    SpriteMain = StandardTankBody;
                    Cannon = TankCannon;
                    break;
                case "Light":
                    //tank stats
                    //Add weapons
                    SpriteMain = LightTankBody;
                    Cannon = TankCannon;
                    break;
            }
        }

        

        public bool IsBot
        {
            get { return _isBot; }
            set { _isBot = value; }
        }


        public Color Colour
        {
            get { return _colour; }
            set { _colour = value; }
        }

        public float Fuel
        {
            get { return _fuel; }
            set { _fuel = value; }
        }


        public Texture2D Cannon
        {
            get { return _cannon; }
            set { _cannon = value; }
        }


        public Texture2D SpriteMain
        {
            get { return _spriteMain; }
            set { _spriteMain = value; }
        }


        public Dictionary<Weapon, int> Weapons
        {
            get { return _weapons; }
            set { _weapons = value; }
        }


        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public float Armour
        {
            get { return _armour; }
            set { _armour = value; }
        }


        public float Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public string TankType
        {
            get { return _tankType; }
            set { _tankType = value; }
        }
        

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

    }
}
