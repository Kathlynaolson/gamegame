using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour {

    public int band;
    private float scaleMultiplier;
    

	// Use this for initialization
	void Start () {
        scaleMultiplier = 5f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(
                         transform.localScale.x,
                         AudioHelper.freqBands[band] * scaleMultiplier, 
                         transform.localScale.z);

	}
}
