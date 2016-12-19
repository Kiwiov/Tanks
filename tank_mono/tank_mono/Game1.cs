using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;


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
        
        private TankManager _tankManager;
        private WeaponCreator _weaponCreator;
        private ProjectileManager _projectileManager;
        private PickUpManager _pickUpManager;
        private GameLogic _gameLogic;
        private UI _ui;

        private MouseState pastMouse;

        Texture2D background;
        SpriteBatch spriteBatch;
        MainMenu main;
        private BackgroundManager backgroundManager;
        private TerrainManager terrainManager;
        bool _done;
        private RandomObjectManager randomObjectManager;

        private ScrollingLayers scrollingLayers;
        private Camera2D camera2D;

        private int previousScrollValue;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = GameSettings.Width,
                PreferredBackBufferHeight = GameSettings.Height
            };
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Window.Title = GameSettings.Title;
            
             main = new MainMenu(this);
            Components.Add(new KeyboardComponent(this));
            backgroundManager = new BackgroundManager(Content);

            camera2D = new Camera2D(GraphicsDevice);

            previousScrollValue = Mouse.GetState().ScrollWheelValue;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            randomObjectManager = new RandomObjectManager(GraphicsDevice, Content, spriteBatch, terrainManager);

            terrainManager = new TerrainManager(GraphicsDevice, Content, spriteBatch, randomObjectManager);

            scrollingLayers = new ScrollingLayers(GraphicsDevice, Content, spriteBatch);
            scrollingLayers.AddLayer("cloud1");
            scrollingLayers.AddLayer("cloud2");

            TextManager.Init(GraphicsDevice, Content, spriteBatch);
            TextManager.Load(GraphicsDevice);

            backgroundManager.Load(GraphicsDevice);

            terrainManager.Load(GraphicsDevice);
            terrainManager.Generate();

            randomObjectManager.Load(GraphicsDevice);

            _weaponCreator = new WeaponCreator(Content.Load<Texture2D>("Projectile"), Content.Load<Texture2D>("Missile"),Content.Load<Texture2D>("AntiArmour"));
            _tankManager = new TankManager(Content.Load<Texture2D>("TankHeavyBody"), Content.Load<Texture2D>("TankStandardBody"), Content.Load<Texture2D>("TankLightBody"), Content.Load<Texture2D>("TankHeavyCannon"), Content.Load<Texture2D>("TankStandardCannon"), Content.Load<Texture2D>("TankLightCannon"),_weaponCreator, terrainManager);
            _pickUpManager = new PickUpManager(Content.Load<Texture2D>("AmmoBox"),Content.Load<Texture2D>("FuelBarrel"));
            _gameLogic = new GameLogic();
            _projectileManager = new ProjectileManager(_gameLogic, terrainManager);

            main.LoadContent(Content);

            _ui = new UI();
            _ui.LoadContent(Content);
            background = Content.Load<Texture2D>("Menu/bg"); // change these names to the names of your images
            // Create a new SpriteBatch, which can be used to draw textures.


            

           
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
            


            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();
            var currentMouseState = Mouse.GetState();



            if (MainMenu.gameState != GameState.inGame)
            {
                main.Update();
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.G))
                    terrainManager.Generate();

                // rotation

                if (keyboardState.IsKeyDown(Keys.Q))
                    camera2D.Rotation -= deltaTime;

                if (keyboardState.IsKeyDown(Keys.W))
                    camera2D.Rotation += deltaTime;

                if (_done == false)
                {
                    
                    _tankManager.CreateTank(new Vector2(300, 300), "Standard", Color.OliveDrab, false, "Hillarp Persson");
                    _tankManager.CreateTank(new Vector2(500, 300), "Heavy", Color.HotPink, false, "Gudrun Schyman");
                    _tankManager.CreateTank(new Vector2(700, 300), "Light", Color.CadetBlue, false, "Åkesson");
                    _tankManager.SetStats();
                    _tankManager.SetWeapons();
                    _done = true;
                    _gameLogic.CurrentTank = _tankManager.Tanks[0];
                    _pickUpManager.CreatePickup(new Vector2(250, 300), "Fuel");
                    _pickUpManager.CreatePickup(new Vector2(200, 300), "Ammo");
                    _tankManager.MoveHitbox();
                }
                else
                {
                _ui.Update(_gameLogic.CurrentTank);

                    _tankManager.MoveTank(_gameLogic.CurrentTank);
                    _tankManager.FindLandPosition();
                    _tankManager.MoveHitbox();
                    _projectileManager.Shoot(_gameLogic.CurrentTank);
                    _projectileManager.MoveProjectiles();
                    _projectileManager.MoveProjectileHitboxes();
                    _projectileManager.DestroyOrNot();
                    _pickUpManager.DetectPickup(_gameLogic.CurrentTank);
                    _projectileManager.DetectCollisionProjectileTank(_tankManager, _gameLogic.CurrentTank);
                    _gameLogic.ChangeTank(_tankManager);
                    camera2D.Rotation = 0;
                }


                /*
                 if (keyboardState.IsKeyDown(Keys.Up))
                    camera2D.Position -= new Vector2(0, 250) * deltaTime;
    
                 if (keyboardState.IsKeyDown(Keys.Down))
                    camera2D.Position += new Vector2(0, 250) * deltaTime;
                */

                if (keyboardState.IsKeyDown(Keys.Left))
                    camera2D.Position -= new Vector2(250, 0)*deltaTime;

                if (keyboardState.IsKeyDown(Keys.Right))
                    camera2D.Position += new Vector2(250, 0)*deltaTime;


                float thisZoom = camera2D.Zoom;

                if (currentMouseState.ScrollWheelValue < previousScrollValue)
                {
                    if ((!GameSettings.Debug && thisZoom > 0.8f) || GameSettings.Debug)
                        camera2D.Zoom -= 0.1f;
                }
                else if (currentMouseState.ScrollWheelValue > previousScrollValue)
                {
                    if ((!GameSettings.Debug && thisZoom < 1.5f) || GameSettings.Debug)
                        camera2D.Zoom += 0.1f;
                }

                previousScrollValue = currentMouseState.ScrollWheelValue;


                scrollingLayers.Update(gameTime,
                    delegate { scrollingLayers.GetLayerByName("cloud1").UpdateAxis(0.8f, 0.35f); },
                    // todo: fix wind direction
                    delegate { scrollingLayers.GetLayerByName("cloud2").UpdateAxis(-0.5f, -0.35f); }
                    // todo: fix wind direction
                );

                terrainManager.Update(gameTime);

                randomObjectManager.Update(gameTime);

            }
            
            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            //spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null, null, camera2D.GetViewMatrix());
            if (MainMenu.gameState == GameState.inGame)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred,
                    BlendState.AlphaBlend,
                    SamplerState.PointClamp,
                    DepthStencilState.Default,
                    RasterizerState.CullNone);

                backgroundManager.Draw(gameTime, spriteBatch);

                scrollingLayers.Draw(gameTime, "cloud1", "cloud2");

                terrainManager.Draw(gameTime);
                randomObjectManager.Draw(gameTime);

                _projectileManager.Draw(spriteBatch);
                _pickUpManager.Draw(spriteBatch);
                _tankManager.Draw(spriteBatch);

                _ui.Draw(spriteBatch, _gameLogic.CurrentTank);


                if (GameSettings.Debug)
                   //TextManager.Draw("Camera zoom: " + camera2D.Zoom.ToString(), new Vector2(250, 50), Color.Purple);
                spriteBatch.End();
            }


            if (MainMenu.gameState == GameState.mainMenum || MainMenu.gameState == GameState.settings)
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
