using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour {

    public GameObject sampleCube;
    private int sampleSize;
    private GameObject[] arrSampleCube;

    private float xSeparation = 0;

    public float maxScale;

    // Use this for initialization
    private void Start()
    {
        sampleSize = AudioHelper.sampleSize;
        arrSampleCube = new GameObject[sampleSize];

        for (int i = 0; i < sampleSize; i++)
        {
            GameObject instanceSampleCube = Instantiate(sampleCube);

            instanceSampleCube.transform.position = transform.position;
            instanceSampleCube.transform.parent = transform;
            instanceSampleCube.name = "jiggyjarjar" + i;

            instanceSampleCube.transform.position = new Vector3(
                                                            transform.position.x + xSeparation,
                                                            transform.position.y, 0);
            xSeparation += 8f / sampleSize;

            arrSampleCube[i] = instanceSampleCube;
            
        }
        
    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < sampleSize; i++)
        {

            if (arrSampleCube != null)
            {
                
                arrSampleCube[i].transform.localScale = new Vector3(0.1f, (AudioHelper.spectrum[i] * maxScale) * i, 1);
                
            }
        }
        
	}
}
