using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

    public ObjectPooler zombiePool;
    public float distBetwZombies;
    

    public void spawnZombie(Vector3 startPos)
    {
        GameObject zombie1 = zombiePool.GetPooledObj();
        zombie1.transform.position = startPos;
        zombie1.SetActive(true);

        /*
        GameObject zombie2 = zombiePool.GetPooledObj();
        zombie2.transform.position = new Vector3(startPos.x - distBetwZombies, startPos.y, startPos.z);
        zombie2.SetActive(true);
        */
        
        /*
        GameObject zombie3 = zombiePool.GetPooledObj();
        zombie3.transform.position = new Vector3(startPos.x + distBetwZombies, startPos.y, startPos.z);
        zombie3.SetActive(true);
        */
    }

}
