using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngagedWithPlayer : MonoBehaviour
{
    public int damageToGive;
    public int currentDamage;
    public GameObject bloodBurst;
    public Transform hitPoint;
    public GameObject damageNumber;
    public GameObject swordClash;
    public Transform swordClashPoint;
    public CircleCollider2D playerArea;
    private HurtPlayerUpdated hurtPlayer;
    private ShieldBlock shield;
    public bool shieldOn;
    private PlayerStats thePS;
    private EnemyTestScript enemy;
    private BasicRangedEnemy rangedEnemy;
    private GameObject enemyGameObject;
    public bool colliderOn;
    public bool attacking;
    private SFXManager sfxMan;
    public bool activateAction;
    public float bloodCounter;
    public bool bloodCounterOn;
    public bool engaged;
    public bool hit;
    public bool deathStrike;
    public bool thePlayerDeathStrike;
    private PlayerController thePlayer;
    private PlayerStaminaManager playerStaminaMan;
    private EnemyHealthManager enemyHealth;
    private bool noShieldBlood;
    private PlayerHealthManager playerHealth;
    private HurtEnemy hurtEnemy;
    private EnemyRangedAttack rangedAttack;
    public int check;
    public int playerInt;
    public bool playerStaminaDrain;
    public bool strikeBlock;
    public bool faceOff;
    public bool beforeRecov;
    public float enemyAttackCounter;
    public bool enemyDamagePossible;
    /*The enemy's (and player's attack) is currently set up to be 3 different blend trees. The following
     4 variables dictate which stage the enemy is in of its attack based on timers and bools*/
    public bool preAttack;
    public float preAttackCounter;
    public bool recovAttack;
    public float recovAttackCounter;
    public float waitOnStrikeBlock;
    private ShieldBlock playerShield;
    float localAttackTimer;
    bool localAttackLock;
    Transform enemyTransform;
    private EnemyMasterScript enemyMaster;
    public bool wallBlock;
    private static int rangedDmg = 0;
    public int enemyMoveDirectionX;
    public bool enemyShield;
    public bool correctSideForDeathStrikeBool;
    public bool enemyMoving;
    public bool following;
    public bool deathSeven;

    // Use this for initialization
    void Start()
    {
        hurtPlayer = FindObjectOfType<HurtPlayerUpdated>();

        playerStaminaMan = FindObjectOfType<PlayerStaminaManager>();

        thePlayer = FindObjectOfType<PlayerController>();

        sfxMan = FindObjectOfType<SFXManager>();

        shield = FindObjectOfType<ShieldBlock>();

        thePS = FindObjectOfType<PlayerStats>();
        enemyGameObject = this.gameObject.transform.parent.gameObject;
        enemy = enemyGameObject.GetComponent<EnemyTestScript>();
        rangedEnemy = enemyGameObject.GetComponent<BasicRangedEnemy>();
        enemyMaster = enemyGameObject.GetComponent<EnemyMasterScript>();
        enemyTransform = enemyGameObject.transform;

        rangedAttack = enemyGameObject.GetComponent<EnemyRangedAttack>();

        playerHealth = FindObjectOfType<PlayerHealthManager>();
        hurtEnemy = FindObjectOfType<HurtEnemy>();
        playerShield = FindObjectOfType<ShieldBlock>();
        hitPoint = GameObject.Find("Player").transform;
        bloodCounter = 10;
        deathStrike = false;
        thePlayerDeathStrike = false;

        playerStaminaDrain = false;

        faceOff = false;

        check = 0;

        beforeRecov = true;

        enemyAttackCounter = 1f;
        enemyDamagePossible = false;

        preAttack = false;
        recovAttack = false;

        preAttackCounter = 0.66f;
        recovAttackCounter = 0.3f;
        enemyAttackCounter = 0.06f;

    }

    // Update is called once per frame
    void Update()
    {
        damageToGive = enemyMaster.damageToGive;

        //Defining variables for enemy type
        if (enemyGameObject.tag == "Enemy")
        {
            enemy.engagedWithPlayerPrivateVariables(localAttackLock);
            enemyMoveDirectionX = enemy.moveDirectionX;
            enemyShield = enemy.enemyShield;
            correctSideForDeathStrikeBool = enemy.correctSideForDeathStrikeBool;
            enemyMoving = enemy.enemyMoving;
            following = enemy.following;
            deathSeven = enemy.deathSeven;
        }
        if (enemyGameObject.tag == "BasicRangedEnemy")
        {
            rangedEnemy.engagedWithPlayerPrivateVariables(localAttackLock);
            enemyMoveDirectionX = rangedEnemy.moveDirectionX;
            enemyShield = rangedEnemy.enemyShield;
            correctSideForDeathStrikeBool = rangedEnemy.correctSideForDeathStrikeBool;
            enemyMoving = rangedEnemy.enemyMoving;
            following = rangedEnemy.following;
            deathSeven = rangedEnemy.deathSeven;
        }

        playerInt = thePlayer.directionInt;

        if (shieldOn)
        {
            if (enemyMoveDirectionX - thePlayer.directionInt == -2
                || enemyMoveDirectionX - thePlayer.directionInt == 2)
            {
                deathStrike = false;
            }
            else
            {
                deathStrike = true;
            }
        }
        else
        {
            deathStrike = true;
        }

        if (enemyShield)
        {
            if (enemyMoveDirectionX - thePlayer.directionInt == -2 || !correctSideForDeathStrikeBool
                || enemyMoveDirectionX - thePlayer.directionInt == 2)
            {
                thePlayerDeathStrike = false;
            }
            else
            {
                thePlayerDeathStrike = true;
            }
        }
        else
        {
            thePlayerDeathStrike = true;
        }

        if (enemyShield)
        {
            thePlayer.damageBlock = true;
        }
        else
        {
            thePlayer.damageBlock = false;
        }

        if (enemyMoveDirectionX - thePlayer.directionInt == -2
            || enemyMoveDirectionX - thePlayer.directionInt == 2)
        {
            faceOff = true;
        }
        else
        {
            faceOff = false;
        }


        if (shield.shieldOn)
        {
            shieldOn = true;
        }
        else
        {
            shieldOn = false;
        }

        if (attacking)
        {
            bloodCounterOn = true;
        }

        if (bloodCounterOn)
        {
            bloodCounter -= Time.deltaTime;
        }

        if (bloodCounter <= 9.97f)
        {
            bloodCounter = 10;
            bloodCounterOn = false;
        }

        if (playerHealth.playerIsDead == true)
        {
            colliderOn = false;
            attacking = false;
        }
        if (enemyGameObject.tag == "Enemy")
        {
            if (localAttackLock == false && colliderOn && engaged
                && preAttackCounter == 0.66f && enemyAttackCounter == 0.06f
                && recovAttackCounter == 0.3f)
            {
                preAttack = true;
                enemyMoving = false;
                following = false;
            }
            if (preAttack)
            {
                preAttackCounter -= Time.deltaTime;
            }
            if (preAttackCounter <= 0)
            {
                enemyAttackCounter -= Time.deltaTime;
            }
            if (preAttackCounter <= 0 && enemyAttackCounter > 0 && !recovAttack
                && preAttack == true)
            {
                if (preAttackCounter <= 0 && preAttack)
                {
                    if (enemyMoveDirectionX == 0)
                    {
                        enemyTransform.position = new Vector2(enemyTransform.position.x,
                        enemyTransform.position.y + 0.31f);
                    }
                    if (enemyMoveDirectionX == 1)
                    {
                        enemyTransform.position = new Vector2(enemyTransform.position.x + 0.31f,
                        enemyTransform.position.y);
                    }
                    if (enemyMoveDirectionX == 2)
                    {
                        enemyTransform.position = new Vector2(enemyTransform.position.x,
                        enemyTransform.position.y - 0.31f);
                    }
                    if (enemyMoveDirectionX == 3)
                    {
                        enemyTransform.position = new Vector2(enemyTransform.position.x - 0.31f,
                        enemyTransform.position.y);
                    }
                    attacking = true;
                    preAttack = false;
                    noShieldBlood = true;

                    doingDamage();
                }
            }
            else
            {
                enemyDamagePossible = false;
            }

            if (enemyAttackCounter <= 0)
            {
                recovAttack = true;
                recovAttackCounter -= Time.deltaTime;
                attacking = false;
            }
            if (recovAttackCounter <= 0)
            {
                recovAttack = false;
                beforeRecov = true;
                preAttackCounter = 0.66f;
                recovAttackCounter = 0.3f;
                enemyAttackCounter = 0.06f;
                // enemyMoving = true;
                // following = true;
            }
            else if (!deathStrike)
            {
                if (shield.shieldOn)
                {
                    playerStaminaDrain = true;
                }

                if (attacking)
                {
                    if (enemyGameObject.tag == "Enemy")
                    {
                        sfxMan.enemyAttack.Play();
                    }
                }
            }
            else
            {
                attacking = false;
            }
        }
    }


    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Collision")
        {
            wallBlock = true;
            engaged = false;
            colliderOn = false;
        }
        else if (other.gameObject.tag == "Player")
        {
            engaged = true;
            colliderOn = true;
            following = false;
            enemyMoving = false;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            activateAction = false;
            colliderOn = false;
            following = true;
            enemyMoving = true;
        }
    }

    public void doingDamage()
    {
        if (playerStaminaDrain && playerShield.shieldOn && !deathStrike && colliderOn && !deathSeven)
        {
            playerStaminaMan.playerCurrentStamina -= 100;
            playerShield.shieldLockBool = true;
            sfxMan.swordsColliding.volume = 1;
            sfxMan.swordsColliding.Play();
            Instantiate(swordClash, hitPoint.position, hitPoint.rotation);
            return;
        }

        if (enemyAttackCounter > 0f && deathStrike && colliderOn && !deathSeven)
        {
            currentDamage = damageToGive - thePS.currentDefense;
            // Debug.Log("wtf");
            playerHealth.playerCurrentHealth -= currentDamage;
            sfxMan.blood.Play();
            if (enemyGameObject.tag == "Enemy")
            {
                sfxMan.enemyAttack.Play();
            }
            hurtPlayer.waitOnStrikeBlock = 1f;

            Instantiate(bloodBurst, hitPoint.position, hitPoint.rotation);
            var clone = (GameObject)Instantiate(damageNumber, hitPoint.position,
                Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
        }
        if (attacking && thePlayer.damagePossible
        && faceOff)
        {
            strikeBlock = true;

            thePlayer.noDamageIsTaken = true;
            if (enemyGameObject.tag == "Enemy")
            {
                sfxMan.enemyAttack.Play();
            }
            sfxMan.swordsColliding.volume = 1;
            sfxMan.swordsColliding.Play();
            Instantiate(swordClash, hitPoint.position, hitPoint.rotation);
        }
        else if (!deathSeven)
        {
            hit = false;
            if (enemyGameObject.tag == "Enemy" && attacking)
            {
                sfxMan.enemyAttack.Play();
            }
        }
    }

    public void doingDamage(int currentDamage, GameObject knifeInstance)
    {
        rangedDmg++;
        if (!knifeInstance.GetComponent<RangedDamage>().rangedDeathStrike && rangedDmg >= 2)
        {
            playerStaminaMan.playerCurrentStamina -= 50;
            sfxMan.swordsColliding.volume = 1;
            sfxMan.swordsColliding.Play();
            playerShield.shieldLockBool = true;
            Instantiate(swordClash, hitPoint.position, hitPoint.rotation);
            rangedDmg = 0;
            return;
        }
        else if (rangedDmg >= 2 && knifeInstance.GetComponent<RangedDamage>().rangedDeathStrike)
        {
            // Debug.Log("wtf");
            playerHealth.playerCurrentHealth -= currentDamage;
            Instantiate(bloodBurst, hitPoint.position, hitPoint.rotation);
            sfxMan.blood.Play();
            rangedDmg = 0;
        }
    }

    //current player ranged damage
    public void doingDamage(int currentDamage, GameObject knifeInstance, GameObject thePlayer)
    {
        // rangedDmg++;
        // if (rangedDmg >= 2)
        // {
        //     playerStaminaMan.playerCurrentStamina -= 50;
        //     sfxMan.swordsColliding.volume = 1;
        //     sfxMan.swordsColliding.Play();
        //     Instantiate(swordClash, hitPoint.position, hitPoint.rotation);
        //     rangedDmg = 0;
        //     return;
        // }
        // else if (rangedDmg >= 2 && knifeInstance.GetComponent<RangedDamage>().rangedDeathStrike)
        // {
        //     // playerHealth.playerCurrentHealth -= 1;
        //     Instantiate(bloodBurst, hitPoint.position, hitPoint.rotation);
        //     sfxMan.blood.Play();
        //     rangedDmg = 0;
        // }
    }
    public void enemyTestScriptVariables(bool newLocalAttackLock)
    {
        localAttackLock = newLocalAttackLock;
    }
}