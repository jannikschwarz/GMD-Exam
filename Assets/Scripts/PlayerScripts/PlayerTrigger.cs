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
        }else if(objectEntered.tag == "MapEdge")
        {
            GameObject fullGameObject = _characterControl.gameObject;
            //FirePlayerDeath()
        }
    }

    private void PickedUpWeapon(string weaponName)
    {
        GameObject weapon = new GameObject();
        switch (weaponName)
        {
            case ("Pistol "):
                weapon = Instantiate(_pistolPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                break;
            case ("Rifle "):
                weapon = Instantiate(_riflePrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                break;
            case ("SmallPistol "):
                weapon = Instantiate(_smallPistolPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                break;
            case ("MachineGun "):
                weapon = Instantiate(_machineGunPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                break;
        }
        weapon.SendMessage("SetCharacterControl", _characterControl);
    }
}
