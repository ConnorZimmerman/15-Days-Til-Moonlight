using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource playerHurt;
    public AudioSource playerAttack;
    public AudioSource enemyAttack;
    public AudioSource swordsColliding;
    public AudioSource blood;
    public AudioSource waterWalk;
    private static bool sfxManager;

    // Use this for initialization
    void Start()
    {
        if (!sfxManager)
        {
            sfxManager = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}