using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    private AudioSource theAudio;
    private float audioLvl;
    public float defaultAduio;

    // Use this for initialization
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
    }

    public void SetAudioLvl(float volume)
    {
        if (theAudio == null)
        {
            theAudio = GetComponent<AudioSource>();
        }

        audioLvl = defaultAduio * volume;
        theAudio.volume = audioLvl;
    }
}
