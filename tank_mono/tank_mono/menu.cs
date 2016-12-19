using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace tank_mono
{
    class Menu
    {
        private Texture2D temp;
        private Texture2D GUITexture;
        private Rectangle GUIRect;
        private string assetName;
        private Song song;
        private float scale = 0f;
        private MouseState previousMouseState;

        public string AssetName
        {
            get { return assetName; }
            set { assetName = value; }
        }

        public bool isActive { get; set; }


        public delegate void ElementClicked(string element);

        public event ElementClicked ClickEvent;

        public Menu(string assetName)
        {
            this.AssetName = assetName;
            isActive = true;
        }

        public void LoadContent(ContentManager content)
        {
            temp = content.Load<Texture2D>("Menu/temp");
            GUITexture = content.Load<Texture2D>("Menu/"+AssetName);
            GUIRect = new Rectangle(0, 0, GUITexture.Width, GUITexture.Height);
            song = content.Load<Song>(/*"Menu/bgmusic"*/"Menu/StormVind");
            MediaPlayer.Play(song); // this will start the song playing
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 1f;
        }

        public void Update()
        {
            var mouseState = Mouse.GetState();
            if (GUIRect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                ClickEvent?.Invoke(AssetName);
            }
            previousMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            scale = Game1.graphics.PreferredBackBufferWidth / 1500f;

            spriteBatch.Draw(GUITexture, new Vector2(GUIRect.X, GUIRect.Y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
#if DEBUG
            spriteBatch.Draw(temp, GUIRect, new Color(155, 155, 155, 0));
#endif

        }

        public void CenterElement(int width, int height)
        {
            //TODO: LÖSNING WIDTH 42, ska fixas i framtiden
            GUIRect = new Rectangle((width) - (GUITexture.Width/2), (height) -(GUITexture.Height/2), GUITexture.Width +42, GUITexture.Height);
        }

        public void MoveElement(int x, int y)
        {
            GUIRect = new Rectangle(GUIRect.X += x, GUIRect.Y += y, (int)(GUIRect.Width / 1920.0 * Game1.graphics.PreferredBackBufferWidth), (int)(GUIRect.Height / 1080.0 * Game1.graphics.PreferredBackBufferHeight));
        }



    }
}
