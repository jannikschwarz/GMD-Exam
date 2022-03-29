using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTwo : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float jumpForce = 60f;
    float horizontMove = 0f;
    float verticalMove = 0f;
    bool isJumping;

    Rigidbody2D _playerBody;

    bool sideWall;
    bool sideWallRight;

    // Start is called before the first frame update
    void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();
        isJumping = false;
        sideWall = false;
        sideWallRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        //horizontMove = Input.GetAxisRaw("Horizontal");
        //verticalMove = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            horizontMove = 0;
        }else if (Input.GetKey(KeyCode.A))
        {
            horizontMove = -1;
        }else if (Input.GetKey(KeyCode.D))
        {
            horizontMove = 1;
        }
        else
        {
            horizontMove = 0;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            verticalMove = 0;
        }else if (Input.GetKey(KeyCode.W))
        {
            verticalMove = 1;
        }else if (Input.GetKey(KeyCode.S))
        {
            verticalMove = -1;
        }
        else
        {
            verticalMove = 0;
        }

        if (!sideWall)
        {
            //transform.position += new Vector3(horizontMove, 0, 0) * Time.deltaTime * movementSpeed;
        }

        //if (sideWallRight) Debug.Log("Touching sidewall R");
        //else if(!sideWallRight && sideWall) Debug.Log("Touching sidewall L");
    }

    void FixedUpdate()
    {
       if(horizontMove > 0.1f || horizontMove < -0.1f)
        {
            _playerBody.AddForce(new Vector2(horizontMove * movementSpeed, 0f), ForceMode2D.Impulse);
        }

       if((verticalMove > 0.1f || verticalMove < -0.1f) && !isJumping)
        {
            if (sideWall)
            {
                _playerBody.AddForce(new Vector2(0f, verticalMove * (jumpForce/2)), ForceMode2D.Impulse);
            }
            else
            {
                _playerBody.AddForce(new Vector2(0f, verticalMove * jumpForce), ForceMode2D.Impulse);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject temp = collision.gameObject;
        if(temp.tag == "SideWall")
        {
            if(temp.transform.position.x > _playerBody.position.x)
            {
                sideWallRight = true;
            }
            else
            {
                sideWallRight = false;
            }
            sideWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "SideWall")
        {
            sideWall = false;
        }

        if (tag == "Platform" || tag == "Plank" || tag == "SideWall")
        {
            isJumping = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Platform" || tag == "Plank" || tag == "SideWall")
        {
            isJumping = false;
        }
    }
}
