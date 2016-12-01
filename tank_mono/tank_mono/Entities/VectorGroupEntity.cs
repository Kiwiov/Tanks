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
    public class VectorGroupEntity
    {
        private Vector2 vectorOne;
        private Vector2 vectorTwo;

        public VectorGroupEntity(Vector2 one, Vector2 two)
        {
            vectorOne = one;
            vectorTwo = two;
        }

        public List<Vector2> GetAll()
        {
            return new List<Vector2> { vectorOne, vectorTwo };
        }
        public Vector2 GetFirst()
        {
            return vectorOne;
        }

        public Vector2 GetSecond()
        {
            return vectorTwo;
        }
    }
}
