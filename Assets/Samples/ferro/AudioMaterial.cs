using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaterial : MonoBehaviour
{
    public LoopbackAudio Audio;
    public Material mat;

    void Start()
    {
        
    }

    Color height; 
    public float normalizedVolume = 0.1f;

    void Update()
    {
        //vis.SetFloat("SpawnRate",100.0f*Audio.Average+100f*Audio.WeightedPostScaledSpectrumData[0]);
        //Audio.WeightedPostScaledSpectrumData[i]
        mat.SetFloat("AudioAverage", Audio.WeightedAverage);
        mat.SetFloat("AudioSub",Audio.WeightedPostScaledSpectrumData[0] );
               
        float[,] audioData = Audio.getBuffer(0);

        Texture2D texture = new Texture2D(Audio.bufferSize, Audio.SpectrumSize);

        for (int y = 0; y < Audio.SpectrumSize; y++)
        {
            for (int x = 0; x < Audio.bufferSize; x++)
            {
                height = new Color(
                    audioData[Audio.bufferSize-x-1, y]*normalizedVolume,
                    audioData[Audio.bufferSize-x-1, y]*normalizedVolume,
                    audioData[Audio.bufferSize-x-1, y]*normalizedVolume,
                    1f
                );
                texture.SetPixel(x, y, height);
            }
        }
        texture.Apply();
        mat.SetTexture("AudioSpectrum", texture);
    }
}
