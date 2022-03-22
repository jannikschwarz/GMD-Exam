using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.WeaponScripts
{
    public sealed class WeaponManager
    {
        private static WeaponManager instance = null;
        private static readonly object padlock = new object();

        WeaponManager()
        {
        }

        public static WeaponManager Instance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new WeaponManager();
                }
                return instance;
            }
        }

        public Weapon GenerateWeapon(string weaponName)
        {
            switch (weaponName)
            {
                case ("SmallPistol"):
                    return new Weapon("SmallPistol",12,1f);
                case ("Pistol"):
                    return new Weapon("Pistol", 15, 1.5f);
                case ("Rifle"):
                    return new Weapon("Rifle", 5, 2.5f);
                case ("MachineGun"):
                    return new Weapon("MachineGun", 25, 3f);
                default:
                    return null;
            }
        }
    }
}
