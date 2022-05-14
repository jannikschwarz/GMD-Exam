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
    [SerializeField] private AudioClip _hitClip;
    [SerializeField] private AudioClip _deathClip;

    private PlayerInfo _playerInfo;
    private CharacterControl _characterControl;
    private AudioSource _audioSource;
    

    public static event Action<string> WeaponPickedUp = delegate { };


    void Start()
    {
        _playerInfo = GetComponentInParent<PlayerInfo>();
        _characterControl = GetComponentInParent<CharacterControl>();
        _audioSource = GetComponent<AudioSource>();
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
            _audioSource.PlayOneShot(_deathClip, 0.1f);
            _playerInfo.DeathOfEdge();
        }else if (objectEntered.tag.Contains("Bullet"))
        {
            _audioSource.PlayOneShot(_hitClip, 0.1f);
            _playerInfo.BulletHit(objectEntered);
        }
    }

    private void PickedUpWeapon(string weaponName)
    {
        GameObject weapon = null;
        if (weaponName.Contains(" ")) weaponName = weaponName.Substring(0, weaponName.IndexOf(" "));
        switch (weaponName)
        {
            case ("Pistol"):
                weapon = Instantiate(_pistolPrefab) as GameObject;
                break;
            case ("Rifle"):
                weapon = Instantiate(_riflePrefab, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case ("SmallPistol"):
                weapon = Instantiate(_smallPistolPrefab, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case ("MachineGun"):
                weapon = Instantiate(_machineGunPrefab, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case ("GrenadeLauncher"):
                weapon = Instantiate(_grenadeLauncherPrefab, new Vector2(0f, 0f), Quaternion.identity);
                break;
            case ("Shotgun"):
                weapon = Instantiate(_shotGunPrefab, new Vector2(0f, 0f), Quaternion.identity);
                break;
        }
        if (weapon != null)
        {
            weapon.transform.parent = gameObject.transform;
            weapon.SendMessage("SetWeaponScript", _characterControl.gameObject);
            WeaponPickedUp(weaponName);
        }
    }
}
