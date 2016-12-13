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
    public class ScrollingLayers
    {
        private GraphicsDevice _device;
        private SpriteBatch _spriteBatch;
        private ContentManager _content;

        private Dictionary<string, LayerEntity> _layers = new Dictionary<string, LayerEntity>();

        public ScrollingLayers(GraphicsDevice device, ContentManager content, SpriteBatch spriteBatch)
        {
            _device = device;
            _content = content;
            _spriteBatch = spriteBatch;
        }

        public LayerEntity AddLayer(string assetName)
        {
            _layers.Add(
                assetName,
                new LayerEntity(_content.Load<Texture2D>(assetName))
            );

            return GetLayerByName(assetName);
        }

        public Dictionary<string, LayerEntity> GetLayers()
        {
            return _layers;
        }

        public LayerEntity GetLayerByName(string assetName)
        {
            return _layers[assetName];
        }

        public void Update(GameTime gameTime, params Action[] specificLayers)
        {
            foreach(Action _delegate in specificLayers)
                _delegate();
        }

        public void Draw(GameTime gameTime, params string[] layers)
        {
            foreach(var layer in layers)
            {
                var _layer = GetLayerByName(layer);

                _spriteBatch.Draw
                    (_layer.GetTexture(), 
                    Vector2.Zero,
                    new Rectangle(
                    (int) (-_layer._scrollX),
                    (int) (-_layer._scrollY),
                    GameSettings.Width,
                    _device.Viewport.Height),
                    Color.White
                );
            }
        }
    }
}
