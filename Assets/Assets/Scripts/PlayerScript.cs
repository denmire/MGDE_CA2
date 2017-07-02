using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    bool isDead;

    [SerializeField]
    float playerSpeed;
    [SerializeField]
    float maxSpeed;

    bool Pressed = false;
    [SerializeField]
    float bulletCooldown = .25f;
    float bulletTimer = .25f;
    [SerializeField]
    float bulletSpeed = 2f;

    [SerializeField]
    float jumpHeight = 5;
    [SerializeField]
    float tiltThreshold = .5f;
    [SerializeField]
    float tiltThresholdGyro = 30;
    float tilt1;
    float tilt2;
    float timer = 0;
    float timerCD = 0.3f;

    Animator animator;
    [SerializeField]
    Rigidbody2D bulletRB;
    Rigidbody2D playerRB;
    [SerializeField]
    Transform bulletSpawn;
    BoxCollider2D playerCollider;
    [SerializeField]
    Collider2D[] colliderGround;
    [SerializeField]
    Collider2D[] colliderEnemy;

    bool gyroAvailable;
    Gyroscope gyro;

    private void Start()
    {
        isDead = false;
        animator = this.GetComponent<Animator>();
        playerRB = this.GetComponent<Rigidbody2D>();
        playerCollider = this.GetComponent<BoxCollider2D>();

        gyroAvailable = enableGyro();

        playerRB.velocity = new Vector2(0, 0);
    }
    private void Update()
    {
        colliderGround = Physics2D.OverlapBoxAll(new Vector2(0, 0), new Vector2(Mathf.Infinity, 20), 0, 1 << LayerMask.NameToLayer("Terrain"));
        colliderEnemy = Physics2D.OverlapBoxAll(new Vector2(0, 0), new Vector2(Mathf.Infinity, 20), 0, 1 << LayerMask.NameToLayer("Enemy"));

        bulletTimer += Time.deltaTime;

        playerRB.velocity += new Vector2(playerSpeed, 0);

        if (playerRB.velocity.magnitude > maxSpeed) //makes sure player doesnt not accelerate past a certain speed
        {
            playerRB.velocity = playerRB.velocity.normalized * maxSpeed;
        }

        if (gyroAvailable)
        {
            //playerRB.velocity += new Vector2(gyro.rotationRateUnbiased.y, 0);

            if (gyro.rotationRateUnbiased.x > tiltThresholdGyro && touchGround())
                playerRB.velocity = new Vector2(0, jumpHeight);
            animator.SetBool("Jump", !touchGround());
        }
        else
        {
            Vector3 tilt = Input.acceleration;
            //animator.SetFloat("Speed", Mathf.Abs(tilt.x));
            //playerRB.velocity += new Vector2(tilt.x, 0);

            if (touchGround())
            {
                timer += Time.deltaTime;
                tilt = Input.acceleration;
                tilt1 = tilt.z;
                if (timer > timerCD)
                {
                    tilt = Input.acceleration;
                    tilt2 = tilt.z;
                    timer = 0;
                }
                if (tilt1 - tilt2 >= tiltThreshold || Input.GetKey(KeyCode.W))
                        playerRB.velocity += new Vector2(0, jumpHeight);
            }
            animator.SetBool("Jump", !touchGround());
        }

        if (touchEnemy())
        {
            isDead = true;
            Handheld.Vibrate();
        }

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
                return true;
        }
        return false;
    }

    private bool touchEnemy() // checks if player is on ground, make sure player cant infinitely jump
    {
        for (int i = 0; i < colliderEnemy.Length; i++)
        {
            if (playerCollider.IsTouching(colliderEnemy[i]))
                return true;
        }
        return false;
    }

    bool enableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        return false;
    }
}