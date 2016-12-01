using System;
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

        Texture2D _bkgrnd;

        TankManager _tankManager;
        Tank _currentTank;
        Tank _secondTank;

        Rectangle _norm;
        Rectangle _koll;
        Rectangle _coll;
        Rectangle player1box;
        Rectangle _r1;
        Rectangle _r2;

        Vector2 _bkPos;
        
        bool _falling;
        bool _bHit = false;
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
            _bkPos = new Vector2(200,400);
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
            _bkgrnd = Content.Load<Texture2D>("BackGroundTest");
            _tankManager = new TankManager(Content.Load<Texture2D>("TankHeavyBody"), Content.Load<Texture2D>("TankStandardBody"), Content.Load<Texture2D>("TankLightBody"), Content.Load<Texture2D>("TankHeavyCannon"), Content.Load<Texture2D>("TankStandardCannon"), Content.Load<Texture2D>("TankLightCannon"));
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
                _tankManager.CreateTank(new Vector2(250,300),"Standard",Color.OliveDrab,false);
                _tankManager.CreateTank(new Vector2(300, 315), "Heavy", Color.AliceBlue, false);
                _tankManager.SetStats();
                _done = true;
                _currentTank = _tankManager.Tanks[0];
                _secondTank = _tankManager.Tanks[1];
            }
            Debug.WriteLine("Cannon Rotation: " + _currentTank.CannonRotation);
            //if (!_falling)
            //{
            //    _tankManager.MoveTank(_currentTank);
            //}
            _tankManager.MoveTank(_currentTank);

            Rectangle player1Box = new Rectangle((int)_currentTank.Position.X - _currentTank.SpriteMain.Width/2, (int)_currentTank.Position.Y, _currentTank.SpriteMain.Width, _currentTank.SpriteMain.Height/2);
            Rectangle player2Box = new Rectangle((int)_secondTank.Position.X, (int)_secondTank.Position.Y, _bkgrnd.Width, _bkgrnd.Height);
            Rectangle mapBox = new Rectangle((int)_bkPos.X, (int)_bkPos.Y, _bkgrnd.Width,_bkgrnd.Height);

            _koll = Collision.Intersection(player1Box, player2Box);
            var _coll = Collision.Intersection(player1Box, mapBox);

            if (_coll.Width > 0 && _coll.Height > 0)
            {
                Rectangle _r1 = Collision.Normalize(player1Box, _coll);
                Rectangle _r2 = Collision.Normalize(mapBox, _coll);
                _bHit = Collision.TestCollision(_currentTank.SpriteMain, _r1, _bkgrnd, _r2);
            }
            else
            {
                _bHit = false;
            }
            if (!_bHit)
            {
                _currentTank.Position.Y += 1f;
                _falling = true;
            }
            else
            {
                _currentTank.Position.Y -= 1f;
                _falling = false;
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

            spriteBatch.Begin(SpriteSortMode.Deferred,
                                BlendState.AlphaBlend,
                                SamplerState.PointClamp,
                                DepthStencilState.Default,
                                RasterizerState.CullNone);
            spriteBatch.Draw(_bkgrnd,_bkPos,Color.White);
            _tankManager.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
