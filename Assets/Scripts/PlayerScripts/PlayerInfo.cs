using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private int _playerNumber;
    public static event Action<int> MapDeath = delegate { };
    public static event Action<int, int> KilledByPlayer = delegate { };
    private Animator _anim;
    private bool _hitAble;
    public string _playerName { private set; get; }
    public int _hitpoints { private set; get; }

    void Start()
    {
        _hitpoints = 100;
        _anim = GetComponentInChildren<Animator>();
        _hitAble = true;
    }

    public int PlayerNumber
    {
        get { return _playerNumber; }
    }

    public void DeathOfEdge()
    {
        _anim.Play("Player_Death");
        MapDeath(_playerNumber);
        Destroy(gameObject, 0.5f);
    }

    public void BulletHit(GameObject objectEntered)
    {
        _anim.Play("Player" + _playerNumber + "_Hit");
        string bulletName = objectEntered.name;
        if (bulletName.Contains("(")) bulletName = bulletName.Substring(0, bulletName.IndexOf("("));
        switch (bulletName)
        {
            case ("Grenade"):
                _hitpoints -= 70;
                break;
            case ("MachineGunBullet"):
                _hitpoints -= 50;
                break;
            case ("PistolBullet"):
                _hitpoints -= 50;
                break;
            case ("RiffleBullet"):
                _hitpoints -= 100;
                break;
            case ("ShotgunBullet"):
                _hitpoints -= 80;
                break;
            case ("SmallPistol"):
                _hitpoints -= 40;
                break;
        }
        if(_hitpoints <= 0 && _hitAble)
        {
            string tag = objectEntered.tag;
            tag = tag.Substring(tag.IndexOf("-") + 1, 1);
            int killedBy = int.Parse(tag);
            KilledByPlayer(_playerNumber, killedBy);
            _anim.Play("Player_Death");
            _hitAble = false;
            Destroy(gameObject, 0.5f);
        }
    }

    public void PlayerName(string playerName)
    {
        _playerName = playerName;
    }
}
