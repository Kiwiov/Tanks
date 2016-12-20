using System;
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
        private static Random rnd = new Random();

        private GraphicsDevice _device;
        private SpriteBatch _spriteBatch;
        private ContentManager _content;

        private RandomObjectManager _randomObjectManager;

        public Texture2D terrainTexture;
        public Texture2D terrainBackgroundTexture;
        private Texture2D treeTexture;
        private Texture2D palmTexture;

        private int[] terrainContour;

        private TreeData[] trees;
        private Color[] ColorData;

        private float flatness = 375;
        private float peakheight = 200;
        private float offset = GameSettings.Height / 2;
        private int currentTime; // for testing only

        public TerrainManager(GraphicsDevice device, ContentManager content, SpriteBatch spriteBatch, RandomObjectManager randomObjectManager)
        {
            _spriteBatch = spriteBatch;
            _content = content;
            _device = device;
            _randomObjectManager = randomObjectManager;

            terrainTexture = new Texture2D(_device, GameSettings.Width, GameSettings.Height, false, SurfaceFormat.Color);
        }

        public void Load(GraphicsDevice device)
        {
            terrainBackgroundTexture = _content.Load<Texture2D>("terrainBackground");

            treeTexture = _content.Load<Texture2D>("tree");
            palmTexture = _content.Load<Texture2D>("palm");
        }

        public void Generate()
        {
            GenerateRandomTerrain();

            // setup trees :)
            var TreeColors = new Color[3];
            TreeColors[0] = Color.RosyBrown;
            TreeColors[1] = Color.Green;
            TreeColors[2] = Color.RoyalBlue;

            trees = new TreeData[3]; // number of trees

            for (int i = 0; i < 3; i++) // number of trees
            {
                trees[i].Color = TreeColors[i];

                trees[i].Position = new Vector2();
                trees[i].Position.X = GameSettings.Width / 4 * (i + 1) * 0.8f + rnd.Next(5, 95);
                trees[i].Position.Y = terrainContour[(int)trees[i].Position.X];
            }

            CreateTerrainGround();
        }

        public void Update(GameTime gameTime)
        {
            currentTime = gameTime.TotalGameTime.Seconds;
        }

        public void Draw(GameTime gameTime)
        {
            if (GameSettings.Debug)
                //TextManager.Draw(gameTime.TotalGameTime.Seconds.ToString(), new Vector2(250, 10), Color.Red);

            _spriteBatch.Draw( terrainTexture, Vector2.Zero, Color.Green);

            // Draw the trees
            foreach (var item in trees)
            {
                int posX = (int)item.Position.X;
                int posY = (int)item.Position.Y;

                var origin = new Vector2(80, 180);

                _spriteBatch.Draw(
                    treeTexture,
                    new Vector2(posX + 20, posY + 5),
                    null,
                    item.Color,
                    0f,
                    origin,
                    0.3f,
                    SpriteEffects.None,
                    0);
            }
        }

        public static double GetRandomDouble(double minimum, double maximum)
        {
            return rnd.NextDouble() * (maximum - minimum) + minimum;
        }
        private void GenerateRandomTerrain()
        {
            terrainContour = new int[GameSettings.Width];

            var rand1 = rnd.NextDouble() + 1;
            var rand2 = rnd.NextDouble() + 2;
            var rand3 = rnd.NextDouble() + 3;

            for (int x = 0; x < GameSettings.Width; x++)
            {
                var height =
                    peakheight / rand1 * Math.Sin(x / flatness * rand1 + rand1)
                    + peakheight / rand2 * Math.Sin(x / flatness * rand2 + rand2)
                    + peakheight / rand3 * Math.Sin(x / flatness * rand3 + rand3)
                    + offset;

                if (height < 0)
                    height = rnd.Next(10, 100);

                terrainContour[x] = (int)height;
            }
        }

        private void CreateTerrainGround()
        {
            var GroundColorData = ConvertTextureToArray(terrainBackgroundTexture);
            ColorData = new Color[GameSettings.Width * GameSettings.Height];

            for (int x = 0; x < GameSettings.Width; x++)
            {
                for (int y = 0; y < GameSettings.Height; y++)
                {
                    if (y > terrainContour[x])
                        ColorData[x + y * GameSettings.Width] = GroundColorData[x % terrainBackgroundTexture.Width, y % terrainBackgroundTexture.Height];
                    else
                        ColorData[x + y * GameSettings.Width] = Color.Transparent;
                }
            }

            terrainTexture.SetData(ColorData);
        }

        private Color[,] ConvertTextureToArray(Texture2D texture)
        {
            var ColorData = new Color[texture.Width * texture.Height];
            var NewColorData = new Color[texture.Width, texture.Height];

            texture.GetData(ColorData);

            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                    NewColorData[x, y] = ColorData[x + y * texture.Width];

            return NewColorData;
        }

        public int FindLand(Vector2 pos)
        {
            int x = (int) MathHelper.Clamp(pos.X, 0, GameSettings.Width - 1);
            int y = (int) MathHelper.Clamp(pos.Y, 0, GameSettings.Height - 1);

            if (ColorData[x + y*GameSettings.Width] == Color.Transparent)
            {
                for (int i = 0; i < GameSettings.Height; i++)
                    if (ColorData[x + i*GameSettings.Width] != Color.Transparent)
                        return i;
                    return GameSettings.Height;
            }
            else
            {
                for (int i = y; i >=0; i--)
                    if (ColorData[x + i *GameSettings.Width] == Color.Transparent)
                        return i;
                    return 0;
            }
        }
        }
    }

