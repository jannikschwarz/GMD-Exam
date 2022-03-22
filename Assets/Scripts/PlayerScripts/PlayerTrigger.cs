using Assets.Scripts.WeaponScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    WeaponManager weaponManager;
    Weapon weapon;

    void Start()
    {
        weapon = null;
        weaponManager = WeaponManager.Instance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject objectEntered = other.gameObject;
        if(objectEntered.tag == "Weapon")
        {
            string weaponName = objectEntered.name;
            if (weaponName.Contains("(")){
                weaponName = weaponName.Substring(0, weaponName.IndexOf("("));
            }
            weapon = weaponManager.GenerateWeapon(weaponName);
            PickedUpWeapon(objectEntered, weaponName);
        }
    }

    private void PickedUpWeapon(GameObject weapon, string name)
    {
        weapon.transform.parent = transform;

        switch (name)
        {
            case ("Pistol"):
                weapon.transform.position = new Vector2(transform.position.x + 0.104f, transform.position.y - 0.091f);
                break;
            case ("SmallPistol"):
                weapon.transform.position = new Vector2(transform.position.x + 0.098f, transform.position.y - 0.093f);
                break;
            case ("Rifle"):
                weapon.transform.position = new Vector2(transform.position.x + 0.098f, transform.position.y - 0.093f);
                break;
            case ("MachineGun"):
                weapon.transform.position = new Vector2(transform.position.x + 0.098f, transform.position.y - 0.093f);
                break;
        }
    }
}
