using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] AudioClip _shotClip;
    [SerializeField] AudioClip _reloading;
    [SerializeField] private float _shootTimerInterval;
    private CharacterControl _characterControl;
    private PlayerInfo _playerInfo;
    private SpriteRenderer _gunRenderer;
    private Transform _gunBarrelTransform;
    private AudioSource _audioSource;
    private string _bulletName;
    private float _recoil;
    private float _recoilSpeed;
    private bool _shootInterval;
    private Weapon _weapon;

    //TEMP
    private int _bulletShoot = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _characterControl = null;
        _gunRenderer = GetComponent<SpriteRenderer>();
        _gunBarrelTransform = transform.GetChild(0).GetComponent<Transform>();
        _bulletName = _bullet.name;
        if (_bulletName.Contains("(")) _bulletName = _bulletName.Substring(0, _bulletName.IndexOf("("));
        _shootInterval = false;
        _shootTimerInterval = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shootInterval)
        {
            _shootTimerInterval -= Time.deltaTime;
            if(_shootTimerInterval <= 0.0f)
            {
                _shootInterval = false;
            }
        }
    }


    private void FixedUpdate()
    {
        //Fire bullets in the facing direction
        if (_characterControl != null && _weapon != null)
        {
            if(Input.GetAxis("X_" + _playerInfo.PlayerNumber) > 0)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        var force = (_characterControl.FacingRight ? Vector2.right : -Vector2.right) * _bulletSpeed;
        if (!_shootInterval)
        {
            if(_weapon.weaponAmmo <= 0)
            {
                if (_weapon.weaponName == "Rifle") _audioSource.PlayOneShot(_reloading, 0.1f);
                _characterControl.Armed = false;
                Destroy(gameObject, 2f);
            }

            if (_weapon.weaponName == "Shotgun") ShotgunShoot(force);
            else
            {
                var bullet = Instantiate(_bullet, _gunBarrelTransform.position, Quaternion.identity);
                bullet.tag = "Bullet-" + _playerInfo.PlayerNumber;
                bullet.GetComponent<Rigidbody2D>().AddForce(force);
                PlayAudio();
                WeaponTypeShoot();
                _weapon.weaponAmmo--;
                _bulletShoot++;
            }
        }
    }

    private void PlayAudio()
    {
        if(_weapon.weaponName == "Shotgun" || _weapon.weaponName == "Rifle" || _weapon.weaponName == "GrenadeLauncher")
        {
            _audioSource.PlayOneShot(_shotClip, 0.1f);
            _audioSource.PlayOneShot(_reloading, 0.1f);
        }
        else
        {
            _audioSource.PlayOneShot(_shotClip, 0.1f);
        }
    }

    private void ShotgunShoot(Vector2 force)
    {
        for(int i = -120; i < 121; i += 60)
        {
            var bullet = Instantiate(_bullet, _gunBarrelTransform.position, Quaternion.identity);
            bullet.tag = "Bullet-" + _playerInfo.PlayerNumber;
            force.y = i;
            bullet.GetComponent<Rigidbody2D>().AddForce(force);
        }
        PlayAudio();
        WeaponTypeShoot();
        _weapon.weaponAmmo--;
        _bulletShoot++;
    }

    private void WeaponTypeShoot()
    {
        if(_weapon.weaponName == "Rifle")
        {
            _shootTimerInterval = 2.0f;
            _shootInterval = true;
        }else if(_weapon.weaponName == "Shotgun" || _weapon.weaponName == "GrenadeLauncher")
        {
            _shootTimerInterval = 1.0f;
            _shootInterval = true;
        }else if(_weapon.weaponName == "MachineGun")
        {
            _shootTimerInterval = 0.2f;
            _shootInterval = true;
        }else if(_weapon.weaponName == "Pistol" || _weapon.weaponName == "SmallPistol")
        {
            _shootTimerInterval = 0.4f;
            _shootInterval = true;
        }
        _recoil += _weapon.recoil;
        Recoiling();
    }

    void Recoiling()
    {
        if(_recoil > 0)
        {
            var maxRecoil = Quaternion.Euler(-20f, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, maxRecoil, Time.deltaTime * _recoilSpeed);
            _recoil -= Time.deltaTime;
        }
        else
        {
            _recoil = 0;
            var minRecoil = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, minRecoil, Time.deltaTime * _recoilSpeed / 2);
        }
    }

    void SetWeaponScript(GameObject mainPlayerGameObject)
    {
        _characterControl = mainPlayerGameObject.GetComponent<CharacterControl>();
        _playerInfo = mainPlayerGameObject.GetComponent<PlayerInfo>();
        //Vector3 parentPosition = _characterControl.transform.position;
        string weaponName = gameObject.name;
        if (weaponName.Contains("("))
        {
            weaponName = weaponName.Substring(0, weaponName.IndexOf("("));
        }
        /*switch (weaponName)
        {
            case ("Pistol"):
                gameObject.transform.position += new Vector3(0.1f, -0.092f, 0f);
                break;
            case ("Rifle"):
                gameObject.transform.position = new Vector3(0.098f, -0.093f, 0f);
                break;
            case ("SmallPistol"):
                gameObject.transform.position = new Vector3(0.098f, -0.093f, 0f);
                break;
            case ("MachineGun"):
                gameObject.transform.position = new Vector3(0.098f, -0.093f, 0f);
                break;
            case ("Shotgun"):
                gameObject.transform.position = new Vector3(0.0661f, -0.0969f, 0f);
                break;
            case ("GrenadeLauncher"):
                gameObject.transform.position = new Vector3(0.08907971f, -0.08400133f, 0f);
                break;
        }*/
        _weapon = new Weapon(weaponName);
        _recoil = 0f;
    }
}
