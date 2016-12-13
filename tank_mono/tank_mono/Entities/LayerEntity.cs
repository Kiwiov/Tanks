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
    public class LayerEntity
    {
        public float _scrollX = 0f;
        public float _scrollY = 0f;

        public int Width;
        public int Height;

        private Texture2D _texture;

        public LayerEntity(Texture2D texture)
        {
            _texture = texture;

            Width = texture.Width;
            Height = texture.Height;
        }

        public void UpdateAxis(float x = 0.5f, float y = 0.5f)
        {
            if (x < 0)
                _scrollX -= (x * -1);
            else
                _scrollX += x;

            if (y < 0)
                _scrollY -= (y * -1);
            else
                _scrollY += y;
        }

        public void ResetAxis()
        {
            _scrollX = 0;
            _scrollY = 0;
        }

        public Texture2D GetTexture()
        {
            return _texture;
        }
    }
}
