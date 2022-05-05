using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private float _bulletSpeed;
    [SerializeField]
    private int _bulletDamage;
    private float _playerNumber;
    private CharacterControl _characterControl;
    public static event Action<int,int> PlayerHitAction = delegate { }; 

    // Start is called before the first frame update
    void Start()
    {
        string tempName = gameObject.name;
        if (tempName.Contains("(")) tempName.Substring(0, tempName.IndexOf("("));
        _characterControl = GetComponentInParent<WeaponScript>().GetComponentInParent<CharacterControl>();
        var force = (_characterControl.FacingRight ? Vector2.right : -Vector2.right) * _bulletSpeed;
        gameObject.GetComponent<Rigidbody2D>().AddForce(force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Platform") Destroy(gameObject);
        else if(tag == "Player")
        {
            string playerNumber = gameObject.GetComponentInParent<GameObject>().GetComponentInParent<GameObject>().name;
            if (playerNumber.Contains("(")) playerNumber = playerNumber.Substring(0, playerNumber.IndexOf("("));

            string colliderName = gameObject.name;
            if(colliderName.Contains("(")) colliderName = colliderName.Substring(0, colliderName.IndexOf("("));

            switch (playerNumber)
            {
                case ("Player1"):
                    if(colliderName == "HeadCollider")
                    {
                        PlayerHitAction(1,100);
                    }
                    else
                    {
                        PlayerHitAction(1, _bulletDamage);
                    }
                    break;
                case ("Player2"):
                    if (colliderName == "HeadCollider")
                    {
                        PlayerHitAction(2, 100);
                    }
                    else
                    {
                        PlayerHitAction(2, _bulletDamage);
                    }
                    break;
                case ("Player3"):
                    if (colliderName == "HeadCollider")
                    {
                        PlayerHitAction(3, 100);
                    }
                    else
                    {
                        PlayerHitAction(3, _bulletDamage);
                    }
                    break;
                case ("Player4"):
                    if (colliderName == "HeadCollider")
                    {
                        PlayerHitAction(4, 100);
                    }
                    else
                    {
                        PlayerHitAction(4, _bulletDamage);
                    }
                    break;
            }
            Destroy(gameObject);
        }
    }
}
