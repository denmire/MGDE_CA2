using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    public GameObject platform;
    public Transform generationPt;
    public float distBetween;

    private float platformWidth;

	// Use this for initialization
	void Start () {
        //width of platform's box collider
        platformWidth = platform.GetComponent<BoxCollider2D>().size.x;

	}
	
	// Update is called once per frame
	void Update () {

        Spawnfloor();

    }

    void Spawnfloor()
    {
        if(transform.position.x < generationPt.position.x)
        {
            transform.position = new Vector3(transform.position.x + platformWidth + distBetween, transform.position.y, transform.position.z);

            Instantiate(platform, transform.position, transform.rotation);
        }


    }


}
