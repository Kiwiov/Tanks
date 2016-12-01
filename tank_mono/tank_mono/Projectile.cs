using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    class Projectile
    {
        private string _type;
        private Vector2 _position;
        private Tank _owner;
        private Texture2D _texture;
        private float _rotation;

        public Projectile(string Type, Vector2 Position, Tank Owner, Texture2D Texture, float Rotation)
        {
            this.Type = Type;
            this.Position = Position;
            this.Owner = Owner;
            this.Texture = Texture;
            this.Rotation = Rotation;
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
