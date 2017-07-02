using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public int giveScore;

    private Scoremanager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<Scoremanager>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "zombie")
        {
            scoreManager.AddScore(giveScore);
            Destroy(gameObject);
            collider.gameObject.SetActive(false);

        }

        if (collider.gameObject.CompareTag("terrain"))
        {
            Debug.Log("hello");
            Destroy(this.gameObject);
            
        }


    }
}
