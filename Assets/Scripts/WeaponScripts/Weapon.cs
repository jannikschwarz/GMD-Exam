using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public string weaponName { get;}
    public int weaponAmmo { get; set; }
    public float recoil { get; }
    public bool melee { get; }

    public Weapon(string name, int weaponAmmo, float recoil = 0f, bool melee = false)
    {
        this.weaponName = name;
        this.weaponAmmo = weaponAmmo;
        this.recoil = recoil;
        this.melee = melee;
    }
}
