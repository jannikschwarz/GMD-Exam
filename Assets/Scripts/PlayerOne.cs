using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour
{
    Rigidbody playerBody;
    int rightClicked;
    int leftClicked;
    int upClicked;
    int downClicked;
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        rightClicked = 0;
        leftClicked = 0;
        upClicked = 0;
        downClicked = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Register key right");
            rightClicked = 20;
        }
        else rightClicked = 0;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftClicked = 20;
        }
        else leftClicked = 0;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upClicked = 20;
        }
        else upClicked = 0;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downClicked = 20;
        }
        else downClicked = 0;
    }

    private void FixedUpdate()
    {
        if (rightClicked != 0)
        {
            Debug.Log("Adding right force: " + rightClicked);
            playerBody.AddForce(Vector3.right * rightClicked);
        }

        if(leftClicked != 0)
        {
            playerBody.AddForce(Vector3.left * leftClicked);
        }

        if(upClicked != 0)
        {
            playerBody.AddForce(Vector3.up * leftClicked);
        }

        if(downClicked != 0)
        {
            //To be implemented
        }
    }
}
