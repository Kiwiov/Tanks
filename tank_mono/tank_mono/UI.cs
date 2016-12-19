using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    class UI
    {
        //UI BAR
        private Texture2D healthTexture;
        private Rectangle healthRectangle;
        private Texture2D fuelTexture;
        private Rectangle fuelRectangle;
        private Texture2D windTextureLeft;
        private Texture2D windTextureRight;
        private Rectangle windRectangle;
        private SpriteFont fontLoader;
        private SpriteFont weaponAmmo;
        public Rectangle rectangle;
        private Vector2 position;
        private GameLogic gameLogic;


        public UI(GameLogic logic)
        {
            gameLogic = logic;
        }

        public void LoadContent(ContentManager content)
        {

            fontLoader = content.Load<SpriteFont>("Menu/MyFont");
            healthTexture = content.Load<Texture2D>("health");
            fuelTexture = content.Load<Texture2D>("fuel");
            if(gameLogic.Wind < 0)
                windTextureLeft = content.Load<Texture2D>("windleft");
            if (gameLogic.Wind > 0)
                windTextureRight = content.Load<Texture2D>("windright");

        }

        public void Update(Tank _currentTank)
        {
            if (MainMenu.gameState == GameState.inGame)
            {
                healthRectangle = new Rectangle(130, 20, (int)(100 * (_currentTank.CurrentHealth / _currentTank.Health)), 20);
                fuelRectangle = new Rectangle(130, 45, (int)(100 * (_currentTank.CurrentFuel / _currentTank.Fuel)), 20);

                if (gameLogic.Wind > 0)
                {

                    windRectangle = new Rectangle(180, 135, 50, 20);
                }

                if (gameLogic.Wind < 0)
                {

                    windRectangle = new Rectangle(180, 135, 50, 20);
                }
            }

            //rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch, Tank _currentTank)
        {
            if (_currentTank == null)
                return;
            //if(health > 0)
            //    spriteBatch.Draw(texture, rectangle, Color.White);

            //if (fuel > 0)
            //    spriteBatch.Draw(texture, rectangle, Color.White);

            spriteBatch.DrawString(fontLoader, "Health: ", new Vector2(50, 15), Color.BlueViolet, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(fontLoader, "Fuel: ", new Vector2(50, 45), Color.BlueViolet, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(fontLoader, "Gun : ", new Vector2(50, 75), Color.BlueViolet, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(fontLoader, "Ammo : ", new Vector2(50, 105), Color.BlueViolet, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(fontLoader, "Wind : ", new Vector2(50, 135), Color.BlueViolet, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(fontLoader, gameLogic.Wind.ToString(), new Vector2(130, 135), Color.BlueViolet, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);

            spriteBatch.DrawString(fontLoader, _currentTank.CurrentWeapon.Name, new Vector2(130, 75), Color.BlueViolet, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            if (_currentTank.CurrentWeapon.Name == "MachineGun")
            {
                spriteBatch.DrawString(fontLoader, "Inf.", new Vector2(130, 105), Color.BlueViolet, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.DrawString(fontLoader, _currentTank.CurrentWeapon.CurrentAmmo.ToString(), new Vector2(130, 105), Color.BlueViolet, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            }


            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);
            spriteBatch.Draw(fuelTexture, fuelRectangle, Color.White);
            if (gameLogic.Wind < 0)
                spriteBatch.Draw(windTextureLeft, windRectangle, Color.White);
            if (gameLogic.Wind > 0)
                spriteBatch.Draw(windTextureRight, windRectangle, Color.White);
        }
    }
}
