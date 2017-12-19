using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    public float spawnTime;
    public float spawnStartTime = 5;
    public float spawnForTime = 15;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public GameObject enemy1;
    public GameObject enemy2;

	// Use this for initialization
	void Start ()
    {
        spawnForTime += spawnStartTime;
        InvokeRepeating("Spawn", spawnStartTime, spawnTime);
	}

    private void Update()
    {
        spawnForTime -= Time.deltaTime;
    }

    public void Spawn ()
    {
        if (spawnForTime >= 0)
        {
            Instantiate(enemy1, spawnPoint1.position, spawnPoint1.rotation);
            Instantiate(enemy2, spawnPoint2.position, spawnPoint2.rotation);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
