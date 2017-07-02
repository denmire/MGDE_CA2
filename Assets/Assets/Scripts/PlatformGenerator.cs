using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    public GameObject platform;
    public Transform generationPt;
    
    public float distBetweenMin;
    public float distBetweenMax;

    //public GameObject enemy;
    private float distBetween;
    private float platformWidth;

    public ObjectPooler ObjectPool;

	// Use this for initialization
	void Start () {
        //width of platform's box collider
        platformWidth = platform.GetComponent<BoxCollider2D>().size.x;

	}
	
	// Update is called once per frame
	void Update () {

        Spawnfloor();
        //SpawnEnemy();

    }

    void Spawnfloor()
    {
        if(transform.position.x < generationPt.position.x)
        {

            distBetween = Random.Range(distBetweenMin, distBetweenMax);

            transform.position = new Vector3(transform.position.x + platformWidth + distBetween, transform.position.y, transform.position.z);

            //Instantiate(platform, transform.position, transform.rotation);

            GameObject newPlatform = ObjectPool.GetPooledObj();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

        }


    }
    /*
    void SpawnEnemy()
    {
        if (enemy.transform.position.x < generationPt.position.x)
        {
            enemy.transform.position = new Vector3(Random.Range(platformWidth - 1, platformWidth)+ distBetween + enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
            
            Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
        }
    }
    */

}
