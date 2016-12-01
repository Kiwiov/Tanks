using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace tank_mono
{
    public class Weapon
    {
        private float _power;
        private int _radius;
        private bool _isExploding;
        private string _type;
        private Texture2D _texture;
        private float _armourDamage;
        private string _name;
        private int _shotFired;
        public Weapon(string Name, float Power, int Radius, bool IsExploding, string Type, Texture2D Texture, float ArmourDamage, int ShotsFired)
        {
            this.Name = Name;
            this.Power = Power;
            this.Radius = Radius;
            this.IsExploding = IsExploding;
            this.Type = Type;
            this.Texture = Texture;
            this.ArmourDamage = ArmourDamage;
            this.ShotsFired = ShotsFired;
        }
        
        public int ShotsFired
        {
            get { return _shotFired; }
            set { _shotFired = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public float ArmourDamage
        {
            get { return _armourDamage; }
            set { _armourDamage = value; }
        }


        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public bool IsExploding
        {
            get { return _isExploding; }
            set { _isExploding = value; }
        }
        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }
        public float Power
        {
            get { return _power; }
            set { _power = value; }
        }
    }
}
