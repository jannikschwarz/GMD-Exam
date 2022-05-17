using Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    //Weapons:
    [SerializeField] public GameObject smallPistol;
    [SerializeField] public GameObject pistol;
    [SerializeField] public GameObject rifle;
    [SerializeField] public GameObject machineGun;
    [SerializeField] public GameObject shotGun;
    [SerializeField] public GameObject grenadeLauncher;
    private float[] smallPistolP;
    private float[] pistolP;
    private float[] rifleP;
    private float[] machineGunP;
    private float[] shotGunP;
    private float[] grenadeLauncherP;

    private WeaponPosition _weaponPosInstance;

    void Start()
    {
        PlayerTrigger.WeaponPickedUp += RespawnWeapon;
    }

    public void WeaponMapSpawn(int mapNumber)
    {
        _weaponPosInstance = WeaponPosition.Instance(mapNumber);
        if (!WeaponPosition.retrievedPositions)
        {
            WeaponSpawn();
            WeaponPosition.retrievedPositions = true;
        }
    }

    private void WeaponSpawn()
    {
        rifleP = WeaponPosition.getRandomPosition();
        pistolP = WeaponPosition.getRandomPosition();
        smallPistolP = WeaponPosition.getRandomPosition();
        machineGunP = WeaponPosition.getRandomPosition();
        shotGunP = WeaponPosition.getRandomPosition();
        grenadeLauncherP = WeaponPosition.getRandomPosition();
        Instantiate(rifle, new Vector2(rifleP[0], rifleP[1]), Quaternion.identity);
        Instantiate(machineGun, new Vector2(machineGunP[0], machineGunP[1]), Quaternion.identity);
        Instantiate(shotGun, new Vector2(shotGunP[0], shotGunP[1]), Quaternion.identity);
        Instantiate(grenadeLauncher, new Vector2(grenadeLauncherP[0], grenadeLauncherP[1]), Quaternion.identity);
        Instantiate(smallPistol, new Vector2(smallPistolP[0], smallPistolP[1]), Quaternion.identity);
        Instantiate(pistol, new Vector2(pistolP[0], pistolP[1]), Quaternion.identity);
    }

    public static void Reset()
    {
        WeaponPosition.retrievedPositions = false;
    }

    void RespawnWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case ("Pistol"):
                WeaponPosition.RemoveWeapon(pistolP);
                pistolP = WeaponPosition.getRandomPosition();
                if(pistolP != null)Instantiate(pistol, new Vector2(pistolP[0], pistolP[1]), Quaternion.identity);
                break;
            case ("Rifle"):
                WeaponPosition.RemoveWeapon(rifleP);
                rifleP = WeaponPosition.getRandomPosition();
                if(rifleP != null) Instantiate(rifle, new Vector2(rifleP[0], rifleP[1]), Quaternion.identity);
                break;
            case ("SmallPistol"):
                WeaponPosition.RemoveWeapon(smallPistolP);
                smallPistolP = WeaponPosition.getRandomPosition();
                if(smallPistolP != null)Instantiate(smallPistol, new Vector2(smallPistolP[0], smallPistolP[1]), Quaternion.identity);
                break;
            case ("MachineGun"):
                WeaponPosition.RemoveWeapon(machineGunP);
                machineGunP = WeaponPosition.getRandomPosition();
                if(machineGunP != null)Instantiate(machineGun, new Vector2(machineGunP[0], machineGunP[1]), Quaternion.identity);
                break;
            case ("GrenadeLauncher"):
                WeaponPosition.RemoveWeapon(grenadeLauncherP);
                grenadeLauncherP = WeaponPosition.getRandomPosition();
                if(grenadeLauncherP != null)Instantiate(grenadeLauncher, new Vector2(grenadeLauncherP[0], grenadeLauncherP[1]), Quaternion.identity);
                break;
            case ("Shotgun"):
                WeaponPosition.RemoveWeapon(shotGunP);
                shotGunP = WeaponPosition.getRandomPosition();
                if(shotGunP != null)Instantiate(shotGun, new Vector2(shotGunP[0], shotGunP[1]), Quaternion.identity);
                break;
        }
    }
}

