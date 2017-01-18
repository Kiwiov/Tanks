using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    public class Projectile
    {
        private string _type;
        private Vector2 _position;
        private Tank _owner;
        private Texture2D _texture;
        private float _rotation;
        private Vector2 _velocity;
        private float _power;
        public Rectangle Hitbox;
        private bool _isExplosive;
        private float _damage;
        private float _armourDamage;
        private int _radius;

        public Projectile(string Type, Vector2 Position, Tank Owner, Texture2D Texture, float Rotation, Vector2 Velocity, float Power, bool IsExplosive, float Damage, float ArmourDamage, int Radius)
        {
            this.Type = Type;
            this.Position = Position;
            this.Owner = Owner;
            this.Texture = Texture;
            this.Rotation = Rotation;
            this.Velocity = Velocity;
            this.Power = Power;
            this.IsExplosive = IsExplosive;
            this.Damage = Damage;
            this.ArmourDamage = ArmourDamage;
            this.Radius = Radius;
            Hitbox = Texture.Bounds;
            Hitbox.X = (int)(Position.X - Texture.Width / 2);
            Hitbox.Y = (int)(Position.Y - Texture.Height / 2);
        }
        
        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }


        public float ArmourDamage
        {
            get { return _armourDamage; }
            set { _armourDamage = value; }
        }


        public float Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }


        public bool IsExplosive
        {
            get { return _isExplosive; }
            set { _isExplosive = value; }
        }


        public float Power
        {
            get { return _power; }
            set { _power = value; }
        }


        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }


        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }


        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Tank Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }


        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        
    }
}
