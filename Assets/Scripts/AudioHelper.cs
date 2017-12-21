using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioHelper : MonoBehaviour {

    AudioSource song;
    public static int sampleSize = 512;
    public static float[] spectrum;
    public static float[] freqBands = new float[8];

	// Use this for initialization
	void Start ()
    {
        
        song = GetComponent<AudioSource>();
        spectrum = new float[sampleSize];
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
	}

    void GetSpectrumAudioSource()
    {
        song.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    }

    void MakeFrequencyBands()
    {
        /*
         * hz / sampleSize = hz/sample
         * 
         * 44100 / 512 ~= 86 hz/sample
         *    samples
         * 0    2   =   172
         * 1    4   =   344
         * 2    8   =   688
         * 3    16  =   1376
         * 4    32  =   2752
         * 5    64  =   5504
         * 6    128 =   11008
         * 7    256 =   22016
         *    510
        */

        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += spectrum[count] * (count + 1); // >> * (count + 1) << to compensate for peaks being lower at higher frequencies
                count++;
            }

            average /= count;

            freqBands[i] = average * 10;
        }
    }
}
