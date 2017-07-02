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

    public ObjectPooler[] ObjectPool;
    
    //public GameObject[] thePlatforms;
    private int platformSelector;
    private float[] platformWidths;

    private float minHeight;
    public Transform maxHeightPt;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    private ZombieSpawner zombieSpawner;
    public float randomZombieStorage;


	// Use this for initialization
	void Start () {
        //width of platform's box collider
        //platformWidth = platform.GetComponent<BoxCollider2D>().size.x;

        platformWidths = new float[ObjectPool.Length];
        
        for(int i = 0; i < ObjectPool.Length; i++)
        {
            platformWidths[i] = ObjectPool[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPt.position.y;

        zombieSpawner = FindObjectOfType<ZombieSpawner>();

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

            platformSelector = Random.Range(0, ObjectPool.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if(heightChange > maxHeight)
            {

                heightChange = maxHeight;

            }else if(heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distBetween, heightChange, transform.position.z);

            

            //Instantiate(/*platform*/ObjectPool[platformSelector], transform.position, transform.rotation);

            
            GameObject newPlatform = ObjectPool[platformSelector].GetPooledObj();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if (Random.Range(0f, 100f) < randomZombieStorage)
            {
                zombieSpawner.spawnZombie(new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y + 1f, transform.position.z));
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

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
