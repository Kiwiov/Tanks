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
    public class LayerEntity
    {
        public float _scrollX = 0f;
        public float _scrollY = 0f;

        public int Width;
        public int Height;

        private Texture2D _texture;

        public LayerEntity(Texture2D texture)
        {
            _texture = texture;

            Width  = texture.Width;
            Height = texture.Height;
        }

        public void UpdateAxis(float x = 0.5f, float y = 0.5f)
        {
            if (x < 0)
                _scrollX -= (x * -1);
            else
                _scrollX += x;

            if(y < 0)
                _scrollY -= (y * -1);
            else
                _scrollY += y;
        }

        public void ResetAxis()
        {
            _scrollX = 0;
            _scrollY = 0;
        }

        public Texture2D GetTexture()
        {
            return _texture;
        }
    }
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

        public void DrawLayers(GameTime gameTime, params string[] layers)
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
                    _device.Viewport.Width,
                    _device.Viewport.Height),
                    Color.White
                );
            }
        }
    }
}
