using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    public class Kollision
    {

        public static Rectangle Intersection(Rectangle r1, Rectangle r2)
        {
            int x1 = Math.Max(r1.Left, r2.Left);
            int y1 = Math.Max(r1.Top, r2.Top);
            int x2 = Math.Min(r1.Right, r2.Right);
            int y2 = Math.Min(r1.Bottom, r2.Bottom);

            if ((x2 >= x1) && (y2 >= y1))
            {
                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }
            return Rectangle.Empty;
        }

        public static Rectangle Normalize(Rectangle reference, Rectangle overlap)
        {
            return new Rectangle(overlap.X - reference.X, overlap.Y - reference.Y, overlap.Width, overlap.Height);
        }

        public static bool TestCollision(Texture2D t1, Rectangle r1, Texture2D t2, Rectangle r2)
        {
            int pixelCount = r1.Width*r1.Height;
            uint[] texture1Pixels = new uint[pixelCount];
            uint[] texture2Pixels = new uint[pixelCount];

            t1.GetData(0, r1, texture1Pixels, 0, pixelCount);
            t2.GetData(0, r2, texture2Pixels, 0, pixelCount);

            for (int i = 0; i < pixelCount; i++)
            {
                if (((texture1Pixels[i] & 0xff000000) > 0) && ((texture2Pixels[i] & 0xff000000) > 0))
                {
                    return true;
                }
            }
            return false;
        }
    }
}