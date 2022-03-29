using Assets.Scripts.WeaponScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    WeaponManager weaponManager;
    Weapon weapon;
    GameObject weaponGameObject;
    private int playerNumber;

    void Start()
    {
        weapon = null;
        weaponGameObject = null;
        weaponManager = WeaponManager.Instance();
        playerNumber = 0;

        string playerName = gameObject.name;
        if (playerName.Contains("("))
        {
            playerName = playerName.Substring(0, playerName.IndexOf("("));
        }
        switch (playerName)
        {
            case ("Player1"):
                playerNumber = 1;
                break;
            case ("Player2"):
                playerNumber = 2;
                break;
            case ("Player3"):
                playerNumber = 3;
                break;
            case ("Player4"):
                playerNumber = 4;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && playerNumber == 1)
        {
            if(weapon != null && weaponGameObject != null)
            {
                if (weapon.weaponAmmo > 0)
                {
                    weapon.weaponAmmo--;
                }
                else
                {
                    Destroy(weaponGameObject);
                    weapon = null;
                }
                Debug.Log("Player 1 is shooting with his " + weapon.weaponName);
            }
        }

        if (Input.GetKey(KeyCode.X) && playerNumber == 2)
        {
            if(weapon != null)
            {
                Debug.Log("Player 2 is shooting with his " + weapon.weaponName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject objectEntered = other.gameObject;
        if(objectEntered.tag == "Weapon" && weapon == null)
        {
            string weaponName = objectEntered.name;
            if (weaponName.Contains("(")){
                weaponName = weaponName.Substring(0, weaponName.IndexOf("("));
            }
            weapon = weaponManager.GenerateWeapon(weaponName);
            PickedUpWeapon(objectEntered, weaponName);
        }

        if(objectEntered.tag == "MapEdge")
        {
            Debug.Log("Player destroyed");
            Destroy(this);
        }
    }

    private void PickedUpWeapon(GameObject weapon, string name)
    {
        weaponGameObject = weapon;
        weapon.transform.parent = transform;
        weapon.GetComponent<BoxCollider2D>().enabled = false;
        weapon.transform.localPosition = new Vector2(0,0);

        switch (name)
        {
            case ("Pistol"):
                weapon.transform.localPosition += new Vector3(0.1f, -0.092f, 0f);
                break;
            case ("SmallPistol"):
                weapon.transform.localPosition += new Vector3(0.098f, -0.093f, 0f);
                break;
            case ("Rifle"):
                weapon.transform.localPosition += new Vector3(0.098f, -0.093f, 0f);
                break;
            case ("MachineGun"):
                weapon.transform.localPosition += new Vector3(0.098f, -0.093f, 0f);
                break;
        }
    }
}
