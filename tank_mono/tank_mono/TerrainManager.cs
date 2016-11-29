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

    public class Welp
    {
        private Vector2 v1;
        private Vector2 v2;

        public Welp(Vector2 v1, Vector2 v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public Vector2 GetV1()
        {
            return v1;
        }

        public Vector2 GetV2()
        {
            return v2;
        }
    }
    public class TerrainManager
    {
        private List<double> HeightMap = new List<double>();
        //Randomerare för slumptal
        private Random rnd = new Random();
        //Bilden som kommer attt genereras i form av en bitmap
        private System.Drawing.Bitmap b = new System.Drawing.Bitmap(512, 512);


        private Texture2D t; //base for the line texture

        private SpriteBatch _spriteBatch;

        private int Iterations = 2;

        public TerrainManager(GraphicsDevice device, SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;

            t = new Texture2D(device, 1, 1);
            t.SetData<Color>(
                new Color[] { Color.White });// fill the texture with white
        }

        public void Generate()
        {
            //Nollställer listan med punkter
            HeightMap.Clear();

            //Lägger till start- och stop-punkt
            HeightMap.Add(0);
            HeightMap.Add(0);
            //Börjar algoritmen med slump +- 1.0
            double Max = 1.0;
            //Bestämmer ojämnheten i terrängen
            double Roughness = ((double)8 / 100.0);
            //Generera linjen
            //
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

            // Rendera bilden(640 pixlar bred)
            //
            for (int x = 0; x <= 511; x++)
            {
                //Beräkna vilken punkt på linjen som ligger närmast
                double index = (x / 512.0) * (HeightMap.Count - 1);
                int start = (int)Math.Floor(index);

                //Interpolera mellan beräknad punkt och nästa punkt
                double r2 = index - Math.Floor(index);
                double r1 = 1.0 - r2;
                double height = HeightMap[start] * r1 + HeightMap[start + 1] * r2;

                //Ge linjen en offset på 300 pixlar och en amplitud på 150 pixlar
                Point p1 = new Point(x, (int)(300 + 150.0 * height));
                Point p2 = new Point(x, 0);

                var v1 = new Vector2(p1.X, p1.Y);
                var v2 = new Vector2(p2.X, p2.Y);

                Vectors.Add(new Welp(v1, v2));
            }
        }

        public List<Welp> Vectors = new List<Welp>();

        public void Draw()
        {
            foreach(Welp item in Vectors)
            {
                DrawLine(item.GetV1(), item.GetV2());
            }
        }

        public void DrawLine(Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


             _spriteBatch.Draw(t,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                Color.Brown, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(2,1), // point in line about which to rotate
                SpriteEffects.None,
                0);
        }
    }
}
