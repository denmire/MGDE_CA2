using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class PlayerScript : MonoBehaviour
{

    bool mainWeapon = true;
    bool Pressed = false;
    [SerializeField]
    float bulletCooldown = .25f;
    float Timer = .25f;
    [SerializeField]
    float bulletSpeed = 2f;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    Rigidbody2D bulletRB;
    [SerializeField]
    Transform bulletSpawn;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }
    private void Update()
    {
        Timer += Time.deltaTime;

        if (Timer > bulletCooldown && Pressed)
        {
            Rigidbody2D rb;

            animator.SetBool("Shoot", true);
            rb = Instantiate(bulletRB, bulletSpawn.position, bulletSpawn.rotation);

            rb.AddForce(bulletSpeed * bulletSpawn.right);

            Timer = 0;
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