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
    class menu
    {
        private Texture2D GUITexture;
        private Rectangle GUIRect;
        private string assetName;
        private Song song;


        public string AssetName
        {
            get { return assetName; }
            set { assetName = value; }
        }


        public delegate void ElementClicked(string element);

        public event ElementClicked clickEvent;

        public menu(string assetName)
        {
            this.AssetName = assetName;
        }

        public void LoadContent(ContentManager content)
        {
            GUITexture = content.Load<Texture2D>("menu/"+AssetName);
            GUIRect = new Rectangle(0, 0, GUITexture.Width, GUITexture.Height);
            Song song = content.Load<Song>("menu/bgmusic");
            MediaPlayer.Play(song); // this will start the song playing
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 1f;



        }

        public void Update()
        {
            if (GUIRect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                clickEvent(AssetName);

            } 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GUITexture, GUIRect, Color.White);
        }

        public void CenterElement(int height, int width)
        {
            GUIRect = new Rectangle((width/2) - (this.GUITexture.Width/2), (height/2) -(this.GUITexture.Height/2), this.GUITexture.Width, this.GUITexture.Height);
        }

        public void MoveElement(int x, int y)
        {
            GUIRect = new Rectangle(GUIRect.X += x, GUIRect.Y += y, GUIRect.Width, GUIRect.Height);
        }



    }
}
