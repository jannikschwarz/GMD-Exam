using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    [SerializeField] private GameObject _grenadeSplinter;
    [SerializeField] private AudioClip _explotion;
    private AudioSource _audioSource; 
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
        _audioSource = GetComponent<AudioSource>();
        _timeStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeStarted)
        {
            _explodeTime -= Time.deltaTime;
            if (_explodeTime <= 0.0f)
            {
                GrenadeExplode();
            }
        }
    }

    private void GrenadeExplode()
    {
        _timeStarted = false;

        Transform[] postions = GetComponentsInChildren<Transform>();
        for (int i = 0; i < postions.Length; i++)
        {
            var splinter = Instantiate(_grenadeSplinter, postions[i].position, Quaternion.identity);
            splinter.tag = gameObject.tag;
            Vector2 force;
            if (postions[i].position.y <= 0) force = new Vector2(-3000f, gameObject.transform.position.x * 500);
            else force = new Vector2(3000f, gameObject.transform.position.x * 500);
            splinter.GetComponent<Rigidbody2D>().AddRelativeForce(force);
        }
        _audioSource.PlayOneShot(_explotion, 0.2f);
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if(tag == "Player" || tag == "Platform")
        {
            if(_timeStarted && _bounce == 0)
            {
                GrenadeExplode();
            }else if(_timeStarted && _bounce > 0)
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
