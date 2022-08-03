using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioactiveBulletsSpawner : MonoBehaviour
{
    public GameObject RadioactiveBullet;
    private float deltaTime = 4.2f;
    void Start()
    {
        StartCoroutine(SpawnBullet());
    }

    IEnumerator SpawnBullet()
    {
        Debug.Log("Spawning bullet...");
        Instantiate(RadioactiveBullet, this.transform, true);
        yield return new WaitForSeconds(deltaTime);

        StartCoroutine(SpawnBullet());
    }
}
