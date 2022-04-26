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

    void Start()
    {
        _characterControl = GetComponentInParent<CharacterControl>();
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
    }
}
