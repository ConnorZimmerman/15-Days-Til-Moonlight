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
    public EnemyStaminaManager enemyStamina;

    private bool noShieldBlood;
    private PlayerHealthManager playerHealth;
    private HurtEnemy hurtEnemy;

    private EnemyRangedAttack rangedAttack;
    public int check;
    public int playerInt;
    //public int enemyInt;

    //private bool showBlood;
    public bool playerStaminaDrain;

    public bool strikeBlock;

    public bool faceOff;
    // private float waitOnStrikeBlock;

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
    //enemy.MoveDirectionX
    //enemy.engagedWithPlayerPrivateVariables(localAttackLock)
    //enemy.enemyShield
    //enemy.correctSideForDeathStrikeBool
    //enemy.enemyMoving
    //enemy.deathSeven

    public int enemyMoveDirectionX;
    public bool enemyShield;
    public bool correctSideForDeathStrikeBool;
    public bool enemyMoving;
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

        //enemy = FindObjectOfType<EnemyTestScript>();
        enemyGameObject = this.gameObject.transform.parent.gameObject;
        enemy = enemyGameObject.GetComponent<EnemyTestScript>();
        rangedEnemy = enemyGameObject.GetComponent<BasicRangedEnemy>();
        enemyMaster = enemyGameObject.GetComponent<EnemyMasterScript>();
        enemyStamina = enemyGameObject.GetComponent<EnemyStaminaManager>();
        enemyTransform = enemyGameObject.transform;

        rangedAttack = enemyGameObject.GetComponent<EnemyRangedAttack>();

        playerHealth = FindObjectOfType<PlayerHealthManager>();
        hurtEnemy = FindObjectOfType<HurtEnemy>();

        //enemyHealth = FindObjectOfType<EnemyHealthManager>();

        //enemyStamina = FindObjectOfType<EnemyStaminaManager>();

        playerShield = FindObjectOfType<ShieldBlock>();

        hitPoint = GameObject.Find("Player").transform;

        bloodCounter = 10;

        deathStrike = false;
        thePlayerDeathStrike = false;

        // showBlood = false;
        playerStaminaDrain = false;

        faceOff = false;

        //waitOnStrikeBlock = 1f;


        check = 0;

        beforeRecov = true;

        enemyAttackCounter = 1f;
        enemyDamagePossible = false;

        preAttack = false;
        recovAttack = false;

        preAttackCounter = 0.66f;
        recovAttackCounter = 0.3f;
        enemyAttackCounter = 0.06f;

        //wallBlock = false; //check if enemy is colliding with non-enemy object.

        //recently turned off, keep eye on ********************* 12/25
        // if (gameObject.transform.Find("Fred"))
        // {
        //     damageToGive = 3;
        //     currentDamage = 3;
        // }
        //if (gameObject.transform.Find("Big Fred"))
        //{
        //    damageToGive = 4;
        //    currentDamage = 4;
        //}
        //currentDamage = enemyMaster.damageToGive;
        //damageToGive = enemyMaster.damageToGive;

    }

    // Update is called once per frame
    void Update()
    {
        damageToGive = enemyMaster.damageToGive;
        //enemyTestScriptVariables(localAttackLock);

        //Defining variables for enemy type
        if (enemyGameObject.tag == "Enemy")
        {
            enemy.engagedWithPlayerPrivateVariables(localAttackLock);
            enemyMoveDirectionX = enemy.moveDirectionX;
            enemyShield = enemy.enemyShield;
            correctSideForDeathStrikeBool = enemy.correctSideForDeathStrikeBool;
            enemyMoving = enemy.enemyMoving;
            deathSeven = enemy.deathSeven;
            //enemyInt = enemy.moveDirectionX;
        }
        if (enemyGameObject.tag == "BasicRangedEnemy")
        {
            rangedEnemy.engagedWithPlayerPrivateVariables(localAttackLock);
            enemyMoveDirectionX = rangedEnemy.moveDirectionX;
            enemyShield = rangedEnemy.enemyShield;
            correctSideForDeathStrikeBool = rangedEnemy.correctSideForDeathStrikeBool;
            enemyMoving = rangedEnemy.enemyMoving;
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
            if (localAttackLock == false /*&& enemy.actionTimer >= 0.17f*/ && colliderOn && engaged
                && preAttackCounter == 0.66f && enemyAttackCounter == 0.06f
                && recovAttackCounter == 0.3f)
            {
                // enemy.actionTimer -= Time.deltaTime;

                if (enemyStamina.enemyCurrentStamina >= 800)
                {
                    preAttack = true;
                    enemyMoving = false;

                }
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

                    //moved from above
                    //preAttack = false;
                    //specialMove = true;
                    if (enemyMoveDirectionX == 0)
                    {
                        enemyTransform.position = new Vector2(enemyTransform.position.x,
                        enemyTransform.position.y + 0.41f);
                    }
                    if (enemyMoveDirectionX == 1)
                    {
                        enemyTransform.position = new Vector2(enemyTransform.position.x + 0.41f,
                        enemyTransform.position.y);
                    }
                    if (enemyMoveDirectionX == 2)
                    {
                        enemyTransform.position = new Vector2(enemyTransform.position.x,
                        enemyTransform.position.y - 0.41f);
                    }
                    if (enemyMoveDirectionX == 3)
                    {
                        enemyTransform.position = new Vector2(enemyTransform.position.x - 0.41f,
                        enemyTransform.position.y);
                    }
                    attacking = true;
                    preAttack = false;
                    noShieldBlood = true;
                    enemyStamina.enemyActions = true;

                    doingDamage();
                }
            }
            else
            {
                enemyDamagePossible = false;
            }
            //if (!enemy.preAttack && !enemy.recovAttack)
            //if(enemy.)
            // {
            if (enemyAttackCounter <= 0  // && attacking == true/*&& enemy.preAttackCounter <= 0*/
            )
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
                enemyMoving = true;
            }
            //beforeRecov = false;
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
                    //sfxMan.swordsColliding.Play();
                }
                /*Instantiate(swordClash, hitPoint.position, hitPoint.rotation);
                noShieldBlood = true;
               // enemy.preAttack = false;*/
            }
            else
            {
                enemyStamina.enemyActions = false;
                attacking = false;
            }
        }
    }


    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Collision")
        {
            wallBlock = true;
            //enemy.following = false;
            //enemy.enemyMoving = true;
            engaged = false;
            colliderOn = false;
        }
        else if (other.gameObject.tag == "Player")
        {
            engaged = true;
            colliderOn = true;
            //enemy.enemyMoving = false;
            //wallBlock = false;
        }
        else
        {
            //wallBlock = false;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            activateAction = false;
            colliderOn = false;
        }
    }

    public void doingDamage()
    {
        // if (enemyGameObject.tag == "Enemy" && !deathSeven)
        // {
        if (playerStaminaDrain && playerShield.shieldOn && !deathStrike && colliderOn)
        {
            playerStaminaMan.playerCurrentStamina -= 400;
            sfxMan.swordsColliding.Play();
            Instantiate(swordClash, hitPoint.position, hitPoint.rotation);
            enemyStamina.enemyCurrentStamina -= 400;
            return;
        }

        if (enemyAttackCounter > 0f && deathStrike && colliderOn && !deathSeven)
        {
            currentDamage = damageToGive - thePS.currentDefense;
            playerHealth.playerCurrentHealth -= currentDamage;
            sfxMan.blood.Play();
            if (enemyGameObject.tag == "Enemy")
            {
                sfxMan.enemyAttack.Play();
            }
            hurtPlayer.waitOnStrikeBlock = 1f;
            enemyStamina.enemyCurrentStamina -= 400;

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
            sfxMan.swordsColliding.Play();
            Instantiate(swordClash, hitPoint.position, hitPoint.rotation);
            enemyStamina.enemyCurrentStamina -= 400;
        }
        else if (!deathSeven)
        {
            hit = false;
            if (enemyGameObject.tag == "Enemy" && attacking)
            {
                sfxMan.enemyAttack.Play();
            }
            enemyStamina.enemyCurrentStamina -= 400;
        }
        // }
        // else if (enemyGameObject.tag == "BasicRangedEnemy" && rangedAttack.timeUntilAttack <= 0)
        // {
        //     // rangedDmg++;
        //     // if (playerStaminaDrain && playerShield.shieldOn && !deathStrike && rangedDmg >= 2)
        //     // {
        //     //     playerStaminaMan.playerCurrentStamina -= 400;
        //     //     sfxMan.swordsColliding.Play();
        //     //     Instantiate(swordClash, hitPoint.position, hitPoint.rotation);
        //     //     enemyStamina.enemyCurrentStamina -= 400;
        //     //     return;
        //     // }
        //     // else if (rangedDmg >= 2)
        //     // {
        //     //     playerHealth.playerCurrentHealth -= currentDamage;
        //     //     Instantiate(bloodBurst, hitPoint.position, hitPoint.rotation);
        //     //     sfxMan.blood.Play();
        //     //     // var clone = (GameObject)Instantiate(currentDamage, hitPoint.position,
        //     //     //     Quaternion.Euler(Vector3.zero));
        //     //     rangedDmg = 0;
        //     //}
        // }
    }

    public void doingDamage(int currentDamage, GameObject knifeInstance)
    {
        rangedDmg++;
        if (!knifeInstance.GetComponent<RangedDamage>().rangedDeathStrike && rangedDmg >= 2)
        {
            playerStaminaMan.playerCurrentStamina -= 400;
            sfxMan.swordsColliding.Play();
            Instantiate(swordClash, hitPoint.position, hitPoint.rotation);
            enemyStamina.enemyCurrentStamina -= 400;
            rangedDmg = 0;
            return;
        }
        else if (rangedDmg >= 2 && knifeInstance.GetComponent<RangedDamage>().rangedDeathStrike)
        {
            playerHealth.playerCurrentHealth -= currentDamage;
            Instantiate(bloodBurst, hitPoint.position, hitPoint.rotation);
            sfxMan.blood.Play();
            rangedDmg = 0;
        }
    }

    public void enemyTestScriptVariables(bool newLocalAttackLock)
    {
        localAttackLock = newLocalAttackLock;
    }
}