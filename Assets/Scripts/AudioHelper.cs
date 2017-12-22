using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioHelper : MonoBehaviour {

    AudioSource song;
    public float beatSensitivity = 1;
    public static int sampleSize = 1024;
    public static float[] spectrum;
    public static float[] freqBands = new float[8];
    public static float[] beatFreqBands;

    public static bool[] beatByFreq = new bool[32];
    public static float[,] energyHistory = new float[32, 43];
    // Use this for initialization
    void Start()
    {
        song = GetComponent<AudioSource>();
        spectrum = new float[sampleSize];
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        MakeBeatFrequencyBands(32);
        detectAllBeats();
    }

    void GetSpectrumAudioSource()
    {
        song.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    }

    void updateEnergyHistory(float[,] arr, int freqBandNum)
    {
        // shifts the 2nd dimension arr specfied to the right by one
        for (int i = 42; i > 0; i--)
            arr[freqBandNum, i] = arr[freqBandNum, i - 1];

        // adds new value to history
        energyHistory[freqBandNum, 0] = beatFreqBands[freqBandNum];
    }

    void MakeBeatFrequencyBands(int numOfBands)
    {
        beatFreqBands = new float[numOfBands];
        float average = 0;

        for (int i = 0; i < numOfBands; i++)
        {
            average = 0;
            int stopPoint = (i + 1) * numOfBands;
            for (int j = (i * numOfBands); j < stopPoint; j++)
                average += spectrum[j];
      
            average /= numOfBands; // Where the num of samples matters. Multiply by numOfBands/sampleSize which could be just /numOfBands

            beatFreqBands[i] = average;
            updateEnergyHistory(energyHistory, i);
        }

    }

    bool detectBeatOnBand (int freqBandNum)
    {
        float sum = 0;
        float average;
        for (int i = 0; i < 43; i++)
            sum += energyHistory[freqBandNum, i];
        average = sum / 43;

        if (beatFreqBands[freqBandNum] > (average * beatSensitivity))
            return true;
        
        return false;
    }

    void detectAllBeats()
    {
        for (int i = 0; i < 32; i++)
        {
            if (detectBeatOnBand(i))
            {
                beatByFreq[i] = true;
            }
            else
                beatByFreq[i] = false;
        }
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
