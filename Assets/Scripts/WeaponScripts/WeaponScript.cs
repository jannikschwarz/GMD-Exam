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
        _characterControl = null;
        _gunRenderer = GetComponent<SpriteRenderer>();
        _gunBarrelTransform = transform.GetChild(0).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        //Fire bullets in the facing direction
        if (_characterControl != null)
        {
            if (Input.GetButtonDown("X_" + _characterControl.PlayerNumber))
            {
                var bullet = Instantiate(_bullet, _gunBarrelTransform.position, Quaternion.identity);
            }
        }
        else Debug.Log("CharacterControl = null");
    }

    void SetCharacterControl(CharacterControl characterControl)
    {
        _characterControl = characterControl;
        gameObject.transform.SetParent(_characterControl.gameObject.GetComponentInChildren<Transform>());
        Vector3 parentPosition = _characterControl.transform.position;
        string weaponName = gameObject.name;
        if (weaponName.Contains("("))
        {
            weaponName = weaponName.Substring(0, weaponName.IndexOf("("));
        }
        Debug.LogWarning(weaponName + "END");
        switch (weaponName)
        {
            case ("Pistol"):
                //gameObject.transform.position = new Vector3(parentPosition.x + 0.1f, parentPosition.y - 0.092f, parentPosition.z + 0f);
                gameObject.transform.position = _characterControl.gameObject.transform.position;
                gameObject.transform.position += new Vector3(0.1f, -0.092f);
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
        }
    }
}
