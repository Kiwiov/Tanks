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

namespace tank_mono
{
    class MainMenu
    {
        enum GameState { mainMenum, enterName, inGame, settings}

        private GameState gameState;
        List<menu> main = new List<menu>();
        List<menu> enterName = new List<menu>();
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
        }

        public void LoadContent(ContentManager content)
        {

            sf = content.Load<SpriteFont>("MyFont");
            int moverange = 0;
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
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnClick(string element)
        {
            int moverange = 300;
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
