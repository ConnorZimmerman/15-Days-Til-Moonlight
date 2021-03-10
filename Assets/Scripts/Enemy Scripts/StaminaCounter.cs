using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaCounter : MonoBehaviour
{
    private HurtPlayerUpdated hurtPlayer;
    private PlayerStaminaManager playerStamina;
    private SFXManager sfxMan;
    public int counter;
    private EngagedWithPlayer playerEngagement;
    private ShieldBlock playerShield;
    public Transform hitPoint;
    public GameObject swordClash;

    // Use this for initialization
    void Start()
    {
        playerStamina = FindObjectOfType<PlayerStaminaManager>();
        hurtPlayer = FindObjectOfType<HurtPlayerUpdated>();
        sfxMan = FindObjectOfType<SFXManager>();
        playerEngagement = FindObjectOfType<EngagedWithPlayer>();
        playerShield = FindObjectOfType<ShieldBlock>();
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
    }
}