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
        // damageBurst = GameObject.Find("BloodBurst");

    }

    // Update is called once per frame
    void Update()
    {
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

        if (other.gameObject.tag == "Enemy" && playerEngagement.thePlayerDeathStrike || other.gameObject.tag == "LargeEnemyBasic" && playerEngagement.thePlayerDeathStrike || other.gameObject.tag == "BasicRangedEnemy" && playerEngagement.thePlayerDeathStrike
            || other.gameObject.tag == "Enemy1")
        {
            enemyHit = true;
            staminaManager.playerCurrentStamina += 20;

            if (playerEngagement.attacking && thePlayer.damagePossible
            && playerEngagement.faceOff)
            {
                playerEngagement.strikeBlock = true;
                sfxMan.swordsColliding.volume = 1;
                sfxMan.swordsColliding.Play();
                Instantiate(swordClash, swordClashPoint.position, swordClashPoint.rotation);
            }
            //was else if
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
                    // currentDamage = damageToGive + thePS.currentAttack + 1;
                    // Debug.Log(currentDamage);
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
        else if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<EnemyTestScript>().enemyShield && !thePlayer.deathStrike
            || other.gameObject.tag == "LargeEnemyBasic" && other.gameObject.GetComponent<EnemyTestScript>().enemyShield && !thePlayer.deathStrike)//
        {
            //enemyShieldStrike = true;
            other.gameObject.GetComponent<EnemyTestScript>().enemyShieldStrike = true;
            sfxMan.swordsColliding.volume = 1;
            sfxMan.swordsColliding.Play();
            Instantiate(swordClash, swordClashPoint.position, swordClashPoint.rotation);
        }
    }

    // public void doingDamage(int currentDamage, GameObject knifeInstance)
    // {
    //     rangedDmg++;
    //     if (!knifeInstance.GetComponent<RangedDamage>().rangedDeathStrike && rangedDmg >= 2)
    //     {
    //         playerStaminaMan.playerCurrentStamina -= 2000;
    //         sfxMan.swordsColliding.volume = 1;
    //         sfxMan.swordsColliding.Play();
    //         Instantiate(swordClash, hitPoint.position, hitPoint.rotation);
    //         rangedDmg = 0;
    //         return;
    //     }
    //     else if (rangedDmg >= 2 && knifeInstance.GetComponent<RangedDamage>().rangedDeathStrike)
    //     {
    //         playerHealth.playerCurrentHealth -= currentDamage;
    //         Instantiate(bloodBurst, hitPoint.position, hitPoint.rotation);
    //         sfxMan.blood.Play();
    //         rangedDmg = 0;
    //     }
    // }
}
