using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private AudioSource song;

    public float spawnTime;
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
        InvokeRepeating("Spawn", spawnStartTime, spawnTime);

        //song.Play();
        /*
                float[] samples = song.GetOutputData()
                song.clip.GetData(samples, 0);
                for (int i = 0; i > samples.Length; i++)
                {
                    samples[i] = samples[i] * 0.5f;
                }

                song.clip.SetData(samples, 0); */
    }
	
	// Update is called once per frame
	void Update ()
    {
        enemyRate = 0;

        float[] spectrum = new float[512];

        song.GetOutputData(spectrum, 0);

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            //Debug.DrawLine(new Vector3(i - 1, 0, spectrum[i] + 10), new Vector3(i, 0, spectrum[i + 1] + 10), Color.red);
            //Debug.DrawLine(new Vector3(i - 1, 0, Mathf.Log(spectrum[i - 1] + 10)), new Vector3(i, 0, Mathf.Log(spectrum[i]) + 10), Color.cyan);
            //Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            //Debug.DrawLine(new Vector3(Mathf.Log(i - 1), 0, Mathf.Log(spectrum[i - 1])), new Vector3(Mathf.Log(i), 0, Mathf.Log(spectrum[i])), Color.blue);
            enemyRate += spectrum[i];
        }

        spawnForTime -= Time.deltaTime;
    }

    public void Spawn()
    {
        if (spawnForTime >= 0)
        {
            Instantiate(enemy1, spawnPoint1.position, spawnPoint1.rotation);
            Instantiate(enemy2, spawnPoint2.position, spawnPoint2.rotation);
        }

        if (enemyRate > 2)
        {
            Instantiate(enemy3, spawnPoint3.position, spawnPoint3.rotation);
        }

        if (enemyRate > 1)
        {
            Instantiate(enemy1, new Vector3(spawnPoint1.position.x, spawnPoint1.position.y, 0), spawnPoint1.rotation);
        }

        //else
        //{
        //    Destroy(gameObject);
        //}
    }
}
