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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Texture2D standardTankMain;
        public Texture2D standardTankCannon;

        public Texture2D backgroundImage;

        private BackgroundManager backgroundManager;
        private TerrainManager terrainManager;

        private ScrollingLayers scrollingLayers;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GameSettings.Width;
            graphics.PreferredBackBufferHeight = GameSettings.Height;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            backgroundManager = new BackgroundManager(Content);
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
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            terrainManager = new TerrainManager(GraphicsDevice, Content, spriteBatch);

            scrollingLayers = new ScrollingLayers(GraphicsDevice, Content, spriteBatch);

            scrollingLayers.AddLayer("cloud1");
            scrollingLayers.AddLayer("cloud2");

            standardTankMain = Content.Load<Texture2D>("TankStandardBody");
            standardTankCannon = Content.Load<Texture2D>("TankStandardCannon");

            backgroundManager.Load(GraphicsDevice);
            backgroundImage = backgroundManager.GetThemeBackground();

            terrainManager.Load(GraphicsDevice);
            terrainManager.Generate();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            scrollingLayers.Update(gameTime,
                delegate { scrollingLayers.GetLayerByName("cloud1").UpdateAxis(0.8f, 0.35f); },
                delegate { scrollingLayers.GetLayerByName("cloud2").UpdateAxis(-0.5f, -0.35f); }
            );

            terrainManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);

            backgroundManager.Draw(gameTime, spriteBatch);

            scrollingLayers.DrawLayers(gameTime, "cloud1", "cloud2");

            terrainManager.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
