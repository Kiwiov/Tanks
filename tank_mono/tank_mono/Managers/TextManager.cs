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
    public static class TextManager
    {
        private static ContentManager _content;
        private static GraphicsDevice _device;
        private static SpriteBatch _spriteBatch;

        private static SpriteFont _font;

        public static void Init(GraphicsDevice device, ContentManager content, SpriteBatch spriteBatch)
        {
            _device = device;
            _content = content;
            _spriteBatch = spriteBatch;
        }

        public static void Load(GraphicsDevice device)
        {
            _font = _content.Load<SpriteFont>("DefaultFont");
        }

        public static void Draw(string text, Vector2 position, Color color)
        {
            
            _spriteBatch.DrawString(_font, text, position, color);
        }
    }
}
