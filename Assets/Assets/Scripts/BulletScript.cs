using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("terrain"))
        {
            Debug.Log("hello");
            Destroy(this.gameObject);
        }
    }
}
