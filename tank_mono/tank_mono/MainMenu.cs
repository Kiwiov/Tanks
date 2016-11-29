using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace tank_mono
{
    class MainMenu
    {
        private GraphicsDeviceManager graphics;
        enum GameState { mainMenum, enterName, inGame, settings}

        private GameState gameState;
        List<menu> main = new List<menu>();
        List<menu> enterName = new List<menu>();
        List<menu> resolution = new List<menu>();
        List<menu> confirm = new List<menu>();
        List<menu> volume = new List<menu>();

        public float Volume { get; set; }
        public Game1 game; //Reference to your main class

        private Keys[] lastpressedKeys = new Keys[5];
        private string myName = string.Empty;
        private SpriteFont sf;

        public MainMenu()
        {
            main.Add(new menu("start"));
            main.Add(new menu("settings"));
            main.Add(new menu("quit"));

            enterName.Add(new menu("name"));
            enterName.Add(new menu("done"));


            resolution.Add(new menu("resolution"));
            resolution.Add(new menu("1920"));
            resolution.Add(new menu("720"));
            resolution.Add(new menu("800"));


            volume.Add(new menu("volume"));
            volume.Add(new menu("33"));
            volume.Add(new menu("66"));
            volume.Add(new menu("100"));
            volume.Add(new menu("muted"));


            confirm.Add(new menu("apply"));
        }

        public void LoadContent(ContentManager content)
        {
            
            sf = content.Load<SpriteFont>("menu/MyFont");
            int moverange = 0;
            int moverangeleft = 200;
            foreach (menu element in main)
            {
                element.LoadContent(content);
                element.CenterElement(960, 1080);
                element.clickEvent += OnClick;
            }
            main.Find(x => x.AssetName == "start").MoveElement(0, moverange);
            moverange += 100;
            main.Find(x => x.AssetName == "settings").MoveElement(0, moverange);
            moverange += 100;
            main.Find(x => x.AssetName == "quit").MoveElement(0, moverange);

            foreach (menu element in enterName)
            {
                element.LoadContent(content);
                element.CenterElement(960, 1080);
                element.clickEvent += OnClick;
            }

            moverange -= 75;
            enterName.Find(x => x.AssetName == "done").MoveElement(0, moverange);



            foreach (menu element in resolution)
            {
                element.LoadContent(content);
                element.CenterElement(960, 1080);
                element.clickEvent += OnClick;
            }
            moverange = 0;
            resolution.Find(x => x.AssetName == "resolution").MoveElement(moverange, 0);
            moverange += 200;
            resolution.Find(x => x.AssetName == "1920").MoveElement(moverange, 0);
            moverange += 200;
            resolution.Find(x => x.AssetName == "720").MoveElement(moverange, 0);
            moverange += 200;
            resolution.Find(x => x.AssetName == "800").MoveElement(moverange, 0);

            foreach (menu element in volume)
            {
                element.LoadContent(content);
                element.CenterElement(960, 1080);
                element.clickEvent += OnClick;
            }
            volume.Find(x => x.AssetName == "volume").MoveElement(0, 75);
            volume.Find(x => x.AssetName == "muted").MoveElement(moverangeleft, 75);
            moverangeleft += 200;
            volume.Find(x => x.AssetName == "33").MoveElement(moverangeleft, 75);
            moverangeleft += 200;
            volume.Find(x => x.AssetName == "66").MoveElement(moverangeleft, 75);
            moverangeleft += 200;
            volume.Find(x => x.AssetName == "100").MoveElement(moverangeleft, 75);


            foreach (menu element in confirm)
            {
                element.LoadContent(content);
                element.CenterElement(960, 1080);
                element.clickEvent += OnClick;
            }
            confirm.Find(x => x.AssetName == "apply").MoveElement(0, 150);


        }

        public void Update()
        {
            switch (gameState)
            {
                case GameState.mainMenum:
                    foreach (menu element in main)
                    {
                        element.Update();
                    }
                    break;
                case GameState.enterName:
                    foreach (menu element in enterName)
                    {
                        element.Update();
                    }
                    GetKeys();
                    break;
                case GameState.inGame:
                    break;
                case GameState.settings:
                    foreach (menu element in resolution)
                    {
                        element.Update();
                    }

                    foreach (menu element in volume)
                    {
                        element.Update();
                    }

                    foreach (menu element in confirm)
                    {
                        element.Update();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }





        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (gameState)
            {
                case GameState.mainMenum:
                    foreach (menu element in main)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.enterName:
                    foreach (menu element in enterName)
                    {
                        element.Draw(spriteBatch);
                    }
                    spriteBatch.DrawString(sf,myName, new Vector2(450,650), Color.Black);
                    break;
                case GameState.inGame:
                    break;
                case GameState.settings:
                    foreach (menu element in resolution)
                    {
                        element.Draw(spriteBatch);
                    }

                    foreach (menu element in volume)
                    {
                        element.Draw(spriteBatch);
                    }

                    foreach (menu element in confirm)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnClick(string element)
        {
            if (element == "start")
            {
                gameState = GameState.enterName;
                
            }

            if (element == "settings")
            {
                gameState = GameState.settings;
            }

            if (element == "done")
            {
                gameState = GameState.inGame;
            }

            if (element == "quit")
            {
                System.Environment.Exit(1);
            }

            if (element == "muted")
            {

                MediaPlayer.Volume = 0;

            }

            if (element == "33")
            {

                MediaPlayer.Volume = 0.33f;

            }

            if (element == "66")
            {
                MediaPlayer.Volume = 0.66f;
            }

            if (element == "100")
            {
                MediaPlayer.Volume = 1f;
            }

            if (element == "1080")
            {
                Game1.graphics.PreferredBackBufferHeight = 1080;
                Game1.graphics.PreferredBackBufferWidth = 1920;
                Game1.graphics.ApplyChanges();

            }

            if (element == "720")
            {
                Game1.graphics.PreferredBackBufferHeight = 720;
                Game1.graphics.PreferredBackBufferWidth = 1280;
                Game1.graphics.ApplyChanges();
            }

            if (element == "800")
            {
                Game1.graphics.PreferredBackBufferHeight = 600;
                Game1.graphics.PreferredBackBufferWidth = 800;
                Game1.graphics.ApplyChanges();
            }

            if (element == "apply")
            {
                gameState = GameState.mainMenum;
            }
        }

        public void GetKeys()
        {
            KeyboardState kbState = Keyboard.GetState();

            Keys[] pressedKeys = kbState.GetPressedKeys();

            foreach (Keys key in lastpressedKeys)
            {
                if (!pressedKeys.Contains(key))
                {
                    //Key is no longer pressed
                    OnKeyUp(key);
                }
            }

            foreach (Keys key in pressedKeys)
            {
                if (!lastpressedKeys.Contains(key))
                {
                    OnKeyDown(key);
                }
            }
            lastpressedKeys = pressedKeys;
        }

        public void OnKeyUp(Keys key)
        {

        }

        public void OnKeyDown(Keys key)
        {
            if (key != Keys.Back || key == Keys.Space)
            {

            }

            if (key == Keys.Back && myName.Length > 0)
            {
                myName = myName.Remove(myName.Length - 1);
            }
            else
            {
                if (myName.Length <10)
                {
                    myName += key.ToString();
                }

            }
        }
    }
}
