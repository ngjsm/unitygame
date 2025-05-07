using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public float spawnInterval = 2f;
    public float xRange = 8f;
    public float yRange = 4f;
    public Vector3 spawnPoint;

    private float spawnDuration = 30f;
    private GameObject targetFruitPrefab;

    void Start()
    {
        int targetIndex = Random.Range(0, fruitPrefabs.Length);
        targetFruitPrefab = fruitPrefabs[targetIndex];
        score.Instance.SetTargetName(targetFruitPrefab.name); // Score 스크립트에서 타겟 이름 지정

        InvokeRepeating("SpawnFruit", 1f, spawnInterval);
        Invoke("StopSpawning", spawnDuration);
    }

    void SpawnFruit()
    {
        int index = Random.Range(0, fruitPrefabs.Length);
        Vector3 spawnPos = spawnPoint + new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), 0);
        GameObject fruit = Instantiate(fruitPrefabs[index], spawnPos, Quaternion.identity);

        if (fruitPrefabs[index] == targetFruitPrefab)
            fruit.tag = "Target";
        else
            fruit.tag = "Fruit";
    }

    void StopSpawning()
    {
        CancelInvoke("SpawnFruit");
    }
}