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
    public class VectorGroup
    {
        private Vector2 v1;
        private Vector2 v2;

        public VectorGroup(Vector2 v1, Vector2 v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public Vector2 GetFirst()
        {
            return v1;
        }

        public Vector2 GetSecond()
        {
            return v2;
        }
    }
    public class TerrainManager
    {
        private List<double> HeightMap = new List<double>();

        //Randomerare för slumptal
        private Random rnd = new Random();

        private Texture2D _texture; //base for the line texture

        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private ContentManager _content;

        internal List<VectorGroup> Vectors = new List<VectorGroup>();

        private int Iterations = 5;

        int currentTime; // for testing only

        public TerrainManager(GraphicsDevice device, ContentManager content, SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _content = content;

            _texture = new Texture2D(device, GameSettings.Width, GameSettings.Height);

            Color[] tData = new Color[GameSettings.Width * GameSettings.Height];

            for (int i = 0; i < tData.Length; i++)
            {
                tData[i] = Color.DarkGray;
            }

            _texture.SetData(tData);
        }

        public List<VectorGroup> GetVectorGroup()
        {
            return Vectors;
        }

        public void Load(GraphicsDevice device)
        {
            _font = _content.Load<SpriteFont>("TimerFont");
        }

        public void Generate()
        {
            //Nollställer listan med punkter
            HeightMap.Clear();

            //Lägger till start- och stop-punkt
            HeightMap.Add(GetRandomDouble(-0.89, 1.666));
            HeightMap.Add(GetRandomDouble(-0.89, 1.666));

            //Börjar algoritmen med slump +- 1.0
            double Max = GetRandomDouble(-1, 2);

            //Bestämmer ojämnheten i terrängen
            double Roughness = ((double)16.666 / 100.0);

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
                    double offset = (rnd.NextDouble() * 2.0 - 1.0) * Max;

                    //Skapa ny punkt med medelvärde + slumvis förskjutning
                    HeightMap.Insert(i * 2 + 1, tmp + offset);
                }
                //Beräkna Max för nästa iteration, avtagande med en faktor 2^(x)
                Max = Max * Math.Pow(2, -Roughness);
            }

            Vectors.Clear();

            for (int x = 0; x < GameSettings.Width; x++)
            {
                //Beräkna vilken punkt på linjen som ligger närmast
                double index = (x / (double)GameSettings.Width) * (HeightMap.Count - 1);
                int start = (int)Math.Floor(index);

                //Interpolera mellan beräknad punkt och nästa punkt
                double r2 = index - Math.Floor(index);
                double r1 = 1.0 - r2;
                double height = HeightMap[start] * r1 + HeightMap[start + 1] * r2;

                //Ge linjen en offset på 300 pixlar och en amplitud på 150 pixlar
                Point p1 = new Point(x, (int)(300 + 150.0 * height));
                Point p2 = new Point(x, 0);

                Vectors.Add(
                new VectorGroup(
                    new Vector2(p1.X, p1.Y),
                    new Vector2(p2.X, p2.Y)
                ));
            }
        }

        public void Update(GameTime gameTime)
        {
            currentTime = gameTime.TotalGameTime.Seconds;

            if(currentTime > 0)
                if (currentTime % 4 == 0)
                    this.Generate();
        }

        public double GetRandomDouble(double minimum, double maximum)
        {
            return rnd.NextDouble() * (maximum - minimum) + minimum;
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.DrawString(_font, gameTime.TotalGameTime.Seconds.ToString(), new Vector2(10, 10), Color.White);

            foreach (var item in GetVectorGroup())
            {
                DrawLine(
                    item.GetFirst(),
                    item.GetSecond()
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
