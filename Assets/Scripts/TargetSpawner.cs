using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject[] targetObjs;
    public float spawnRate = 1.0f;

    private float lastSpawnTime = 0;

    void Update()
    {
        float currentTime = Time.time;
        if (Time.time > lastSpawnTime + spawnRate)
        {
            Spawn();
            lastSpawnTime = currentTime;
        }
    }

    private void Spawn()
    {
        var clonedTargetObj = Instantiate(targetObjs[Random.Range(0, targetObjs.Length)]);
        clonedTargetObj.transform.position = transform.position;
    }
}
