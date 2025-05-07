using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxtrig : MonoBehaviour
{
    public GameManager2 gameManager;
    public SpawnManager spawnManager;

    private void OnTriggerEnter(Collider other)
    {
        var fruit = other.GetComponent<fruit>();
        if (fruit == null) return;

        gameManager.OnFruitInBox(fruit.fruitType);
        Destroy(other.gameObject);

        spawnManager.SpawnRandomFruit();
    }
}