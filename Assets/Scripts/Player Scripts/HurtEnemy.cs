using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    private int damageToGive;
    public int currentDamage;
    public GameObject damageBurst;
    public Transform hitPoint;
    public GameObject damageNumber;
    public GameObject swordClash;
    public Transform swordClashPoint;
    private PlayerStats thePS;
    public bool enemyHit;
    private EnemyTestScript theEnemy;
    private PlayerController thePlayer;
    private SFXManager sfxMan;
    private HurtPlayerUpdated hurtPlayer;
    private EngagedWithPlayer playerEngagement;
    private PlayerStaminaManager staminaManager;
    public bool recovVar;
    private PlayerStats playerStats;
    float freezeFrame;
    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>();

        enemyHit = false;
        thePlayer = FindObjectOfType<PlayerController>();
        hurtPlayer = FindObjectOfType<HurtPlayerUpdated>();
        staminaManager = FindObjectOfType<PlayerStaminaManager>();
        playerStats = FindObjectOfType<PlayerStats>();

        thePS = FindObjectOfType<PlayerStats>();

        recovVar = false;

        damageToGive = thePS.playerDamage;
        freezeFrame = 0.8f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        try
        {
            playerEngagement = other.gameObject.transform.GetChild(0).GetComponent<EngagedWithPlayer>();
        }
        catch
        {
            return;
        }
        theEnemy = other.gameObject.GetComponent<EnemyTestScript>();

        // FreezeFrame();

        if (other.gameObject.tag == "Enemy" && playerEngagement.thePlayerDeathStrike || other.gameObject.tag == "LargeEnemyBasic" && playerEngagement.thePlayerDeathStrike || other.gameObject.tag == "BasicRangedEnemy" && playerEngagement.thePlayerDeathStrike ||
            other.gameObject.tag == "Enemy1")
        {
            enemyHit = true;
            // staminaManager.playerCurrentStamina += 20;

            // Animator anim2 = other.gameObject.GetComponent<Animator>();
            // if (thePlayer.recovAttack)
            // {
            //     anim2.enabled = false;
            // }
            // else
            // {
            //     anim2.enabled = true;
            // }

            if (playerEngagement.attacking && thePlayer.damagePossible &&
                playerEngagement.faceOff)
            {
                playerEngagement.strikeBlock = true;
                sfxMan.swordsColliding.volume = 1;
                sfxMan.swordsColliding.Play();
                Instantiate(swordClash, swordClashPoint.position, swordClashPoint.rotation);
            }

            if (!thePlayer.noDamageIsTaken && !playerEngagement.attacking)
            {
                if (thePlayer.wasSprint && staminaManager.playerCurrentStamina > 50 && playerStats.dexterity >= 14 && playerStats.strength >= 9)
                {
                    if (playerStats.strength == 11)
                    {
                        currentDamage = damageToGive + thePS.currentAttack + 2;
                    }
                    else
                    {
                        currentDamage = damageToGive + thePS.currentAttack + 1;
                    }
                }
                else
                {
                    if (playerStats.strength == 11)
                    {
                        currentDamage = damageToGive + thePS.currentAttack + 1;
                    }
                    else
                    {
                        currentDamage = damageToGive + thePS.currentAttack;
                    }
                }

                sfxMan.blood.Play();

                if (this.gameObject.tag == "Throwing Knife")
                {
                    other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(2);
                    Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
                    var clone = (GameObject)Instantiate(damageNumber, hitPoint.position,
                        Quaternion.Euler(Vector3.zero));
                    clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
                }
                else
                {
                    other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDamage);
                    Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
                    var clone = (GameObject)Instantiate(damageNumber, hitPoint.position,
                        Quaternion.Euler(Vector3.zero));
                    clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
                }
            }
        }
        else if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<EnemyTestScript>().enemyShield && !thePlayer.deathStrike ||
            other.gameObject.tag == "LargeEnemyBasic" && other.gameObject.GetComponent<EnemyTestScript>().enemyShield && !thePlayer.deathStrike) //
        {
            other.gameObject.GetComponent<EnemyTestScript>().enemyShieldStrike = true;
            sfxMan.swordsColliding.volume = 1;
            sfxMan.swordsColliding.Play();
            theEnemy.blockCounter++;
            Instantiate(swordClash, swordClashPoint.position, swordClashPoint.rotation);
        }
    }
}