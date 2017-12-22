using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePractice : MonoBehaviour {

    private float xSeparation = 0;
    private GameObject[] arrParticleSystems;
    public GameObject sampleParticleSystem;
    

	// Use this for initialization
	void Start () {
        
        arrParticleSystems = new GameObject[AudioHelper.energyHistory.Length];

        for (int i = 0; i < 32; i++)
        {
            GameObject instanceParticleSystem = Instantiate(sampleParticleSystem);

            instanceParticleSystem.transform.position = transform.position;
            instanceParticleSystem.transform.parent = transform;
            instanceParticleSystem.name = "jiggyjarjar" + i;

            instanceParticleSystem.transform.position = new Vector3(
                                                            transform.position.x + xSeparation,
                                                            transform.position.y, 0);
            xSeparation += 8f / 32;

            arrParticleSystems[i] = instanceParticleSystem;
        }

        transform.Rotate(new Vector3(0, 0, 90));
    }
	
	// Update is called once per frame
	void Update () {
        
        for (int i = 0; i < AudioHelper.beatByFreq.Length; i++)
        {
            if (AudioHelper.beatByFreq[i])
            {
                arrParticleSystems[i].GetComponent<ParticleSystem>().Emit(1);
            }
        }
	}
}
