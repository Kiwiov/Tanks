using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    public class PickUp
    {
        private Vector2 _position;
        private string _type;
        private Texture2D _texture;
        private Double _rotation;
        public Rectangle Hitbox;
        public PickUp(Vector2 Position, string Type, Texture2D Texture)
        {
            this.Position = Position;
            this.Type = Type;
            this.Texture = Texture;
            Hitbox = Texture.Bounds;
            Hitbox.X = (int)(Position.X - Texture.Width / 2);
            Hitbox.Y = (int)(Position.Y - Texture.Height / 2);
        }
        
        public Double Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }


        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Vector2  Position
        {
            get { return _position; }
            set { _position = value; }
        }

    }
}
