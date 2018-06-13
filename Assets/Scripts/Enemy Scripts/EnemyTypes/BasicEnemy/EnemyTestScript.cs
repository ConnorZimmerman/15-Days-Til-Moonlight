using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTestScript : MonoBehaviour //Welcome to the most complex script out of all 34 scripts
{
    private EnemyHealthManager enemyHealthMan;
    // private EnemyStaminaManager enemyStaminaMan;
    private PlayerHealthManager playerHealthMan;
    private PlayerStaminaManager playerStaminaMan;

    public PlayerController thePlayer;
    private HurtPlayerUpdated hurtPlayer;
    private PlayerHealthManager playerHealth;
    private EngagedWithPlayer playerEngagement;
    //    private RandomMovement randomMove;
    public bool enemyShieldStrike;
    public float speed;
    private Rigidbody2D myRigidbody;
    public Vector2 lastMove; /*lastMove stores whichever way the character was facing last so that
    when there is no input the image does not go to a default position*/

    public GameObject target; //The player is the target (used to follow player)

    private static bool enemyExists;

    private float playerTrackX; //These two bad boys are the players coordinates
    private float playerTrackY;

    public float enemyTrackX;
    public float enemyTrackY;


    public float trackingMasterX;
    public float trackingMasterY; /*These badder boys are the difference in coordinates between player
    and enemy*/

    public int moveDirectionX; //Dictates which image is displayed based on which way enemy is facing
    public int moveDirectionY;

    private Animator anim;

    public bool enemyMoving;
    public bool following; //Following player

    public BoxCollider2D enemyArea; /*This is currently ineffective as it covers the whole map, but
    it's the box that that triggers when the enemy is aggroed*/

    private int actionControl; /*This int is assigned based on where the player is relative to the
    enemy and it dictates to the #SwitchCases where the actions are carried out so that the enemy 
    knows which way its facing while carrying out its action*/

    public float actionTimer; /*This timer manipulates the enemies #ActionDecisions so that every
    time it hits 0 a new action is given based on the enemies priorities*/

    private int actionDecision; /*The #ActionDecisions are based on the enemies priorities and tell 
    the enemy what to do... Pretty much the enemies brain*/

    /*The AI Priorities: there were originally only 4 that were in order of importance, but more were
    added as I kept working so they're not in order of importance anymore... I'll eventually reorganize
    them once I don't think I'll be adding more*/
    // public bool enemyHealthOne;
    // public bool playerHealthTwo;
    // public bool enemyStaminaThree;
    // public bool playerStaminaFour;
    // public bool dodgingFive;
    // public bool patienceSix;
    public bool deathSeven;

    //New experimental (simplified) priorities
    public bool playerHealthOne;
    public bool shieldUpTwo;
    public bool retreatingThree;
    public bool patienceFour;
    public bool enemyShield;
    public GameObject shieldTell; //Shield icon over the enemies head when enemy blocks
    public Transform Fred;
    public float tellCounter; //How long the red Stamina icon stays over the enemies head

    private bool attackLock; //Dictates whether or not the enemy is allowed to attack
    public bool staminaLockBool; //Cripples the enemy if there stamina hits 0
    public float staminaLock; //Counter for how long the enemy is cripped

    private bool enemyRecover; /*Whether or now the enemy is trying to regain its stamina (effects its
    priorities)*/

    public float dodgeCounter; //Puts a timer on how often the enemy can dodge

    private int attackWhileBlocking; /*A variable that is randomly generated to decide if the enemy will
    attack while its blocking*/
    private bool attackWhileBlockingBool;

    /*The enemy's (and player's attack) is currently set up to be 3 different blend trees. The following
     4 variables dictate which stage the enemy is in of its attack based on timers and bools
    public bool preAttack; 
    public float preAttackCounter;

    public bool recovAttack;
    public float recovAttackCounter;*/

    //Both of variables show whether or not the enemy is hurt to trigger blood and damage
    public float enemyHurtCounter;
    public bool inPain;

    public bool dodging;

    public bool dodgeFirstTime; /*Can't quite remember the point of this... but, I remember it's 
    important*/

    private ShieldBlock playerShield;

    private RecognizeStalkZone stalkZone; /*An important scipt that tells the enemy when it should
    stop moving at the player to attack the player*/

    /*These are both random variables that tell the enemy whether it should jump away or back away
     while in dodge priority*/
    public float dashOrBack;
    private bool dashOrBackActive;

    bool dodgeOnlyOnceBool;

    public GameObject playerObject;

    public bool correctSideForDeathStrikeBool;

    GameObject enemyObject;
    public GameObject engageWithPlayerObject;

    public GameObject stalkZoneObject;
    private int walkDirection;
    private TrackingRaycast raycastPath;
    private Vector2 enemyPos;
    public bool isPathfinding;
    // initiates when player enters enemies zone
    private bool followToDeath;
    public float shieldBreakRecoveryCounter;

    // Use this for initialization
    void Start()
    {
        this.gameObject.name = "Fred";
        enemyObject = this.gameObject;
        engageWithPlayerObject = this.gameObject.transform.GetChild(0).gameObject;
        enemyHealthMan = enemyObject.GetComponent<EnemyHealthManager>();
        // enemyStaminaMan = enemyObject.GetComponent<EnemyStaminaManager>();
        playerEngagement = engageWithPlayerObject.GetComponent<EngagedWithPlayer>();
        stalkZoneObject = this.gameObject.transform.GetChild(7).gameObject;
        stalkZone = stalkZoneObject.GetComponent<RecognizeStalkZone>();

        raycastPath = enemyObject.GetComponent<TrackingRaycast>();

        playerHealthMan = FindObjectOfType<PlayerHealthManager>();
        playerStaminaMan = FindObjectOfType<PlayerStaminaManager>();

        playerHealth = FindObjectOfType<PlayerHealthManager>();

        hurtPlayer = FindObjectOfType<HurtPlayerUpdated>();

        playerObject = GameObject.Find("Player");
        thePlayer = playerObject.GetComponent<PlayerController>();
        target = playerObject;

        enemyShieldStrike = false;
        shieldBreakRecoveryCounter = 0.5f;

        dodgeCounter = 2.2f;

        tellCounter = 0.5f;
        actionTimer = 0.2f;

        enemyHurtCounter = 2;

        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (!enemyExists)
        {
            enemyExists = true;
        }

        dodging = true;
        dodgeFirstTime = true;

        isPathfinding = false;
        retreatingThree = false;
        shieldUpTwo = false;

        followToDeath = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if (enemyHealthMan.fredIsDead)
        if (enemyHealthMan.fredIsDead && enemyHealthMan.CurrentHealth <= 0)
        {
            deathSeven = true;
            actionDecision = 7;
            myRigidbody.velocity = Vector2.zero;
        }

        if (enemyShieldStrike)
        {
            shieldBreakRecoveryCounter -= Time.deltaTime;
        }

        if (shieldBreakRecoveryCounter <= 0)
        {
            enemyShieldStrike = false;
            shieldBreakRecoveryCounter = 0.5f;
        }

        if (CalculatePlayerDistance() && !playerEngagement.colliderOn && !playerEngagement.wallBlock)
        {
            followToDeath = true;
        }
        else
        {
            followToDeath = false;
        }

        if (followToDeath && !playerHealth.playerIsDead && !enemyHealthMan.fredIsDead)
        {
            FollowingPlayer();
            ChooseDirection();
        }

        if (enemyHealthMan.CurrentHealth < enemyHealthMan.oldCurrentHealth)
        {
            inPain = true;
            enemyHurtCounter -= Time.deltaTime;
        }

        if (enemyHurtCounter <= 0)
        {
            enemyHurtCounter = 2;
            inPain = false;
            enemyHealthMan.oldCurrentHealth = enemyHealthMan.CurrentHealth;
        }

        ChooseAction();

        staminaLockBool = false;

        if (dodgeFirstTime && inPain)
        {
            dodgeCounter -= Time.deltaTime;
        }

        if (dodgeCounter <= 0)
        {
            dodgeCounter = 2.2f;

            //should be right spot?
            dodgeOnlyOnceBool = false;

            dodgeFirstTime = false;
        }
        if (dodgeCounter >= 2f && !inPain)
        {
            dodging = true;
        }
        if (dodgeCounter < 2f)
        {
            dodging = false;
            dodgeCounter -= Time.deltaTime;
        }

        if (staminaLockBool == true) //part of the crippling effect of a stamina lock
        {
            speed = 1;
        }
        else
        {
            speed = 3.5f;
        }

        //These are all the rules for the blend trees to transition
        anim.SetFloat("MoveDirectionX", moveDirectionX);
        anim.SetFloat("MoveDirectionY", moveDirectionY);
        anim.SetBool("Enemy Moving", enemyMoving);
        anim.SetBool("Enemy Following", following);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
        anim.SetBool("Collider On", playerEngagement.colliderOn);
        anim.SetBool("Attacking", playerEngagement.attacking);
        anim.SetInteger("Action Control", actionControl);
        anim.SetFloat("Action Timer", actionTimer);
        anim.SetBool("Player Dead", playerHealth.playerIsDead);
        anim.SetInteger("Action Decision", actionDecision);
        anim.SetBool("Shield On", enemyShield);
        anim.SetBool("Preemptive Attack", playerEngagement.preAttack);
        anim.SetBool("Recovery Attack", playerEngagement.recovAttack);
        anim.SetBool("Fred is dead", enemyHealthMan.fredIsDead);
        anim.SetBool("Damage Possible", playerEngagement.enemyDamagePossible);
        anim.SetBool("Enqueue", raycastPath.enqueue);


        if (enemyShield)
        {
            correctSideForDeathStrikeBool = false;
        }

        /*The colliderOn is triggered by the stalkZone telling the enemy when to stop following and 
         start attacking*/
        if (playerEngagement.colliderOn)
        {
            following = false;
            enemyMoving = false;
        }
        if (actionTimer <= 0)
        {
            playerEngagement.activateAction = false;
        }

        if (enemyShield)
        {
            speed = 2.5f;
            shieldTell.SetActive(true);
        }
        else
        {
            shieldTell.SetActive(false);
        }

        /*I think this code is an extra safe guard telling the enemy that if its not doing anything
        than it shouldn't be moving*/
        if (!playerEngagement.attacking && !following && !playerEngagement.colliderOn && !enemyMoving)
        {
            myRigidbody.velocity = Vector2.zero;
        }

        if (following)
        {
            enemyMoving = true;
        }
        else
        {
            // enemyMoving = false;
        }

        /*#SwitchCases: what the enemy is doing (each case is the same [except for the direction the
        enemy is facing], but I'll explain what each actionDecision does in the 1st case)
        
        Some of these are not even ever called upon in the current setup, but I'm keeping them around
         cuz I know I'll eventually want more actionDecisions*/
        switch (actionControl)
        {
            case 0:
                if (actionDecision == 0) //Enemy is attacking
                {
                    enemyRecover = false;
                    myRigidbody.velocity = new Vector2(0, 0);
                    enemyShield = false;
                    attackLock = false;
                }

                // if (actionDecision == 1 && playerEngagement.colliderOn)//is not called upon currently
                // {
                //     myRigidbody.velocity = new Vector2(0, 0);
                //     Shield();
                //     enemyRecover = false;
                //     attackLock = true;
                // }

                /*Enemies health is its priority and it's shield is up, but it stays close to the player
                looking for an opportunity to strike*/
                if (actionDecision == 2)
                {
                    enemyRecover = false;
                    myRigidbody.velocity = new Vector2(0, 0);
                    Shield();
                    attackLock = true;
                }

                //The enemies shield is up as it tries to recover stamina
                // if (actionDecision == 3)
                // {
                //     Shield();
                //     // myRigidbody.velocity = new Vector2(0, 0);
                //     enemyRecover = true;
                //     //enemyShield = false;
                //     attackLock = true;
                //     // enemyMoving = false;
                // }

                /*The enemy is trying to put distance between itself and the player (either just being
                hurt or trying to regain stamina*/
                if (actionDecision == 4)
                {
                    Shield();
                    myRigidbody.velocity = new Vector2(-3, 0);
                    enemyRecover = true;
                    attackLock = true;
                    following = false;
                }

                /*The enemy is leaping away (currently pretty broken as there needs to be a timer on how
                long the enemy is leaping away for*/
                //if (actionDecision == 5)
                // if (actionDecision == 5 && !dodgeOnlyOnceBool)
                // {
                //     myRigidbody.velocity = new Vector2(-75, 0);
                //     enemyShield = false;
                //     dodgeCounter -= Time.deltaTime;
                //     enemyRecover = true;

                //     dodgeOnlyOnceBool = true;
                // }

                break;

            case 1:
                if (actionDecision == 0)
                {
                    myRigidbody.velocity = new Vector2(0, 0);
                    enemyRecover = false;
                    enemyShield = false;
                    attackLock = false;
                }

                // if (actionDecision == 1 && playerEngagement.colliderOn)
                // {
                //     myRigidbody.velocity = new Vector2(0, 0);
                //     Shield();
                //     enemyRecover = false;
                //     attackLock = true;
                // }

                if (actionDecision == 2)
                {
                    enemyRecover = false;
                    myRigidbody.velocity = new Vector2(0, 0);
                    Shield();
                    attackLock = true;
                }
                // if (actionDecision == 3)
                // {
                //     Shield();
                //     // myRigidbody.velocity = new Vector2(0, 0);
                //     enemyRecover = true;
                //     //enemyShield = false;
                //     attackLock = true;
                //     // enemyMoving = false;
                // }

                if (actionDecision == 4)
                {
                    Shield();
                    myRigidbody.velocity = new Vector2(3, 0);
                    attackLock = true;
                    enemyRecover = true;
                    following = false;
                }

                // if (actionDecision == 5 && !dodgeOnlyOnceBool)
                // {
                //     attackLock = true;
                //     enemyRecover = true;
                //     myRigidbody.velocity = new Vector2(75, 0);
                //     enemyShield = false;
                //     dodgeCounter -= Time.deltaTime;

                //     dodgeOnlyOnceBool = true;
                // }

                break;

            case 2:
                if (actionDecision == 0)
                {
                    myRigidbody.velocity = new Vector2(0, 0);
                    enemyShield = false;
                    attackLock = false;
                    enemyRecover = false;
                }

                if (actionDecision == 1 && playerEngagement.colliderOn)
                {
                    myRigidbody.velocity = new Vector2(0, 0);
                    Shield();
                    enemyRecover = true;
                    attackLock = true;
                }

                if (actionDecision == 2)
                {
                    myRigidbody.velocity = new Vector2(0, 0);
                    enemyRecover = false;
                    Shield();
                    following = false;
                    attackLock = true;
                }

                // if (actionDecision == 3)
                // {
                //     Shield();
                //     // myRigidbody.velocity = new Vector2(0, 0);
                //     enemyRecover = true;
                //     // enemyShield = false;
                //     attackLock = true;
                //     // enemyMoving = false;
                // }

                if (actionDecision == 4)
                {
                    Shield();
                    myRigidbody.velocity = new Vector2(0, 3);
                    enemyRecover = true;
                    attackLock = true;
                    following = false;
                }

                // if (actionDecision == 5 && !dodgeOnlyOnceBool)
                // {
                //     enemyRecover = true;
                //     myRigidbody.velocity = new Vector2(0, 75);
                //     enemyShield = false;
                //     dodgeCounter -= Time.deltaTime;

                //     dodgeOnlyOnceBool = true;
                // }
                break;

            case 3:
                if (actionDecision == 0)
                {
                    enemyRecover = false;
                    myRigidbody.velocity = new Vector2(0, 0);
                    enemyShield = false;
                    attackLock = false;
                }

                // if (actionDecision == 1 && playerEngagement.colliderOn)
                // {
                //     myRigidbody.velocity = new Vector2(0, 0);
                //     enemyRecover = false;
                //     Shield();
                //     attackLock = true;
                // }

                if (actionDecision == 2)
                {
                    enemyRecover = false;
                    myRigidbody.velocity = new Vector2(0, 0);
                    Shield();
                    attackLock = true;
                }

                // if (actionDecision == 3)
                // {
                //     // myRigidbody.velocity = new Vector2(0, 0);
                //     Shield();
                //     enemyRecover = true;
                //     attackLock = true;
                //     // enemyMoving = false;
                // }


                if (actionDecision == 4)
                {
                    myRigidbody.velocity = new Vector2(0, -3);
                    enemyRecover = true;
                    attackLock = true;
                    Shield();
                    following = false;
                }

                // if (actionDecision == 5 && !dodgeOnlyOnceBool)
                // {
                //     enemyRecover = true;
                //     myRigidbody.velocity = new Vector2(0, -75);
                //     dodgeCounter -= Time.deltaTime;

                //     dodgeOnlyOnceBool = true;
                // }
                break;
        }



        if (playerHealth.playerIsDead)
        {
            playerEngagement.engaged = false;
            following = false;
            myRigidbody.velocity = new Vector2(0, 0);
        }
    }

    public void ChooseAction() /*#ActionDecisions: this is where the action decisions are made based
        on what it retrieves from the ActionPriorities() function below*/
    {
        if (deathSeven)
        {
            actionDecision = 7;
            return;
        }
        ActionPriorities();

        if (playerHealthOne && !deathSeven)
        {
            // dashOrBack = UnityEngine.Random.Range(-1, 3);
            // if (dashOrBack == 2)
            // {
            // actionDecision = 2;
            // }
            // else
            // {
            actionDecision = 0;
            // }
        }
        else if (retreatingThree && !deathSeven)
        {
            actionDecision = 4;
        }
        else if (shieldUpTwo && !deathSeven)
        {
            actionDecision = 4;
        }

        else if (playerHealthOne && !deathSeven)
        {
            actionDecision = 0;
        }

        else if (shieldUpTwo && !deathSeven)
        {
            actionDecision = 2;
        }

        if (patienceFour && !deathSeven)
        {
            if (dashOrBackActive)
            {
                dashOrBack = UnityEngine.Random.Range(-1, 3);
            }

            dashOrBackActive = false;

            if (dashOrBack == -1 || dashOrBack == 0 || dashOrBack == 1)
            {
                actionDecision = 4;
            }
            else if (dashOrBack == 2)
            {
                actionDecision = 4;
            }
        }

        else
        {
            dashOrBackActive = true;
        }


        if (patienceFour && !deathSeven)
        {

            if (attackWhileBlockingBool)
            {
                attackWhileBlocking = UnityEngine.Random.Range(0, 2);
            }

            if (attackWhileBlocking == 0)
            {
                actionDecision = 0;
            }

            if (attackWhileBlocking == 1 && !attackLock)
            {
                actionDecision = 3;
            }

            attackWhileBlockingBool = false;

        }
        else
        {
            attackWhileBlockingBool = true;
        }

    }
    //#PickUp
    public void ActionPriorities() /*#ActionDecisions: this is the core of the enemies action decisions,
        basing each of its priorities on whether or not the variables line up.*/
    {
        /*if (thePlayer.preAttack)
        {
            dodgingFive = true;
        }*/
        if (stalkZone.stalkZoneOn && !playerHealthOne && !playerEngagement.deathStrike
            && !shieldUpTwo)
        {
            patienceFour = true;
        }
        else
        {
            patienceFour = false;
        }

        // if (dodging && inPain
        //  && playerStaminaMan.playerStaminaPercent >= 25 && !thePlayer.damagePossible)
        // {
        //     dodgingFive = true;
        // }
        // else
        // {
        //     dodgingFive = false;
        // }
        if (playerStaminaMan.playerStaminaPercent >= 25 && inPain || playerStaminaMan.playerStaminaPercent >= 75 && enemyShieldStrike)
        {
            retreatingThree = true;
        }
        else
        {
            retreatingThree = false;
        }

        if (playerStaminaMan.playerStaminaPercent >= 25 && !playerHealthOne && !retreatingThree
        || inPain && playerStaminaMan.playerStaminaPercent >= 25)
        {
            shieldUpTwo = true;
        }
        else
        {
            shieldUpTwo = false;
        }

        if (!patienceFour && !retreatingThree
                || playerStaminaMan.staminaLock || playerStaminaMan.playerStaminaPercent <= 25)
        {
            playerHealthOne = true;
        }
        else
        {
            playerHealthOne = false;
        }

        // if (!patienceSix && !enemyHealthOne && !playerHealthTwo
        //      && !dodgingFive
        //    && playerStaminaMan.playerStaminaPercent >= 25
        //    || !playerStaminaMan.staminaLock)
        // {
        //     enemyStaminaThree = true;
        // }
        // else
        // {
        //     enemyStaminaThree = false;
        // }

        // if (!patienceSix && !enemyHealthOne && !playerHealthTwo && !enemyStaminaThree
        //     && !dodgingFive && playerStaminaMan.playerStaminaPercent >= 25)
        // {
        //     playerStaminaFour = true;
        // }
        // else
        // {
        //     playerStaminaFour = false;
        // }
    }

    // public void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.name == "Player" && !playerEngagement.colliderOn && !playerEngagement.wallBlock)
    //     {
    //         if (!deathSeven) // && enemyMoving)
    //         {
    //             followToDeath = true;
    //         }
    //     }
    //     else
    //     {
    //         //randomMove.ChooseDirection();
    //     }
    // }

    // public void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.name == "Player")
    //     {
    //         following = false;
    //         enemyMoving = false;
    //     }
    // }

    public void Shield()
    {
        if (!enemyShieldStrike)
        {
            enemyShield = true;
        }
        else
        {
            enemyShield = false;
        }
    }

    public void engagedWithPlayerPrivateVariables(bool localAttackLock)
    {
        localAttackLock = attackLock;
        playerEngagement.enemyTestScriptVariables(localAttackLock);
    }

    public void FollowingPlayer()
    {
        following = true;
        if (raycastPath.lineOfSight && !playerEngagement.colliderOn)
        {
            raycastPath.enqueue = true;
            // if (!shieldUpTwo)
            // {
            transform.position = Vector2.MoveTowards(transform.position,
            playerObject.transform.position, speed * Time.deltaTime);
            // }
        }
        else
        {
            if (raycastPath.path != null)
            {
                foreach (Vector2 n in raycastPath.path)
                {
                    enemyPos = enemyObject.transform.position;
                    if (enemyPos != n)
                    {
                        transform.position = Vector2.MoveTowards(transform.position,
                        n, (speed) * Time.deltaTime);
                    }
                    if (!raycastPath.lineOfSight)
                    {
                        raycastPath.enqueue = false;
                        break;
                    }
                }
            }
            else
            {
                enemyMoving = false;
                following = false;
            }
        }
    }

    public void ChooseDirection()
    {
        playerTrackX = playerObject.transform.position.x;
        playerTrackY = playerObject.transform.position.y;

        trackingMasterX = playerTrackX - transform.position.x;
        trackingMasterY = playerTrackY - transform.position.y;

        enemyTrackX = transform.position.x;
        enemyTrackY = transform.position.y;

        if (trackingMasterY > 0)
        {
            if (trackingMasterX > 0) //Quadrant 2
            {
                if (enemyShield && moveDirectionX == 0)
                {
                    correctSideForDeathStrikeBool = false;
                }
                else
                {
                    correctSideForDeathStrikeBool = true;
                }

                if (trackingMasterX < trackingMasterY)
                {
                    lastMove.x = 0;
                    lastMove.y = 0;
                    moveDirectionX = 0;
                    moveDirectionY = 0;
                    actionControl = 1;
                }
                else
                {
                    lastMove.x = 1;
                    lastMove.y = 1;
                    moveDirectionX = 1;
                    moveDirectionY = 1;
                    actionControl = 2;
                }
            }
            else if (trackingMasterX < 0)
            {
                if (Math.Abs(trackingMasterX) > trackingMasterY) //Quadrant 1
                {
                    lastMove.x = 3;
                    lastMove.y = 3;
                    moveDirectionX = 3;
                    moveDirectionY = 3;
                    actionControl = 1;
                }
                else
                {
                    lastMove.x = 0;
                    lastMove.y = 0;
                    moveDirectionX = 0;
                    moveDirectionY = 0;
                    actionControl = 3;
                }
            }
        }
        else if (trackingMasterY < 0) //Quadrant 4
        {
            if (trackingMasterX < 0)
            {
                if (trackingMasterX > trackingMasterY)
                {
                    lastMove.x = 2;
                    lastMove.y = 2;
                    moveDirectionX = 2;
                    moveDirectionY = 2;
                    actionControl = 0;
                }
                else if (trackingMasterX < trackingMasterY)
                {
                    lastMove.x = 3;
                    lastMove.y = 3;
                    moveDirectionX = 3;
                    moveDirectionY = 3;
                    actionControl = 3;
                }
            }
            else if (trackingMasterX > 0) //Quadrant 3
            {
                if (Math.Abs(trackingMasterY) > trackingMasterX)
                {
                    lastMove.x = 2;
                    lastMove.y = 2;
                    moveDirectionX = 2;
                    moveDirectionY = 2;
                    actionControl = 2;
                }
                else
                {
                    lastMove.x = 1;
                    lastMove.y = 1;
                    moveDirectionX = 1;
                    moveDirectionY = 1;
                    actionControl = 0;
                }
            }
        }
    }

    public bool CalculatePlayerDistance()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);

        if (distanceToPlayer <= 13)
        {
            return true;
        }

        return false;
    }
}



