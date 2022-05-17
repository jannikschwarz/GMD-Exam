using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if(tag == "Player" || tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
}
