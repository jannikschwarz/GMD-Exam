using Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    private float spawnPositionY;
    private float spawnPositionX;
    private GunPosition gunPosition; 

    // Start is called before the first frame update
    void Start()
    {
        gunPosition = GunPosition.Instance();
        float[] positions = gunPosition.getRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
    }
}
