using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    public struct TreeData
    {
        public Vector2 Position;
        public Color Color;
        public float Angle;
    }
    public class TerrainManager
    {
        private static Random _rnd = new Random();

        private GraphicsDevice _device;
        private SpriteBatch _spriteBatch;
        private ContentManager _content;

        private RandomObjectManager _randomObjectManager;

        public Texture2D TerrainTexture;
        public Texture2D TerrainBackgroundTexture;
        private Texture2D _treeTexture;
        private Texture2D _palmTexture;

        private int[] _terrainContour;

        private TreeData[] _trees;
        private Color[] _colorData;

        private float _flatness = 375;
        private float _peakheight = 200;
        private float _offset = GameSettings.Height * 0.70000f;
        private int _currentTime; // for testing only

        public TerrainManager(GraphicsDevice device, ContentManager content, SpriteBatch spriteBatch, RandomObjectManager randomObjectManager)
        {
            _spriteBatch = spriteBatch;
            _content = content;
            _device = device;
            _randomObjectManager = randomObjectManager;

            TerrainTexture = new Texture2D(_device, GameSettings.Width, GameSettings.Height, false, SurfaceFormat.Color);
        }

        public void Load(GraphicsDevice device)
        {
            TerrainBackgroundTexture = _content.Load<Texture2D>(/*"terrainBackground"*/"Grass");

            _treeTexture = _content.Load<Texture2D>("EverGran");
            _palmTexture = _content.Load<Texture2D>("palm");
        }

        public void Generate()
        {
            GenerateRandomTerrain();

            // setup trees :)
            Color[] treeColors = new Color[4];
            treeColors[0] = Color.Green;
            treeColors[1] = Color.RoyalBlue;
            treeColors[2] = Color.YellowGreen;
            treeColors[3] = Color.DarkSeaGreen;

            _trees = new TreeData[30]; // number of trees
            
            for (int i = 0; i < _trees.Length; i++) // number of trees
            {
                _trees[i].Color = treeColors[i % treeColors.Length];

                _trees[i].Position = new Vector2();
                _trees[i].Position.X = GameSettings.Width / (float)_trees.Length * (i + 1) + _rnd.Next(-50, 50);
                _trees[i].Position.X = MathHelper.Clamp(_trees[i].Position.X, 10, 1910);
                _trees[i].Position.Y = _terrainContour[(int)_trees[i].Position.X];
             }

                CreateTerrainGround();
        }

        public void Update(GameTime gameTime)
        {
            _currentTime = gameTime.TotalGameTime.Seconds;
        }

        public void Draw(GameTime gameTime)
        {
            if (GameSettings.Debug)
                //TextManager.Draw(gameTime.TotalGameTime.Seconds.ToString(), new Vector2(250, 10), Color.Red);

            _spriteBatch.Draw( TerrainTexture, Vector2.Zero, Color.White);

            // Draw the trees
            foreach (var item in _trees)
            {
                int posX = (int)item.Position.X;
                int posY = (int)item.Position.Y;

                
                _spriteBatch.Draw(
                    _treeTexture,
                    new Vector2(posX, posY + 5),
                    null,
                    item.Color,
                    0f,
                    new Vector2(_treeTexture.Width/2 , _treeTexture.Height), 
                    1f,
                    SpriteEffects.None,
                    0);
            }
        }

        public static double GetRandomDouble(double minimum, double maximum)
        {
            return _rnd.NextDouble() * (maximum - minimum) + minimum;
        }
        private void GenerateRandomTerrain()
        {
            _terrainContour = new int[GameSettings.Width];

            var rand1 = _rnd.NextDouble() + 1;
            var rand2 = _rnd.NextDouble() + 2;
            var rand3 = _rnd.NextDouble() + 3;

            for (int x = 0; x < GameSettings.Width; x++)
            {
                var height =
                    _peakheight / rand1 * Math.Sin(x / _flatness * rand1 + rand1)
                    + _peakheight / rand2 * Math.Sin(x / _flatness * rand2 + rand2)
                    + _peakheight / rand3 * Math.Sin(x / _flatness * rand3 + rand3)
                    + _offset;

                if (height < 0)
                    height = _rnd.Next(10, 100);

                _terrainContour[x] = (int)height;
            }
        }

        private void CreateTerrainGround()
        {
            var groundColorData = ConvertTextureToArray(TerrainBackgroundTexture);
            _colorData = new Color[GameSettings.Width * GameSettings.Height];

            for (int x = 0; x < GameSettings.Width; x++)
            {
                for (int y = 0; y < GameSettings.Height; y++)
                {
                    if (y > _terrainContour[x])
                        _colorData[x + y * GameSettings.Width] = groundColorData[x % TerrainBackgroundTexture.Width, y % TerrainBackgroundTexture.Height];
                    else
                        _colorData[x + y * GameSettings.Width] = Color.Transparent;
                }
            }

            TerrainTexture.SetData(_colorData);
        }

        private Color[,] ConvertTextureToArray(Texture2D texture)
        {
            var ColorData = new Color[texture.Width * texture.Height];
            var newColorData = new Color[texture.Width, texture.Height];

            texture.GetData(ColorData);

            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                    newColorData[x, y] = ColorData[x + y * texture.Width];

            return newColorData;
        }

        public int FindLand(Vector2 pos)
        {
            int x = (int) MathHelper.Clamp(pos.X, 0, GameSettings.Width - 1);
            int y = (int) MathHelper.Clamp(pos.Y, 0, GameSettings.Height - 1);

            if (_colorData[x + y*GameSettings.Width] == Color.Transparent)
            {
                for (int i = 0; i < GameSettings.Height; i++)
                    if (_colorData[x + i*GameSettings.Width] != Color.Transparent)
                        return i;
                    return GameSettings.Height;
            }
            else
            {
                for (int i = y; i >=0; i--)
                    if (_colorData[x + i *GameSettings.Width] == Color.Transparent)
                        return i;
                    return 0;
            }
        }
        }
    }

