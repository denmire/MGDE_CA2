﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class PlayerScript : MonoBehaviour
{

    bool Pressed = false;
    [SerializeField]
    float bulletCooldown = .25f;
    float bulletTimer = .25f;
    [SerializeField]
    float bulletSpeed = 2f;

    [SerializeField]
    float jumpHeight = 3;
    [SerializeField]
    float jumpMaxHeight = 5;

    [SerializeField]
    float playerSpeed = 3;
    [SerializeField]
    float playerMaxSpeed = 5;

    private Animator animator;
    [SerializeField]
    Rigidbody2D bulletRB;
    Rigidbody2D playerRB;
    [SerializeField]
    Transform bulletSpawn;
    BoxCollider2D playerCollider;
    [SerializeField]
    Collider2D[] colliderGround;

    private void Start()
    {
        colliderGround = Physics2D.OverlapBoxAll(new Vector2(0, 0), new Vector2(Mathf.Infinity, 20), 0, 1 << LayerMask.NameToLayer("Terrain"));
        animator = this.GetComponent<Animator>();
        playerRB = this.GetComponent<Rigidbody2D>();
        playerCollider = this.GetComponent<BoxCollider2D>();
        playerRB.velocity = new Vector2(0, 0);
    }
    private void Update()
    {
        bulletTimer += Time.deltaTime;

        Vector3 tilt = Input.acceleration;
        tilt = Quaternion.Euler(60, 0, 0) * tilt;
        animator.SetFloat("Speed", Mathf.Abs(tilt.x));
        playerRB.velocity += new Vector2(tilt.x, 0);

        if (touchGround())
            playerRB.velocity += new Vector2(0, Mathf.Clamp(-tilt.y * jumpHeight,0,jumpMaxHeight));
        animator.SetBool("Height", !touchGround());

        if (bulletTimer > bulletCooldown && Pressed)
        {
            Rigidbody2D rb;

            animator.SetBool("Shoot", true);
            rb = Instantiate(bulletRB, bulletSpawn.position, bulletSpawn.rotation);

            rb.AddForce(bulletSpeed * bulletSpawn.right);

            bulletTimer = 0;
        }
        else if (!Pressed)
        {
            animator.SetBool("Shoot", false);
        }
    }

    void OnEnable()
    {
        LeanTouch.OnFingerDown += press;
        LeanTouch.OnFingerUp += press;
    }

    void OnDisable()
    {
        LeanTouch.OnFingerDown -= press;
        LeanTouch.OnFingerUp -= press;
    }

    void press(LeanFinger finger)
    {
        Pressed = !Pressed;
    }
    private bool touchGround() // checks if player is on ground, make sure player cant infinitely jump
    {
        for (int i = 0; i < colliderGround.Length; i++)
        {
            if (playerCollider.IsTouching(colliderGround[i]))
            {
                return true;
            }
        }
        return false;
    }
}