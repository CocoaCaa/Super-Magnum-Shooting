using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleCrateSpawner : MonoBehaviour
{
    public GameObject moleCrate;
    public AudioClip spawnSound;

    public void StartSpawn()
    {
        StartCoroutine(HandleStartSpawn());
    }

    private IEnumerator HandleStartSpawn()
    {
        yield return new WaitForSeconds(Random.Range(10.0f, 30.0f));
        var clonedTargetObj = Instantiate(moleCrate);
        clonedTargetObj.transform.position = transform.position;
        GetComponent<AudioSource>().PlayOneShot(spawnSound);
    }
}
