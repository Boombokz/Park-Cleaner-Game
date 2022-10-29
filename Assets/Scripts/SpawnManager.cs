using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnObjects;
    [SerializeField] private float spawnTime = 2;

    public void SpawnObjects(bool spawn)
    {
        if (spawn)
        {
            StartCoroutine(SpawnRoutine(spawn));
        }
        else
        {
            StopAllCoroutines();
        }

    }

    IEnumerator SpawnRoutine(bool spawn)
    {
        while (spawn)
        {
            yield return new WaitForSeconds(spawnTime);

            int randomIndex = Random.Range(0, spawnObjects.Length);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-8, 8), 15, Random.Range(-8, 8));

            Instantiate(spawnObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
        }
    }
}
