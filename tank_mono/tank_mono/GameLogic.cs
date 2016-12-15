using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tank_mono
{
    class GameLogic
    {
        private double _wind;

        public GameLogic()
        {
            Wind = -100;
        }

        public double Wind
        {
            get { return _wind; }
            set { _wind = value; }
        }

        

    }
}
