using Scripts;
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

    private WeaponPosition weaponPosition;
    private MenuSelection menuSelection;

    void Start()
    {
        weaponPosition = WeaponPosition.Instance();
        PistolSpawn();
        RifleSpawn();
    }

    private void RifleSpawn()
    {
        float[] p1 = weaponPosition.getRandomPosition(menuSelection._mapNumber);
        float[] p2 = weaponPosition.getRandomPosition(menuSelection._mapNumber);
        float[] p3 = weaponPosition.getRandomPosition(menuSelection._mapNumber);
        float[] p4 = weaponPosition.getRandomPosition(menuSelection._mapNumber);
        Instantiate(rifle, new Vector2(p1[0], p1[1]), Quaternion.identity);
        Instantiate(machineGun, new Vector2(p2[0], p2[1]), Quaternion.identity);
        Instantiate(shotGun, new Vector2(p3[0], p3[1]), Quaternion.identity);
        Instantiate(grenadeLauncher, new Vector2(p4[0], p4[1]), Quaternion.identity);
    }

    private void PistolSpawn()
    {
        float[] p1 = weaponPosition.getRandomPosition(menuSelection._mapNumber);
        float[] p2 = weaponPosition.getRandomPosition(menuSelection._mapNumber);
        Instantiate(smallPistol, new Vector2(p1[0], p1[1]), Quaternion.identity);
        Instantiate(pistol, new Vector2(p2[0], p2[1]), Quaternion.identity);
    }
}

