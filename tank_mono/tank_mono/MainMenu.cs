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
    enum GameState { mainMenum, enterName, inGame, settings }
    class MainMenu : DrawableGameComponent
    {
        private GraphicsDevice graphics;
        

        private GameState gameState;
        List<Menu> main = new List<Menu>();
        List<Menu> enterName = new List<Menu>();
        List<Menu> resolution = new List<Menu>();
        List<Menu> confirm = new List<Menu>();
        List<Menu> volume = new List<Menu>();

        public float Volume { get; set; }

        private Keys[] lastpressedKeys = new Keys[5];
        private string myName = string.Empty;
        private SpriteFont sf;

        public MainMenu(Game game): base (game)
        {
            
            main.Add(new Menu("start"));
            main.Add(new Menu("settings"));
            main.Add(new Menu("quit"));

            enterName.Add(new Menu("name"));
            enterName.Add(new Menu("done"));


            resolution.Add(new Menu("resolution"));
            resolution.Add(new Menu("1920"));
            resolution.Add(new Menu("720"));
            resolution.Add(new Menu("800"));


            volume.Add(new Menu("volume"));
            volume.Add(new Menu("33"));
            volume.Add(new Menu("66"));
            volume.Add(new Menu("100"));
            volume.Add(new Menu("muted"));


            confirm.Add(new Menu("apply"));

        }

        public void loadmenu()
        {
            int moverange = 0;

            int moverangeleft = 0;
            foreach (Menu element in main)
            {
                element.CenterElement(Game1.graphics.PreferredBackBufferWidth / 2, Game1.graphics.PreferredBackBufferHeight/ 2);
            }
            main.Find(x => x.AssetName == "start").MoveElement(0, moverange);
            moverange += (int)(100/1920.0* Game1.graphics.PreferredBackBufferWidth);
            main.Find(x => x.AssetName == "settings").MoveElement(0, moverange);
            moverange += (int)(100 / 1920.0 * Game1.graphics.PreferredBackBufferWidth);
            main.Find(x => x.AssetName == "quit").MoveElement(0, moverange);

            foreach (Menu element in enterName)
            {
                element.CenterElement(Game1.graphics.PreferredBackBufferWidth / 1, Game1.graphics.PreferredBackBufferHeight / 2);
            }

            moverange -= (int)(75 / 1920.0 * Game1.graphics.PreferredBackBufferWidth);
            enterName.Find(x => x.AssetName == "done").MoveElement(0, moverange);

            foreach (Menu element in resolution)
            {
                element.CenterElement(Game1.graphics.PreferredBackBufferWidth / 4, Game1.graphics.PreferredBackBufferHeight / 4);
            }
            moverange = 0;
            resolution.Find(x => x.AssetName == "resolution").MoveElement(moverange, 0);
            moverange += (int)(200 / 1920.0 * Game1.graphics.PreferredBackBufferWidth);
            resolution.Find(x => x.AssetName == "1920").MoveElement(moverange, 0);
            moverange += (int)(200 / 1920.0 * Game1.graphics.PreferredBackBufferWidth); ;
            resolution.Find(x => x.AssetName == "720").MoveElement(moverange, 0);
            moverange += (int)(200 / 1920.0 * Game1.graphics.PreferredBackBufferWidth); ;
            resolution.Find(x => x.AssetName == "800").MoveElement(moverange, 0);

            foreach (Menu element in volume)
            {
                element.CenterElement(Game1.graphics.PreferredBackBufferWidth / 4, Game1.graphics.PreferredBackBufferHeight / 4);
            }
            volume.Find(x => x.AssetName == "volume").MoveElement(0, 75);
            volume.Find(x => x.AssetName == "muted").MoveElement(moverangeleft, 75);
            moverangeleft += (int)(200 / 1920.0 * Game1.graphics.PreferredBackBufferWidth);
            volume.Find(x => x.AssetName == "33").MoveElement(moverangeleft, 75);
            moverangeleft += (int)(200 / 1920.0 * Game1.graphics.PreferredBackBufferWidth);
            volume.Find(x => x.AssetName == "66").MoveElement(moverangeleft, 75);
            moverangeleft += (int)(200 / 1920.0 * Game1.graphics.PreferredBackBufferWidth);
            volume.Find(x => x.AssetName == "100").MoveElement(moverangeleft, 75);


            foreach (Menu element in confirm)
            {
                element.CenterElement(Game1.graphics.PreferredBackBufferWidth / 4, Game1.graphics.PreferredBackBufferHeight / 4);
            }
            confirm.Find(x => x.AssetName == "apply").MoveElement(0, 150);
        }
        public void LoadContent(ContentManager content)
        {
                
            sf = content.Load<SpriteFont>("Menu/MyFont");
            int moverange = 0;
            int moverangeleft = 0;
            foreach (Menu element in main)
            {
                element.LoadContent(content);
                element.CenterElement(Game1.width / 2, Game1.height/ 2);
                element.clickEvent += OnClick;
            }
            main.Find(x => x.AssetName == "start").MoveElement(0, moverange);
            moverange += (int)(100 / 1920.0 * Game1.graphics.PreferredBackBufferWidth);
            main.Find(x => x.AssetName == "settings").MoveElement(0, moverange);
            moverange += (int)(100 / 1920.0 * Game1.graphics.PreferredBackBufferWidth);
            main.Find(x => x.AssetName == "quit").MoveElement(0, moverange);

            foreach (Menu element in enterName)
            {
                element.LoadContent(content);
                element.CenterElement(Game1.width / 4, Game1.height/4);
                element.clickEvent += OnClick;
            }

            moverange -= 75;
            enterName.Find(x => x.AssetName == "done").MoveElement(0, moverange);

            foreach (Menu element in resolution)
            {
                element.LoadContent(content);
                element.CenterElement(Game1.width / 4, Game1.height/4);
                //element.clickEvent += OnClick;

            }
            resolution[1].clickEvent += OnClick1920;
            resolution[2].clickEvent += OnClick1280;
            resolution[3].clickEvent += OnClick800;
            moverange = 0;
            resolution.Find(x => x.AssetName == "resolution").MoveElement(moverange, 0);
            moverange += (int)(200 / 1920.0 * Game1.graphics.PreferredBackBufferWidth);
            resolution.Find(x => x.AssetName == "1920").MoveElement(moverange, 0);
            moverange += (int)(200 / 1920.0 * Game1.graphics.PreferredBackBufferWidth); ;
            resolution.Find(x => x.AssetName == "720").MoveElement(moverange, 0);
            moverange += (int)(200 / 1920.0 * Game1.graphics.PreferredBackBufferWidth); ;
            resolution.Find(x => x.AssetName == "800").MoveElement(moverange, 0);

            foreach (Menu element in volume)
            {
                element.LoadContent(content);
                element.CenterElement(Game1.width / 4, Game1.height / 4);
                element.clickEvent += OnClick;
            }
            volume.Find(x => x.AssetName == "volume").MoveElement(0, 75);
            volume.Find(x => x.AssetName == "muted").MoveElement(moverangeleft, 75);
            moverangeleft += (int)(200 / 1920.0 * Game1.graphics.PreferredBackBufferWidth);
            volume.Find(x => x.AssetName == "33").MoveElement(moverangeleft, 75);
            moverangeleft += (int)(200 / 1920.0 * Game1.graphics.PreferredBackBufferWidth);
            volume.Find(x => x.AssetName == "66").MoveElement(moverangeleft, 75);
            moverangeleft += (int)(200 / 1920.0 * Game1.graphics.PreferredBackBufferWidth);
            volume.Find(x => x.AssetName == "100").MoveElement(moverangeleft, 75);


            foreach (Menu element in confirm)
            {
                element.LoadContent(content);
                element.CenterElement(Game1.width / 4, Game1.height / 4);
                element.clickEvent += OnClick;
            }
            confirm.Find(x => x.AssetName == "apply").MoveElement(0, 150);
        }

        public void RecalcMenu()
        {
            loadmenu();
        }

        public void Update()
        {
            
            switch (gameState)
            {
                case GameState.mainMenum:
                    foreach (Menu element in main)
                    {
                        element.Update();
                    }
                    break;
                case GameState.enterName:
                    foreach (Menu element in enterName)
                    {
                        element.Update();
                    }
                    GetKeys();
                    break;
                case GameState.inGame:
                    break;
                case GameState.settings:
                    foreach (Menu element in resolution)
                    {
                        element.Update();
                    }

                    foreach (Menu element in volume)
                    {
                        element.Update();
                    }

                    foreach (Menu element in confirm)
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
                    foreach (Menu element in main)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.enterName:
                    foreach (Menu element in enterName)
                    {
                        element.Draw(spriteBatch);
                    }
                    spriteBatch.DrawString(sf,myName, new Vector2(450,650), Color.Black,0,Vector2.Zero,0.5f,SpriteEffects.None,0);
                    break;
                case GameState.inGame:
                    break;
                case GameState.settings:
                    foreach (Menu element in resolution)
                    {
                        element.Draw(spriteBatch);
                    }

                    foreach (Menu element in volume)
                    {
                        element.Draw(spriteBatch);
                    }

                    foreach (Menu element in confirm)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnClick1920(string element)
        {

            Game1.graphics.PreferredBackBufferHeight = 1080;
            Game1.graphics.PreferredBackBufferWidth = 1920;
            Game1.graphics.ApplyChanges();
            RecalcMenu();

        }

        public void OnClick1280(string element)
        {
            Game1.graphics.PreferredBackBufferHeight = 720;
            Game1.graphics.PreferredBackBufferWidth = 1280;
            Game1.graphics.ApplyChanges();
            RecalcMenu();


        }

        public void OnClick800(string element)
        {
            Game1.graphics.PreferredBackBufferHeight = 600;
            Game1.graphics.PreferredBackBufferWidth = 800;
            Game1.graphics.ApplyChanges();
            RecalcMenu();
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
