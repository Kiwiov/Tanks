using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    public class RandomObjectEntity
    {
        public int Width;
        public int Height;

        private double _posX = double.NaN;
        private double _posY = double.NaN;

        private Texture2D _texture;

        private Rectangle bounds;
        private Vector2 _position;

        public Color Color = Color.White;
        public RandomObjectEntity(Texture2D texture, object position = null)
        {
            _texture = texture;

            if (position != null)
                SetPosition(position);

            Width = texture.Width;
            Height = texture.Height;
        }

        public object Calculate() // todo..
        {
            return null;
        }

        public void UpdateAxis(float x, float y = float.NaN)
        {
            if (!float.IsNaN(x))
            {
                if (x < 0)
                    _position.X -= (x * -1);
                else
                    _position.X += x;
            }

            if (!float.IsNaN(y))
            {
                if (y < 0)
                    _position.Y -= (y * -1);
                else
                    _position.Y += y;
            }
        }

        public bool SetPosition(object position)
        {
            var data = position.GetType();

            if (data == typeof(Vector2))
                _position = (Vector2)position;
            else if (data == typeof(Point))
            {
                // convert point to vector2 format :p
                _position = new Vector2(
                    (float)data.GetType().GetProperty("X").GetValue(this, null),
                    (float)data.GetType().GetProperty("Y").GetValue(this, null)
                );
            }
            else
                return false;

            return true;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public Rectangle Bounds
        {
            get
            {
                bounds.X = (int)_position.X;
                bounds.Y = (int)_position.Y;
                return bounds;
            }
        }

        public Texture2D GetTexture()
        {
            return _texture;
        }
    }
}