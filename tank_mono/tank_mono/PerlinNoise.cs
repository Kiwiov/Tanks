using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tank_mono
{
    /// Perlin Noise
    ///

    static class PerlinNoise
    {
        public static int Seed { get; private set; }

        public static int Octaves { get; set; }

        public static double Amplitude { get; set; }

        public static double Persistence { get; set; }

        public static double Frequency { get; set; }

        static PerlinNoise()
        {
            Random r = new Random();
            //LOOOL
            PerlinNoise.Seed = r.Next(Int32.MaxValue);
            PerlinNoise.Octaves = 8;
            PerlinNoise.Amplitude = 1;
            PerlinNoise.Frequency = 0.015;
            PerlinNoise.Persistence = 0.65;
        }

        public static double Noise(int x, int y)
        {
            //returns -1 to 1
            double total = 0.0;
            double freq = PerlinNoise.Frequency, amp = PerlinNoise.Amplitude;
            for (int i = 0; i < PerlinNoise.Octaves; ++i)
            {
                total = total + PerlinNoise.Smooth(x * freq, y * freq) * amp;
                freq *= 2;
                amp *= PerlinNoise.Persistence;
            }
            if (total < -2.4) total = -2.4;
            else if (total > 2.4) total = 2.4;

            return (total / 2.4);
        }

        public static double NoiseGeneration(int x, int y)
        {
            int n = x + y * 57;
            n = (n << 13) ^ n;

            return (1.0 - ((n * (n * n * 15731 + 789221) + PerlinNoise.Seed) & 0x7fffffff) / 1073741824.0);
        }

        private static double Interpolate(double x, double y, double a)
        {
            double value = (1 - Math.Cos(a * Math.PI)) * 0.5;
            return x * (1 - value) + y * value;
        }

        private static double Smooth(double x, double y)
        {
            double n1 = NoiseGeneration((int)x, (int)y);
            double n2 = NoiseGeneration((int)x + 1, (int)y);
            double n3 = NoiseGeneration((int)x, (int)y + 1);
            double n4 = NoiseGeneration((int)x + 1, (int)y + 1);

            double i1 = Interpolate(n1, n2, x - (int)x);
            double i2 = Interpolate(n3, n4, x - (int)x);

            return Interpolate(i1, i2, y - (int)y);
        }
    }
    #region lel
    /*public class PerlinNoise
    {
        /// Perlin Noise Constructot
        public PerlinNoise(int width, int height)
        {
            this.MAX_WIDTH = width;
            this.MAX_HEIGHT = height;
        }

        public int MAX_WIDTH = 256;
        public int MAX_HEIGHT = 256;

        /// Gets the value for a specific X and Y coordinate
        /// results in range [-1, 1] * maxHeight
        public float GetRandomHeight(float X, float Y, float MaxHeight,
            float Frequency, float Amplitude, float Persistance,
            int Octaves)
        {
            GenerateNoise();
            float FinalValue = 0.0f;
            for (int i = 0; i < Octaves; ++i)
            {
                FinalValue += GetSmoothNoise(X * Frequency, Y * Frequency) * Amplitude;
                Frequency *= 2.0f;
                Amplitude *= Persistance;
            }
            if (FinalValue < -1.0f)
            {
                FinalValue = -1.0f;
            }
            else if (FinalValue > 1.0f)
            {
                FinalValue = 1.0f;
            }
            return FinalValue * MaxHeight;
        }

        //This function is a simple bilinear filtering function which is good (and easy) enough.        
        private float GetSmoothNoise(float X, float Y)
        {
            float FractionX = X - (int)X;
            float FractionY = Y - (int)Y;
            int X1 = ((int)X + MAX_WIDTH) % MAX_WIDTH;
            int Y1 = ((int)Y + MAX_HEIGHT) % MAX_HEIGHT;
            //for cool art deco looking images, do +1 for X2 and Y2 instead of -1...
            int X2 = ((int)X + MAX_WIDTH - 1) % MAX_WIDTH;
            int Y2 = ((int)Y + MAX_HEIGHT - 1) % MAX_HEIGHT;
            float FinalValue = 0.0f;
            FinalValue += FractionX * FractionY * Noise[X1, Y1];
            FinalValue += FractionX * (1 - FractionY) * Noise[X1, Y2];
            FinalValue += (1 - FractionX) * FractionY * Noise[X2, Y1];
            FinalValue += (1 - FractionX) * (1 - FractionY) * Noise[X2, Y2];
            return FinalValue;
        }

        float[,] Noise;
        bool NoiseInitialized = false;
        /// create a array of randoms
        /// 

        private static Random StaticRandom = new Random();
        private void GenerateNoise()
        {
            if (NoiseInitialized)                //A boolean variable in the class to make sure we only do this once
                return;
            Noise = new float[MAX_WIDTH, MAX_HEIGHT];    //Create the noise table where MAX_WIDTH and MAX_HEIGHT are set to some value>0            
            for (int x = 0; x < MAX_WIDTH; ++x)
            {
                for (int y = 0; y < MAX_HEIGHT; ++y)
                {
                    Noise[x, y] = ((float)(StaticRandom.Next(1, 2)) - 0.5f) * 2.0f;  //Generate noise between -1 and 1
                }
            }
            NoiseInitialized = true;
        }

    }*/
    #endregion
}