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
    public class TerrainManager
    {
        private List<double> HeightMap = new List<double>();

        //Randomerare för slumptal
        private static Random rnd = new Random();

        private Texture2D _texture; //base for the line texture

        private SpriteBatch _spriteBatch;
        private ContentManager _content;

        private List<VectorGroupEntity> Vectors = new List<VectorGroupEntity>();

        private int Iterations = 9;
        private double MaxRandomNess = 1;

        int currentTime; // for testing only

        public TerrainManager(GraphicsDevice device, ContentManager content, SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _content = content;

            _texture = new Texture2D(device, GameSettings.ExtendedWidth, GameSettings.Height);
            Color[] tData = new Color[GameSettings.ExtendedWidth * GameSettings.Height];

            for (int i = 0; i < tData.Length; i++)
            {
                tData[i] = Color.DarkGray;
            }

            _texture.SetData(tData);
        }

        public List<VectorGroupEntity> GetVectorGroup()
        {
            return Vectors;
        }

        public void Load(GraphicsDevice device)
        {
        }

        public void Generate()
        {
            //Nollställer listan med punkter
            HeightMap.Clear();

            //Rensar nuvarande vektorer
            Vectors.Clear();

            //Lägger till start- och stop-punkt
            HeightMap.Add(GetRandomDouble(-0.33, 1.33));
            HeightMap.Add(GetRandomDouble(-0.33, 1.33));

            //Börjar algoritmen med slump +- 1.0
            double Max = MaxRandomNess;

            //Bestämmer ojämnheten i terrängen
            double Roughness = ((double)100 / 100.0);

            //Uprepa enligt angivet antal iterationer
            for (int j = 0; j < Iterations; j++)
            {
                //Räkna ut hur många nya punkter som ska skapas
                int count = HeightMap.Count;
                for (int i = 0; i < count - 1; i++)
                {
                    //Medelvärdet mellan två punkter
                    double tmp = (HeightMap[i * 2] + HeightMap[i * 2 + 1]) / 2.0;
                    //Beräkna en slumpvis förskjutning +-Max
                    double offset = (rnd.NextDouble() * 2.0 - 1.0) * Max; /// 0.85;
                    //Skapa ny punkt med medelvärde + slumvis förskjutning
                    HeightMap.Insert(i * 2 + 1, tmp + offset);
                }
                //Beräkna Max för nästa iteration, avtagande med en faktor 2^(x)
                Max = Max * Math.Pow(2, -Roughness);
            }

            for (int x = 0; x < GameSettings.ExtendedWidth; x++)
            {
                //Beräkna vilken punkt på linjen som ligger närmast
                double index = (x / (double)GameSettings.ExtendedWidth) * (HeightMap.Count - 1);
                int start = (int)Math.Floor(index);

                //Interpolera mellan beräknad punkt och nästa punkt
                double r2 = index - Math.Floor(index);
                double r1 = 1.0 - r2;
                double height = HeightMap[start] * r1 + HeightMap[start + 1] * r2;

                //Ge linjen en offset på 300 pixlar och en amplitud på 150 pixlar

                int randomHeight = Convert.ToInt32(GetRandomDouble(150, 155));
                Vector2 v1 = new Vector2(x, (int)(250 + 350 * height));
                Vector2 v2 = new Vector2(x, 0);

                Vectors.Add(new VectorGroupEntity(v1, v2));
            }
        }

        public void Update(GameTime gameTime)
        {
            currentTime = gameTime.TotalGameTime.Seconds;

            if(currentTime > 0)
                if (currentTime % 4 == 0)
                    this.Generate();
        }

        public static double GetRandomDouble(double minimum, double maximum)
        {
            return rnd.NextDouble() * (maximum - minimum) + minimum;
        }

        public void Draw(GameTime gameTime)
        {
            if(GameSettings.Debug)
                TextManager.Draw(gameTime.TotalGameTime.Seconds.ToString(), new Vector2(10, 10), Color.White);

            foreach (var item in GetVectorGroup())
            {
                var vectors = item.GetAll();

                DrawLine(
                    vectors[0],
                    vectors[1]
                );
            }
        }
        public void DrawLine(Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            _spriteBatch.Draw(_texture,
               new Rectangle(// rectangle defines shape of line and position of start of line
                   (int)start.X,
                   GameSettings.Height,
                   (int)edge.Length(), //this will strech the texture to fill this rectangle
                   1), //width of line, change this to make thicker line
               null,
               Color.DarkGray, //colour of line
               angle,     //angle of line (calulated above)
               new Vector2(2, 1), // point in line about which to rotate
               SpriteEffects.None,
               0);
        }
    }
}
