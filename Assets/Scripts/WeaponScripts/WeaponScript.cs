using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    private CharacterControl _characterControl;
    private SpriteRenderer _gunRenderer;
    private Transform _gunBarrelTransform;
    // Start is called before the first frame update

    private void Awake()
    {
        _characterControl = GetComponentInParent<CharacterControl>();
        _gunRenderer = GetComponent<SpriteRenderer>();
        _gunBarrelTransform = transform.GetChild(0).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Don't show the gun or allow shooting if the player is ducking

        _gunRenderer.enabled = true;
    }

    private void FixedUpdate()
    {
        //Fire bullets in the facing direction
        /*if(Input.GetButtonDown("X_" + _characterControl.PlayerNumber) && )
        {
            var bullet = Instantiate(_bullet, _gunBarrelTransform.position, Quaternion.identity);
        }*/
    }
}
