using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    public class WeaponCreator
    {
        private Texture2D _bulletTexture;
        private Texture2D _rocketTexture;
        private Texture2D _solidAntiArmourTexture;
        private Texture2D _mineTexture;

        public WeaponCreator(Texture2D BulletTexture, Texture2D RocketTexture, Texture2D SolidAntiArmourTexture, Texture2D MineTexture)
        {
            _bulletTexture = BulletTexture;
            _rocketTexture = RocketTexture;
            _solidAntiArmourTexture = SolidAntiArmourTexture;
            _mineTexture = MineTexture;
        }

        public Weapon MachineGun()
        {
            Weapon machineGun = new Weapon("MachineGun",20,1,false,"Bullet",_bulletTexture,2,10,int.MaxValue);
            return machineGun;
        }

        public Weapon Missile()
        {
            Weapon missile = new Weapon("Missile", 100, 15, true, "Rocket", _rocketTexture, 50, 1,5);
            return missile;
        }

        public Weapon AntiArmour()
        {
            Weapon missile = new Weapon("AntiArmour", 50, 5, false, "SolidAntiArmour", _solidAntiArmourTexture, 100, 1,5);
            return missile;
        }

        public Weapon Mine()
        {
            Weapon mine = new Weapon("Mine", 150, 20, true, "Mine", _mineTexture, 100, 1, 2);
            return mine;
        }
    }
}
