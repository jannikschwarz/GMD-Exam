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
                weapon = Instantiate(_riflePrefab) as GameObject;
                break;
            case ("SmallPistol"):
                weapon = Instantiate(_smallPistolPrefab) as GameObject;
                break;
            case ("MachineGun"):
                weapon = Instantiate(_machineGunPrefab) as GameObject;
                break;
            case ("GrenadeLauncher"):
                weapon = Instantiate(_grenadeLauncherPrefab) as GameObject;
                break;
            case ("Shotgun"):
                weapon = Instantiate(_shotGunPrefab) as GameObject;
                break;
        }
        if (weapon != null)
        {
            weapon.transform.parent = _characterControl.gameObject.transform;
            weapon.transform.position = Vector3.zero;
            weapon.transform.localPosition = Vector3.zero;
            if (!_characterControl.FacingRight) weapon.transform.Rotate(new Vector3(0f, 180f));
            weapon.SendMessage("SetWeaponScript", _characterControl.gameObject);
            WeaponPickedUp(weaponName);
        }
    }
}
