using System.Collections;
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
    float phoneInitialTilt;
    [SerializeField]
    float phoneSubcequentTilt;
    [SerializeField]
    double tiltThreshold;
    float tiltCD = 1;
    float tiltTimer = 1;
    [SerializeField]
    float jumpHeight = 5;

    [SerializeField]
    float playerSpeed;
    [SerializeField]
    float playerMaxSpeed;

    private Animator animator;
    [SerializeField]
    Rigidbody2D bulletRB;
    Rigidbody2D playerRB;
    [SerializeField]
    Transform bulletSpawn;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        playerRB = this.GetComponent<Rigidbody2D>();
        playerRB.velocity = new Vector2(0, 0);
    }
    private void Update()
    {
        bulletTimer += Time.deltaTime;
        tiltTimer += Time.deltaTime;

        animator.SetFloat("Speed", Mathf.Abs(Input.acceleration.y));
        playerSpeed *= Input.acceleration.y;
        playerRB.velocity += new Vector2(Mathf.Clamp(playerSpeed, -playerMaxSpeed, playerMaxSpeed), 0);

        phoneInitialTilt = Input.acceleration.z;
        if (tiltTimer > tiltCD)
        {
            phoneSubcequentTilt = Input.acceleration.z;
            double phoneTiltAngle = tiltThreshold / 180.0;
            if ((phoneSubcequentTilt - phoneInitialTilt) >= phoneTiltAngle)
            {
                playerRB.velocity += new Vector2(0, jumpHeight);
            }
        }
        animator.SetFloat("Height", playerRB.velocity.y);

        if (bulletTimer > bulletCooldown && Pressed)
        {
            Rigidbody2D rb;

            animator.SetBool("Shoot", true);
            Debug.Log("Shoot");
            rb = Instantiate(bulletRB, bulletSpawn.position, bulletSpawn.rotation);

            rb.AddForce(bulletSpeed * bulletSpawn.right);

            bulletTimer = 0;
        }
        else if (!Pressed)
        {
            animator.SetBool("Shoot", false);
        }

        Debug.Log(Pressed);
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
}