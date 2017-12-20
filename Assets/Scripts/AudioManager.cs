using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private AudioSource song;

    public float spawnInvokeBetweenTime;
    public float spawnStartTime = 5;
    public float spawnForTime = 15;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    public float enemyRate;

    // Use this for initialization
    void Start ()
    {
        song = GetComponent<AudioSource>();

        spawnForTime += spawnStartTime;
        InvokeRepeating("Spawn", spawnStartTime, spawnInvokeBetweenTime);
    }
	
	// Update is called once per frame
	void Update ()
    {
        enemyRate = 0;

        float[] samples = new float[256];
        float[] spectrum = new float[512];

        song.GetOutputData(samples, 0);
        song.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        for (int i = 1; i < samples.Length - 1; i++)
        {
            enemyRate += samples[i];
        }

        spawnForTime -= Time.deltaTime;
    }

    public void Spawn()
    {
        /*if (spawnForTime >= 0)
        {
            Instantiate(enemy1, spawnPoint1.position, spawnPoint1.rotation);
            Instantiate(enemy2, spawnPoint2.position, spawnPoint2.rotation);
        }*/

        if (enemyRate > 2)
        {
            Instantiate(enemy3, spawnPoint3.position, spawnPoint3.rotation);
        }

        if (enemyRate > 1)
        {
            Instantiate(enemy1, spawnPoint1.position, spawnPoint1.rotation);
            Instantiate(enemy2, spawnPoint2.position, spawnPoint2.rotation);
            Instantiate(enemy1, new Vector3(spawnPoint1.position.x + 0.5f, spawnPoint1.position.y, 0), spawnPoint1.rotation);
            Instantiate(enemy2, new Vector3(spawnPoint2.position.x + 0.5f, spawnPoint2.position.y, 0), spawnPoint2.rotation);
        }
    }
}
