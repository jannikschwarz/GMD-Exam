using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public string weaponName { get;}
    public int weaponAmmo { get; set; }
    public float recoil { get; }
    public bool melee { get; }

    public Weapon(string name)
    {
        this.weaponName = name;
        melee = false;
        switch (name)
        {
            case ("Pistol"):
                weaponAmmo = 12;
                recoil = 0.15f;
                break;
            case ("Rifle"):
                weaponAmmo = 5;
                recoil = 0.5f;
                break;
            case ("SmallPistol"):
                weaponAmmo = 15;
                recoil = 0.1f;
                break;
            case ("MachineGun"):
                weaponAmmo = 30;
                recoil = 0.4f;
                break;
            case ("Shotgun"):
                weaponAmmo = 7;
                recoil = 0.6f;
                break;
            case ("GrenadeLauncher"):
                weaponAmmo = 5;
                recoil = 0.4f;
                break;
        }
    }
}
