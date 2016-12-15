using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace tank_mono
{
    class GameLogic
    {
        private double _wind;
        private Tank _currentTank;
        private KeyboardState pks;
        public GameLogic()
        {
            Wind = -100;
        }

        public void ChangeTank(TankManager TankManager)
        {
            KeyboardState ks = Keyboard.GetState();

            

            if (ks.IsKeyDown(Keys.E) && pks.IsKeyUp(Keys.E))
            {
                for (int i = 0; i < TankManager.Tanks.Count; i++)
                {
                    if (TankManager.Tanks[i] == CurrentTank)
                    {
                        CurrentTank = TankManager.Tanks[(i + 1)%TankManager.Tanks.Count];
                        TankManager.Tanks[i].CurrentFuel = TankManager.Tanks[i].Fuel;
                        break;
                    }
                }

            }
            pks = ks;
        }

        public Tank CurrentTank
        {
            get { return _currentTank; }
            set { _currentTank = value; }
        }


        public double Wind
        {
            get { return _wind; }
            set { _wind = value; }
        }

        

    }
}
