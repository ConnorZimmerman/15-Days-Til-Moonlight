using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public VolumeController[] vcObjects;
    public float currentVolumeLvl;
    public float maxVolumeLvl = 1.0f;

    // Use this for initialization
    void Start()
    {
        vcObjects = FindObjectsOfType<VolumeController>();

        if (currentVolumeLvl > maxVolumeLvl)
        {
            currentVolumeLvl = maxVolumeLvl;
        }

        for (int i = 0; i < vcObjects.Length; i++)
        {
            if (vcObjects[i].name == "Overworld")
            {
                continue;
            }
            vcObjects[i].SetAudioLvl(currentVolumeLvl);
        }
    }
}