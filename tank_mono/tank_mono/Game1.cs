using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace tank_mono
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        public static int width = 1920;
        public static int height = 1080;

        private UI ui;


        //UI BAR
        private Texture2D healthTexture;
        private Rectangle healthRectangle;
        private Texture2D fuelTexture;
        private Rectangle fuelRectangle;
        private Texture2D weaponTexture;
        private Rectangle weaponRectangle;

        private MouseState pastMouse;

        Texture2D background;
        SpriteBatch spriteBatch;
        MainMenu main;
        TankManager _tankManager;
        WeaponCreator _weaponCreator;
        Tank _currentTank;
        bool _done;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
             main = new MainMenu(this);
            Components.Add(new KeyboardComponent(this));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            IsMouseVisible = true;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _weaponCreator = new WeaponCreator(Content.Load<Texture2D>("Projectile"), Content.Load<Texture2D>("Missile"),Content.Load<Texture2D>("AntiArmour"));
            _tankManager = new TankManager(Content.Load<Texture2D>("TankHeavyBody"), Content.Load<Texture2D>("TankStandardBody"), Content.Load<Texture2D>("TankLightBody"), Content.Load<Texture2D>("TankHeavyCannon"), Content.Load<Texture2D>("TankStandardCannon"), Content.Load<Texture2D>("TankLightCannon"),_weaponCreator);
            main.LoadContent(Content);
            

            ui = new UI(Content.Load<Texture2D>("TankHeavyBody"), new Vector2(400,400), 100);

            healthTexture = Content.Load<Texture2D>("health");
            background = Content.Load<Texture2D>("Menu/bg"); // change these names to the names of your images
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            healthRectangle = new Rectangle(50, 20, (int)_currentTank.Health, 20);

            ui.Update();
            pastMouse = mouse;
            base.Update(gameTime);


            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;


            if (MainMenu.gameState != GameState.inGame)
            {
                main.Update();
            }
            else
            {
                if (_done == false)
                {
                    _tankManager.CreateTank(new Vector2(300, 300), "Heavy", Color.OliveDrab, false);
                    _tankManager.SetStats();
                    _done = true;
                    _currentTank = _tankManager.Tanks[0];
                }
                Debug.WriteLine("Cannon Rotation: " + _currentTank.CannonRotation);
                _tankManager.MoveTank(_currentTank);
            }
            base.Update(gameTime);

            if (mouse.MiddleButton == ButtonState.Pressed && pastMouse.MiddleButton == ButtonState.Released)
            {
                _currentTank.Health -= 10;

            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            if (MainMenu.gameState == GameState.inGame)
            {

                spriteBatch.Begin();
                spriteBatch.Draw(healthTexture, healthRectangle, Color.White);
                ui.Draw(spriteBatch);
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.Deferred,
                          BlendState.AlphaBlend,
                          SamplerState.PointClamp,
                          DepthStencilState.Default,
                          RasterizerState.CullNone);
                
                _tankManager.Draw(spriteBatch);
                spriteBatch.End();


            }

            if (MainMenu.gameState == GameState.mainMenum || MainMenu.gameState == GameState.enterName || MainMenu.gameState == GameState.settings)
            {

                spriteBatch.Begin();
                spriteBatch.Draw(background, new Rectangle(0, 0, width, height), Color.White);
                main.Draw(spriteBatch);
                spriteBatch.End();

            }

            base.Draw(gameTime);
        }
        public void Quit()
        {
            this.Exit();
        }
    }
}
