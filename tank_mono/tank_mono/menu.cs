﻿using System;
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


        public delegate void ElementClicked(string element);

        public event ElementClicked clickEvent;

        public Menu(string assetName)
        {
            this.AssetName = assetName;
        }

        public void LoadContent(ContentManager content)
        {
            temp = content.Load<Texture2D>("Menu/temp");
            GUITexture = content.Load<Texture2D>("Menu/"+AssetName);
            GUIRect = new Rectangle(0, 0, GUITexture.Width, GUITexture.Height);
            song = content.Load<Song>("Menu/bgmusic");
            MediaPlayer.Play(song); // this will start the song playing
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 1f;
        }

        public void Update()
        {
            var mouseState = Mouse.GetState();
            if (GUIRect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                clickEvent(AssetName);
            }
            previousMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            scale = Game1.graphics.PreferredBackBufferWidth / 1500f;
            //spriteBatch.Draw(temp, GUIRect, Color.White);
            spriteBatch.Draw(GUITexture, new Vector2(GUIRect.X, GUIRect.Y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);

        }

        public void CenterElement(int width, int height)
        {
            GUIRect = new Rectangle((width) - (GUITexture.Width/2), (height) -(GUITexture.Height/2), GUITexture.Width, GUITexture.Height);
        }

        public void MoveElement(int x, int y)
        {
            GUIRect = new Rectangle(GUIRect.X += x, GUIRect.Y += y, (int)(GUIRect.Width / 1920.0 * Game1.graphics.PreferredBackBufferWidth), (int)(GUIRect.Height / 1080.0 * Game1.graphics.PreferredBackBufferHeight));
        }



    }
}
