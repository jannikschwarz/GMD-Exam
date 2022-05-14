using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    public bool Ducking { get; private set; }
    public bool FacingRight { get; private set; }
    public bool Armed { get; set; }

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _maxJumps;
    [SerializeField] private float _sprintFactor;

    [SerializeField] private float _groundCheckerRadius;
    [SerializeField] private LayerMask _whatIsGround;

    private Transform _groundChecker;
    private bool _grounded;

    private Rigidbody2D _rb;
    private float _move;
    private bool _jump;
    private bool _onWall;
    private float _jumpInput;
    private int _jumps;
    private Animator _anim;
    private PlayerInfo _playerInfo;
    private BoxCollider2D _wallCollider;


    private void Awake()
    {
        _jumps = 1;
        Armed = false;
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _groundChecker = transform.GetChild(1);
        _playerInfo = GetComponent<PlayerInfo>();
        _wallCollider = GetComponent<BoxCollider2D>();
        //Check if the character is facing left
        FacingRight = transform.localScale.x != -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Listening for input. Using player numbers to avoid creating individual scripts for each player. 
        _move = Input.GetAxis("L_XAxis_" + _playerInfo.PlayerNumber);
        _jumpInput = Input.GetAxis("A_" + _playerInfo.PlayerNumber);

        if (_jumpInput > 0) _jump = true;
        else _jump = false;

        //Flipping the player depending on the direction of motion
        if (_move > 0 && !FacingRight) Flip();
        else if (_move < 0 && FacingRight) Flip();

        //Tell the animator if we are walking or ducking
        _anim.SetFloat("Speed", Mathf.Abs(_move));

        if (_grounded) _jumps = 1;

        //It's okay to jump in Update (Change velocity) without deltaTime, because we are not adding force continiously
        Jump();

        //Tell the animator if we are grounded 
        _anim.SetBool("Grounded",_grounded);

        //Tell the animator if we are on a wall
        _anim.SetBool("OnWall", _onWall);
    }

    private void FixedUpdate()
    {
        //One way to check if the character are grounded. The layer mask decides what layers count as ground
        _grounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, _whatIsGround);

        //Move left or right
        _rb.velocity = new Vector2(_move * _speed, _rb.velocity.y);
    }


    private void Jump()
    {
        //Check if jump is pressed and if we have any jumps left
        if (!_jump || _jumps != 1) return;
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        _jumps = 0;
    }

    private void Flip()
    {
        //Flipping the character by inverting the x-scale
        FacingRight = !FacingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        string tag = collision.tag;
        if(tag == "Platform")
        {
            _onWall = true;
            _jumps = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.tag;
        if(tag == "Platform")
        {
            _onWall = false;
        }
    }
}
