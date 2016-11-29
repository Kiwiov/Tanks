using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tank_mono
{
    public class Weapon : WeaponEntity
    {
        public AmmoType AmmoType { get; private set; }
        public WeaponType WeaponType { get; private set; }
        public WeaponVisibility WeaponVisibility { get; private set; }

        public string Name { get; private set; }

        public int Cost { get; private set; }
        public int Damage { get; private set; }
        public int Test { get; private set; }

        public Weapon(string name, int damage, int cost, WeaponType weaponType, AmmoType ammoType, WeaponVisibility weaponVisibility = WeaponVisibility.Stock)
        {
            this.Name = name;
            this.Damage = damage;
            this.Cost = cost;
            this.WeaponType = weaponType;
            this.AmmoType = ammoType;
            this.WeaponVisibility = weaponVisibility;
        }

        public bool IsBuyAble()
        {
            return base.IsBuyAble(Cost, this.WeaponVisibility);
        }
        public bool IsStock()
        {
            return base.IsStock(Cost, this.WeaponVisibility);
        }
    }
}
