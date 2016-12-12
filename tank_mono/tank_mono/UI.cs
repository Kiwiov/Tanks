using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    class UI
    {
        private Texture2D texture;
        public Rectangle rectangle;
        private Vector2 position;

        public int health;

        public UI(Texture2D newTexture, Vector2 newPosition, int NewHealth)
        {
            texture = newTexture;
            position = newPosition;

            health = NewHealth;
        }

        public void Update()
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(health > 0)
                spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
