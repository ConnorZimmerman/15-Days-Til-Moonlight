using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaCounter : MonoBehaviour
{
    private HurtPlayerUpdated hurtPlayer;

    // private EnemyStaminaManager enemyStamina;
    private PlayerStaminaManager playerStamina;

    private SFXManager sfxMan;

    public int counter;

    private EngagedWithPlayer playerEngagement;
    //private PlayerController thePlayer;
    private ShieldBlock playerShield;

    public Transform hitPoint;
    public GameObject swordClash;

    // Use this for initialization
    void Start()
    {
        // enemyStamina = FindObjectOfType<EnemyStaminaManager>();
        playerStamina = FindObjectOfType<PlayerStaminaManager>();

        hurtPlayer = FindObjectOfType<HurtPlayerUpdated>();

        sfxMan = FindObjectOfType<SFXManager>();

        playerEngagement = FindObjectOfType<EngagedWithPlayer>();

        playerShield = FindObjectOfType<ShieldBlock>();

        //thePlayer = FindObjectOfType<PlayerController>();

        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (playerEngagement.playerStaminaDrain && playerShield.shieldOn)
        {
            playerStamina.playerCurrentStamina -= 200;
            Instantiate(swordClash, hitPoint.position, hitPoint.rotation);
        }
        */

        //enemyStamina.enemyCurrentStamina -= 400;

        //sfxMan.enemyAttack.Play();

        counter++;
    }
}
