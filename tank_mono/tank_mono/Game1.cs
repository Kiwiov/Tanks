using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace tank_mono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private TankManager _tankManager;
        private WeaponCreator _weaponCreator;
        private ProjectileManager _projectileManager;
        private PickUpManager _pickUpManager;

        private BackgroundManager backgroundManager;
        private Tank _currentTank;
        private TerrainManager terrainManager;

        private RandomObjectManager randomObjectManager;

        private ScrollingLayers scrollingLayers;
        private Camera2D camera2D;

        private int previousScrollValue;
        
        bool _done = false;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = GameSettings.Width,
                PreferredBackBufferHeight = GameSettings.Height
            };
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 800;
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
            _projectileManager = new ProjectileManager();
            _weaponCreator = new WeaponCreator(Content.Load<Texture2D>("Projectile"), Content.Load<Texture2D>("Missile"),Content.Load<Texture2D>("AntiArmour"));
            _tankManager = new TankManager(Content.Load<Texture2D>("TankHeavyBody"), Content.Load<Texture2D>("TankStandardBody"), Content.Load<Texture2D>("TankLightBody"), Content.Load<Texture2D>("TankHeavyCannon"), Content.Load<Texture2D>("TankStandardCannon"), Content.Load<Texture2D>("TankLightCannon"),_weaponCreator);
            _pickUpManager = new PickUpManager(Content.Load<Texture2D>("AmmoBox"),Content.Load<Texture2D>("FuelBarrel"));

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TextManager.Init(GraphicsDevice, Content, spriteBatch);

            randomObjectManager = new RandomObjectManager(GraphicsDevice, Content, spriteBatch, terrainManager);

            terrainManager = new TerrainManager(GraphicsDevice, Content, spriteBatch, randomObjectManager);

            scrollingLayers = new ScrollingLayers(GraphicsDevice, Content, spriteBatch);
            scrollingLayers.AddLayer("cloud1");
            scrollingLayers.AddLayer("cloud2");

            TextManager.Load(GraphicsDevice);

            backgroundManager.Load(GraphicsDevice);

            terrainManager.Load(GraphicsDevice);
            terrainManager.Generate();

            randomObjectManager.Load(GraphicsDevice);
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

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();
            var currentMouseState = Mouse.GetState();


            if (keyboardState.IsKeyDown(Keys.G))
                terrainManager.Generate();

            // rotation
            
            if (keyboardState.IsKeyDown(Keys.Q))
                camera2D.Rotation -= deltaTime;

            if (keyboardState.IsKeyDown(Keys.W))
                camera2D.Rotation += deltaTime;

            if (_done == false)
            {
                _tankManager.CreateTank(new Vector2(300,300),"Light",Color.OliveDrab,false);
                _tankManager.SetStats();
                _tankManager.SetWeapons();
                _done = true;
                _currentTank = _tankManager.Tanks[0];
                _pickUpManager.CreatePickup(new Vector2(310, 300), "Fuel");
                _pickUpManager.CreatePickup(new Vector2(320, 300), "Ammo");
            }
            //_tankManager.MapHit(_currentTank.Hitbox, new Rectangle(0, 0, GameSettings.Width, GameSettings.Height),_currentTank,terrainManager);
            _tankManager.MoveTank(_currentTank);
            _tankManager.MoveHitbox(_currentTank);
            _projectileManager.Shoot(_currentTank);
            _projectileManager.MoveProjectiles();
            _pickUpManager.DetectPickup(_currentTank);
            camera2D.Rotation = 0;
            

            /*
             if (keyboardState.IsKeyDown(Keys.Up))
                camera2D.Position -= new Vector2(0, 250) * deltaTime;

             if (keyboardState.IsKeyDown(Keys.Down))
                camera2D.Position += new Vector2(0, 250) * deltaTime;
            */

            if (keyboardState.IsKeyDown(Keys.Left))
                camera2D.Position -= new Vector2(250, 0) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.Right))
                camera2D.Position += new Vector2(250, 0) * deltaTime;


            float thisZoom = camera2D.Zoom;

            if (currentMouseState.ScrollWheelValue < previousScrollValue)
            {
                if ((!GameSettings.Debug && thisZoom > 0.8f) || GameSettings.Debug)
                    camera2D.Zoom -= 0.1f;
            }
            else if (currentMouseState.ScrollWheelValue > previousScrollValue)
            {
                if((!GameSettings.Debug && thisZoom < 1.5f) || GameSettings.Debug)
                    camera2D.Zoom += 0.1f;
            }

            previousScrollValue = currentMouseState.ScrollWheelValue;


            scrollingLayers.Update(gameTime,
                delegate { scrollingLayers.GetLayerByName("cloud1").UpdateAxis(0.8f, 0.35f); }, // todo: fix wind direction
                delegate { scrollingLayers.GetLayerByName("cloud2").UpdateAxis(-0.5f, -0.35f); } // todo: fix wind direction
            );

            terrainManager.Update(gameTime);

            randomObjectManager.Update(gameTime);

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
            

            if (GameSettings.Debug)
                TextManager.Draw("Camera zoom: " + camera2D.Zoom.ToString(), new Vector2(250, 50), Color.Purple);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
