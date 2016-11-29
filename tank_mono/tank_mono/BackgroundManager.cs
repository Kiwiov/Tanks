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
    class BackgroundManager
    {
        private Dictionary<string, Texture2D> _backgrounds = new Dictionary<string, Texture2D>();
        private ContentManager _content;
        public Texture2D CurrentBackground { get; private set; }
        public BackgroundManager(ContentManager Content)
        {
            _content = Content;
        }

        public void Load(GraphicsDevice Device)
        {
            Register("Mountain", "MountainBackground");
            Register("Snow", "SnowBackground");
            Register("Desert", "SandBackground");
        }

        private void Register(string key, string background)
        {
            _backgrounds.Add(key, _content.Load<Texture2D>(background));
        }

        public Texture2D GetThemeBackground()
        {
            return _backgrounds[GameSettings.Theme];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var dest = new Rectangle(0, 0, GameSettings.Width, GameSettings.Height);
            spriteBatch.Draw(GetThemeBackground(), Vector2.Zero, dest, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        public void Unload(GraphicsDevice Device, object Source)
        {
            // TODO: unloada skitet
        }
    }
}
