using Assets.Scripts.WeaponScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _riflePrefab;
    [SerializeField] private GameObject _machineGunPrefab;
    [SerializeField] private GameObject _pistolPrefab;
    [SerializeField] private GameObject _smallPistolPrefab;
    [SerializeField] private GameObject _shotGunPrefab;
    [SerializeField] private GameObject _grenadeLauncherPrefab;
    private CharacterControl _characterControl;
    private int _hitPoints;

    void Start()
    {
        _characterControl = GetComponentInParent<CharacterControl>();
        _hitPoints = 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject objectEntered = collision.gameObject;
        if(objectEntered.tag == "Weapon" && !_characterControl.Armed)
        {
            string weaponName = objectEntered.name.Substring(0,objectEntered.name.IndexOf("1"));
            _characterControl.Armed = true;
            Destroy(objectEntered);
            PickedUpWeapon(weaponName);
        }else if(objectEntered.tag == "MapEdge")
        {
            GameObject fullGameObject = _characterControl.gameObject;
            Destroy(fullGameObject);
        }else if(objectEntered.tag == "Bullet")
        {
            string bulletName = objectEntered.name;
            if (bulletName.Contains("(")) bulletName = bulletName.Substring(0, bulletName.IndexOf("("));
            switch (bulletName)
            {
                case ("MachineGunBullet"):
                    _hitPoints -= 50;
                    break;
                case ("PistolBullet"):
                    _hitPoints -= 34;
                    break;
                case ("RiffleBullet"):
                    _hitPoints -= 70;
                    break;
                case ("ShotgunBullet"):
                    _hitPoints -= 30;
                    break;
                case ("SmallPistolBullet"):
                    _hitPoints -= 25;
                    break;
                case ("GrenadeSplinter"):
                    _hitPoints -= 50;
                    break;
            }

            if(_hitPoints < 0)
            {
                GameObject parentObject = _characterControl.gameObject;
                Destroy(parentObject);
            }
        }
    }

    private void PickedUpWeapon(string weaponName)
    {
        switch (weaponName)
        {
            case ("Pistol"):
                Instantiate(_pistolPrefab, new Vector3(0.1f, -0.092f, 0f), Quaternion.identity);
                break;
            case ("Rifle"):
                Instantiate(_riflePrefab, new Vector3(0.098f, -0.093f, 0f), Quaternion.identity);
                break;
            case ("SmallPistol"):
                Instantiate(_smallPistolPrefab, new Vector3(0.098f, -0.093f, 0f), Quaternion.identity);
                break;
            case ("MachineGun"):
                Instantiate(_machineGunPrefab, new Vector3(0.098f, -0.093f, 0f), Quaternion.identity);
                break;
        }
        Debug.LogWarning("SPAWNED WEAPON: " + weaponName);
    }
}
