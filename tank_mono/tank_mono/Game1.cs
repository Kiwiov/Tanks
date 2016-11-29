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

        Vector2 position;

        float fuel;
        float speed;
        float acceleration;

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
            position = new Vector2(300,300);
            base.Initialize();
            fuel = 300;
            speed = 0;
            acceleration = 1;
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _tankManager = new TankManager(Content.Load<Texture2D>("TankHeavyBody"), Content.Load<Texture2D>("TankStandardBody"), Content.Load<Texture2D>("TankLightBody"), Content.Load<Texture2D>("TankStandardCannon"));
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

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyUp(Keys.A) && ks.IsKeyUp(Keys.Left) && ks.IsKeyUp(Keys.D) && ks.IsKeyUp(Keys.Right))
            {
                speed = 0;
            }

            if ((ks.IsKeyDown(Keys.Left) | ks.IsKeyDown(Keys.A)) && ks.IsKeyUp(Keys.Right) && ks.IsKeyUp(Keys.D) && fuel > 0)
            {
                if (speed < 30)
                {
                    speed += acceleration;
                }
                position.X -= speed / 10;
                fuel -= 1;
            }
            if ((ks.IsKeyDown(Keys.Right) | ks.IsKeyDown(Keys.D)) && ks.IsKeyUp(Keys.Left) && ks.IsKeyUp(Keys.A) && fuel > 0)
            {

                if (speed < 30)
                {
                    speed += acceleration;
                }
                position.X += speed / 10;
                fuel -= 1;
            }

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

            spriteBatch.Begin();
            spriteBatch.Draw(standardTankMain,position,Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
