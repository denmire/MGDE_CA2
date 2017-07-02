using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroy : MonoBehaviour {

    public GameObject PlatformDestroyPt;
    
	// Use this for initialization
	void Start () {

        PlatformDestroyPt = GameObject.Find("PlatformDestroyPoint");

	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.x < PlatformDestroyPt.transform.position.x)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
	}
}
