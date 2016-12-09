using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    class PickUp
    {
        private Vector2 _position;
        private string _type;
        private Texture2D _texture;
        private Double _rotation;
        private Vector2 _hitboxPosition;
        private Rectangle _hitbox;
        public PickUp(Vector2 Position, string Type, Texture2D Texture)
        {
            this.Position = Position;
            this.Type = Type;
            this.Texture = Texture;
            Hitbox = Texture.Bounds;
            HitboxPosition = Position - new Vector2(Texture.Width/2, Texture.Height/2);
        }

        

        public Rectangle Hitbox
        {
            get { return _hitbox; }
            set { _hitbox = value; }
        }


        public Vector2 HitboxPosition
        {
            get { return _hitboxPosition; }
            set { _hitboxPosition = value; }
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
