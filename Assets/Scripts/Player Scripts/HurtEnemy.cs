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

    private EnemyStaminaManager enemyStaminaMan;

    public bool enemyHit;

    private EnemyTestScript theEnemy;
    private PlayerController thePlayer;

    private SFXManager sfxMan;

    private HurtPlayerUpdated hurtPlayer;
    private EngagedWithPlayer playerEngagement;

    public bool beforeRecov;
    public bool recovVar;

    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>();

        enemyHit = false;

        //theEnemy = FindObjectOfType<EnemyTestScript>();
        thePlayer = FindObjectOfType<PlayerController>();
        hurtPlayer = FindObjectOfType<HurtPlayerUpdated>();
        //playerEngagement = FindObjectOfType<EngagedWithPlayer>();

        //enemyStaminaMan = FindObjectOfType<EnemyStaminaManager>();

        thePS = FindObjectOfType<PlayerStats>();

        recovVar = false;
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
        catch{
            return;
        }
        enemyStaminaMan = other.gameObject.GetComponent<EnemyStaminaManager>();
        theEnemy = other.gameObject.GetComponent<EnemyTestScript>();

        if (other.gameObject.tag == "Enemy" && playerEngagement.thePlayerDeathStrike || other.gameObject.tag == "LargeEnemyBasic" && playerEngagement.thePlayerDeathStrike || other.gameObject.tag == "BasicRangedEnemy" //&& playerEngagement.thePlayerDeathStrike
            || other.gameObject.tag == "Enemy1")
        {
            enemyHit = true;

            if (playerEngagement.attacking && thePlayer.damagePossible
      && playerEngagement.faceOff)
            {
                playerEngagement.strikeBlock = true;
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


        //switched order of operations
        else if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<EnemyTestScript>().enemyShield
            || other.gameObject.tag == "LargeEnemyBasic" && other.gameObject.GetComponent<EnemyTestScript>().enemyShield)//!thePlayer.deathStrike)//
        {
            enemyStaminaMan.enemyCurrentStamina -= 400;
            sfxMan.swordsColliding.Play();
            Instantiate(swordClash, swordClashPoint.position, swordClashPoint.rotation);

            //adding return to test
            //return;
        }

        //trying to make clash when hit inanimate objects, but it's not working yet
        /*else if(other.gameObject.tag != "Enemy")
        {
            sfxMan.swordsColliding.Play();
            Instantiate(swordClash, swordClashPoint.position, swordClashPoint.rotation);
        }*/


    }
}
