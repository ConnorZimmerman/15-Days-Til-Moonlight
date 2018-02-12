using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damageToGive;
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
    public bool recovVar;
    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>();

        enemyHit = false;
        thePlayer = FindObjectOfType<PlayerController>();
        hurtPlayer = FindObjectOfType<HurtPlayerUpdated>();

        thePS = FindObjectOfType<PlayerStats>();

        recovVar = false;
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
                if (thePlayer.wasSprint)
                {
                    currentDamage = damageToGive + thePS.currentAttack + 1;
                }
                else
                {
                    currentDamage = damageToGive + thePS.currentAttack;
                }
                sfxMan.blood.Play();
                other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDamage);
                Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
                var clone = (GameObject)Instantiate(damageNumber, hitPoint.position,
                    Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
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
