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
        

        public static GameState gameState;
        List<Menu> main = new List<Menu>();
        List<Menu> enterName = new List<Menu>();
        List<Menu> resolution = new List<Menu>();
        List<Menu> confirm = new List<Menu>();
        List<Menu> volume = new List<Menu>();

        public float Volume { get; set; }

        private Keys[] lastpressedKeys = new Keys[5];
        private string myName = string.Empty;
        private SpriteFont sf;
        SpriteBatch spriteBatch;

        public MainMenu(Game game): base (game)
        {
            
            main.Add(new Menu("start"));
            main.Add(new Menu("settings"));
            main.Add(new Menu("quit"));

            enterName.Add(new Menu("name"));
            enterName.Add(new Menu("done"));
            enterName.Add(new Menu("back"));


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




        public void loadText(SpriteBatch spriteBatch)
        {
            
            spriteBatch.DrawString(sf, myName, new Vector2(450 / 1920.0f * Game1.graphics.PreferredBackBufferWidth, 1050/ 1920.0f * Game1.graphics.PreferredBackBufferHeight), Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
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
                element.CenterElement(Game1.graphics.PreferredBackBufferWidth / 4, Game1.graphics.PreferredBackBufferHeight / 2);
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
            }
            main[0].ClickEvent += onClickStart;
            main[1].ClickEvent += onClicketting;
            main[2].ClickEvent += onClickQuit;
            main.Find(x => x.AssetName == "start").MoveElement(0, moverange);
            moverange += (int)(100 / 1920.0 * Game1.graphics.PreferredBackBufferWidth);
            main.Find(x => x.AssetName == "settings").MoveElement(0, moverange);
            moverange += (int)(100 / 1920.0 * Game1.graphics.PreferredBackBufferWidth);
            main.Find(x => x.AssetName == "quit").MoveElement(0, moverange);

            foreach (Menu element in enterName)
            {
                element.LoadContent(content);
                element.CenterElement(Game1.width / 4, Game1.height/4);
            }
            enterName[1].ClickEvent += onClickDone;
            enterName[2].ClickEvent += onClickBack;
            moverange -= 75;
            enterName.Find(x => x.AssetName == "done").MoveElement(0, moverange);
            moverange += 75;
            enterName.Find(x => x.AssetName == "back").MoveElement(0, moverange);

            foreach (Menu element in resolution)
            {
                element.LoadContent(content);
                element.CenterElement(Game1.width / 4, Game1.height/4);

            }
            resolution[1].ClickEvent += OnClick1920;
            resolution[2].ClickEvent += OnClick1280;
            resolution[3].ClickEvent += OnClick800;
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
                element.ClickEvent += OnClick;
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
                element.ClickEvent += OnClick;
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
                    myName += KeyboardComponent.GetKeyInput();
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
                    loadText(spriteBatch);
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

        public void onClickDone(string element)
        {
            gameState = GameState.inGame;
        }

        public void onClickBack(string element)
        {
            gameState = GameState.mainMenum;
        }
        public void onClickStart(string element)
        {
                gameState = GameState.enterName;
        }

        public void onClicketting(string element)
        {
                gameState = GameState.settings;
        }

        public void onClickQuit(string element)
        {
                Environment.Exit(1);
        }


        public void OnClick(string element)
        {
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

    }
}
