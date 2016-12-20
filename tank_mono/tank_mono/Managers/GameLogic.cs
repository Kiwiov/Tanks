using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace tank_mono
{
    public class GameLogic
    {
        private double _wind;
        private Tank _currentTank;
        private int _timeMax;
        private int _timeLeft;
        private Random ran = new Random();
        public GameLogic()
        {
            Wind = ran.Next(-100,101);
            TimeMax = 10;
            TimeLeft = TimeMax * 60;
        }

        public void CheckTime(TankManager tankManager, PickUpManager pickUpManager)
        {
            if (TimeLeft <= 0 && !ProjectileManager.IsShooting())
            {
                ChangeTank(tankManager, pickUpManager);
            }
            else
            {
                TimeLeft -= 1;
            }
        }

        private void SpawnPickUps(PickUpManager pickUpManager)
        {
            if (ran.Next(1, 101) <= 100)
            {
                pickUpManager.CreatePickup("Random");
            }
        }

        public void ChangeTank(TankManager tankManager, PickUpManager pickUpManager)
        {

            for (int i = 0; i < tankManager.Tanks.Count; i++)
            {
                if (tankManager.Tanks[i] == CurrentTank)
                {
                    CurrentTank = tankManager.Tanks[(i + 1)%tankManager.Tanks.Count];
                    tankManager.Tanks[i].CurrentFuel = tankManager.Tanks[i].Fuel;
                    break;
                }
            }
            TimeLeft = TimeMax * 60;
            ProjectileManager._fired = false;
            SpawnPickUps(pickUpManager);
            Wind = ran.Next(-100, 101);
        }

        public int TimeLeft
        {
            get { return _timeLeft; }
            set { _timeLeft = value; }
        }

        public int TimeMax
        {
            get { return _timeMax; }
            set { _timeMax = value; }
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
