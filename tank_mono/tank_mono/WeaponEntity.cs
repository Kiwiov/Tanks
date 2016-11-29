using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tank_mono
{
    public abstract class WeaponEntity
    {
        public bool IsBuyAble(int cost, WeaponVisibility visibility)
        {
            return cost > 0 && visibility == WeaponVisibility.Buyable;
        }

        public bool IsStock(int cost, WeaponVisibility visibility)
        {
            return cost == 0 && visibility == WeaponVisibility.Stock;
        }
    }

    /*public class WeaponCollection
    {
        public WeaponCollection()
        {
            var Weapon1 = new Weapon("Standard kanon", 50, 0, WeaponType.Canon, AmmoType.Bomb);
        }

        public Weapon GetWeaponByName(string name)
        {
            return null;
        }
        public Weapon GetWeaponByType(WeaponType type)
        {
            return null;
        }
    }*/

    public enum WeaponVisibility
    {
        Stock = 1,
        Buyable = 2
    }

    public enum AmmoType
    {
        Missile = 1,
        Grenade = 2,
        Bomb = 3,
        Others = 4
    }

    public enum WeaponType
    {
        Canon = 1
    }
}
