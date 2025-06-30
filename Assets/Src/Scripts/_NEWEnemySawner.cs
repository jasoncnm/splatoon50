using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySawner : MonoBehaviour
{
    public float spawnRate = 1f;
    public GameObject enemyPrefabs;
    public bool canSpawn = true;
    private void Start()
    {
        StartCoroutine(Spawner());
    }


    IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        for(;canSpawn;)
        {
           
            yield return wait;
            GameObject enemys = enemyPrefabs;
            Instantiate(enemys, transform.position, Quaternion.identity);
        }
    }

}
