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
    public class RandomObjectManager
    {
        private Random rnd = new Random();

        private GraphicsDevice _device;
        private SpriteBatch _spriteBatch;
        private ContentManager _content;

        private TerrainManager _terrainManager;

        private Dictionary<string, RandomObjectEntity> _objects = new Dictionary<string, RandomObjectEntity>();

        public RandomObjectManager(GraphicsDevice device, ContentManager content, SpriteBatch spriteBatch, TerrainManager terrainManager)
        {
            _device = device;
            _content = content;
            _spriteBatch = spriteBatch;
            _terrainManager = terrainManager;
        }

        public void Load(GraphicsDevice device)
        {
            //AddObject("palm", new Vector2(300, 300));
        }

        public void Update(GameTime gameTime)
        {
            //GetObjectByName("palm").UpdateAxis(0.1f, -0.15f);
        }

        public void Draw(GameTime gameTime)
        {
            foreach(var item in GetObjectsList())
            {
                _spriteBatch.Draw(item.GetTexture(), item.GetPosition(), item.Color);
            }
        }

        public RandomObjectEntity AddObject(string assetName, object position = null)
        {
            _objects.Add(
                assetName,
                new RandomObjectEntity(_content.Load<Texture2D>(assetName), position)
            );

            return GetObjectByName(assetName);
        }

        public Dictionary<string, RandomObjectEntity> GetObjects()
        {
            return _objects;
        }

        public List<RandomObjectEntity> GetObjectsList()
        {
            return _objects.Values.ToList();
        }

        public RandomObjectEntity GetObjectByName(string assetName)
        {
            return _objects[assetName];
        }
    }
}
