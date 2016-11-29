using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tank_mono
{
    class Tank
    {
        public Vector2 Position;
        private Color _colour;
        

        private string _tankType;

        private float _health;
        private float _armour;
        private float _speed;
        private float _fuel;
        private float _cannonRotation;
        private float _tankRotation;


        private bool _isBot;
        
        private Dictionary<Weapon, int> _weapons;

        private Texture2D _spriteMain;
        private Texture2D _cannon;


        public Tank(Vector2 Position, string TankType, Color Colour, bool IsBot)
        {
            this.Position = Position;
            this.TankType = TankType;
            this.IsBot = IsBot;
            this.Colour = Colour;
        }

        
        
        public float TankRotaion
        {
            get { return _tankRotation; }
            set { _tankRotation = value; }
        }

        public float CannonRotation
        {
            get { return _cannonRotation; }
            set { _cannonRotation = value; }
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
        
    }
}
