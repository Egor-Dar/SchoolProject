using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class PlayerControl : MonoBehaviour
{
    public GameObject explosionEffect;

    private PlayerControll _playerControll;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private Transform feetPos;
    [SerializeField] private TextController Canvas;
    private Rigidbody2D _rigidbody2D;
    private bool isGround;

    private void OnEnable()
    {
        _playerControll.Player.Shoot.performed += context => Boom();
        _playerControll.Player.Jump.performed += context => Jump();
        _playerControll.Enable();

    }

    private void OnDisable()
    {
        _playerControll.Disable();
    }

    private void Awake()
    {
        _playerControll = new PlayerControll();
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 direction = _playerControll.Player.Move.ReadValue<Vector2>();
        Move(direction);
    }

    public int enemyHealth;
    public int playerDamage;
    public event EventHandler Attack;

    void Boom()
    {
        //Show effect

        //Ger nearby objects
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 5);

        foreach (var nearbyObject in collider2Ds)
        {
            if (nearbyObject.gameObject.layer == 10)
            {
                if (Vector2.Distance(nearbyObject.transform.position, transform.position) < 5)

                {
                    var heading = nearbyObject.transform.position - transform.position;
                    var distance = heading.magnitude;
                    var direction = heading / distance;
                    RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction);
                    _playerControll.Disable();
                    if (hit2D)
                    {
                        var effect = Instantiate(explosionEffect, hit2D.point, Quaternion.identity);
                        Destroy(effect, 3f);
                    }

                    nearbyObject.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * 20, ForceMode2D.Impulse);
                    Attack(this, EventArgs.Empty);
                    Time.timeScale = 0;
                }
            }
        }
        //Add force
        //damage

        //remove this object
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(feetPos.position, checkRadius, WhatIsGround);
    }

    private void Move(Vector2 direction)
    {
        var particle = GetComponentInChildren<ParticleSystem>();
        float ScaleMoveSpeed = _moveSpeed * Time.deltaTime;

        _rigidbody2D.velocity = new Vector2(direction.x * _moveSpeed, _rigidbody2D.velocity.y);
        if (direction.x < 0)
        {
            particle.startSpeed = 5;
        }
        else
        {
            particle.startSpeed = -5;
        }
    }

    private void Jump()
    {
        if (isGround)
        {
            Debug.Log("Jump");
            _rigidbody2D.velocity = Vector2.up * _jumpForce;
        }
    }
}