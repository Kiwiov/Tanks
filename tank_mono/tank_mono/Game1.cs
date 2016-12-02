using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tank_mono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TankManager _tankManager;
        WeaponCreator _weaponCreator;
        ProjectileManager _projectileManager;
        Tank _currentTank;
        
        bool _done = false;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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
            _projectileManager = new ProjectileManager();
            _weaponCreator = new WeaponCreator(Content.Load<Texture2D>("Projectile"), Content.Load<Texture2D>("Missile"),Content.Load<Texture2D>("AntiArmour"));
            _tankManager = new TankManager(Content.Load<Texture2D>("TankHeavyBody"), Content.Load<Texture2D>("TankStandardBody"), Content.Load<Texture2D>("TankLightBody"), Content.Load<Texture2D>("TankHeavyCannon"), Content.Load<Texture2D>("TankStandardCannon"), Content.Load<Texture2D>("TankLightCannon"),_weaponCreator);
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

            if (_done == false)
            {
                _tankManager.CreateTank(new Vector2(300,300),"Light",Color.OliveDrab,false);
                _tankManager.SetStats();
                _tankManager.SetWeapons();
                _done = true;
                _currentTank = _tankManager.Tanks[0];
            }
            Debug.WriteLine("Cannon Rotation: " + _currentTank.CannonRotation);
            _tankManager.MoveTank(_currentTank);
            _projectileManager.Shoot(_currentTank);

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

            spriteBatch.Begin(SpriteSortMode.Deferred,
                                BlendState.AlphaBlend,
                                SamplerState.PointClamp,
                                DepthStencilState.Default,
                                RasterizerState.CullNone);
            _tankManager.Draw(spriteBatch);
            _projectileManager.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
