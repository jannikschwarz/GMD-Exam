using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private string _bulletName;
    private bool _timeStarted;
    private int _bounce;
    private float _explodeTime;
    // Start is called before the first frame update
    void Start()
    {
        _bulletName = gameObject.name;
        if (_bulletName.Contains("(")) _bulletName = _bulletName.Substring(0, _bulletName.IndexOf("("));
        _bounce = 3;
        _explodeTime = 2.0f;
    }

    private void Update()
    {
        if (_timeStarted)
        {
            _explodeTime -= Time.deltaTime;
            if(_explodeTime <= 0.0f)
            {
                GrenadeExplode();
            }
        }
    }

    private void GrenadeExplode()
    {
        _timeStarted = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if(tag == "Player" || tag == "Platform")
        {
            if (_bulletName == "Grenade" && _bounce == 3)
            {
                _timeStarted = true;
                _bounce--;
            }
            else if(_timeStarted && _bounce == 0)
            {
                GrenadeExplode();
            }
            else if(_timeStarted && _bounce > 0)
            {
                _bounce--;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
