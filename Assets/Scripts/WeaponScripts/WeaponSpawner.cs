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

    private WeaponPosition weaponPosition;

    void Start()
    {
        weaponPosition = WeaponPosition.Instance();
        PistolSpawn();
        RifleSpawn();
    }

    private void RifleSpawn()
    {
        float[] p1 = weaponPosition.getRandomPosition();
        float[] p2 = weaponPosition.getRandomPosition();
        Instantiate(rifle, new Vector2(p1[0], p1[1]), Quaternion.identity);
        Instantiate(machineGun, new Vector2(p2[0], p2[1]), Quaternion.identity);
    }

    private void PistolSpawn()
    {
        float[] p1 = weaponPosition.getRandomPosition();
        float[] p2 = weaponPosition.getRandomPosition();
        Instantiate(smallPistol, new Vector2(p1[0], p1[1]), Quaternion.identity);
        Instantiate(pistol, new Vector2(p2[0], p2[1]), Quaternion.identity);
    }
}

