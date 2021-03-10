using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRangedEnemy : MonoBehaviour
{
    private EnemyRangedAttack rangedAttack;
    private EnemyHealthManager enemyHealthMan;
    private PlayerHealthManager playerHealthMan;
    private PlayerStaminaManager playerStaminaMan;
    public PlayerController thePlayer;
    private HurtPlayerUpdated hurtPlayer;
    private PlayerHealthManager playerHealth;
    private EngagedWithPlayer playerEngagement;
    private RandomMovement randomMove;
    public float speed;
    private Rigidbody2D myRigidbody;
    // lastMove stores whichever way the character was facing last so that when there is no input the image does not go to a default position
    public Vector2 lastMove;
    // The player is the target (used to follow player)
    public GameObject target;
    private static bool enemyExists;
    // These two bad boys are the players coordinates
    private float playerTrackX;
    private float playerTrackY;
    public float enemyTrackX;
    public float enemyTrackY;
    public float trackingMasterX;
    // These badder boys are the difference in coordinates between player and enemy
    public float trackingMasterY;
    // Dictates which image is displayed based on which way enemy is facing
    public int moveDirectionX;
    public int moveDirectionY;
    private Animator anim;
    public bool enemyMoving;
    // Following player
    public bool following;
    // This is currently ineffective as it covers the whole map, but it's the box that that triggers when the enemy is aggroed
    public BoxCollider2D enemyArea;
    // This int is assigned based on where the player is relative to the enemy and it dictates to the #SwitchCases where the actions are carried out so that the enemy knows which way its facing while carrying out its action
    private int actionControl;
    // This timer manipulates the enemies #ActionDecisions so that every time it hits 0 a new action is given based on the enemies priorities
    public float actionTimer;
    // The #ActionDecisions are based on the enemies priorities and tell the enemy what to do... Pretty much the enemies brain
    public int actionDecision;
    // The AI Priorities: there were originally only 4 that were in order of importance, but more were added as I kept working so they're not in order of importance anymore... I'll eventually reorganize them once I don't think I'll be adding more
    public bool isAttackingOne;
    public bool fleeTwo;
    public bool dodgingThree;
    public bool snipeSpotFour;
    public bool inRangeFive;
    public bool deathSeven;
    public bool enemyShield;
    // Shield icon over the enemies head when enemy blocks
    public GameObject shieldTell;
    public Transform Thomas;
    // How long the red Stamina icon stays over the enemies head
    public float tellCounter;
    // Dictates whether or not the enemy is allowed to attack
    private bool attackLock;
    // Cripples the enemy if there stamina hits 0
    public bool staminaLockBool;
    // Counter for how long the enemy is cripped
    public float staminaLock;
    //Whether or now the enemy is trying to regain its stamina (effects its priorities)
    private bool enemyRecover;
    // Puts a timer on how often the enemy can dodge
    public float dodgeCounter;
    // A variable that is randomly generated to decide if the enemy will attack while its blocking
    private int attackWhileBlocking;
    private bool attackWhileBlockingBool;
    // Both of variables show whether or not the enemy is hurt to trigger blood and damage
    public float enemyHurtCounter;
    public bool inPain;
    private ShieldBlock playerShield;
    // An important scipt that tells the enemy when it should stop moving at the player to attack the player
    private RecognizeStalkZone stalkZone;
    // These are both random variables that tell the enemy whether it should jump away or back away while in dodge priority
    public float dashOrBack;
    private bool dashOrBackActive;
    bool dodgeOnlyOnceBool;
    public GameObject playerObject;
    public bool correctSideForDeathStrikeBool;
    GameObject enemyObject;
    public GameObject engageWithPlayerObject;
    public GameObject stalkZoneObject;
    private int walkDirection;
    private Vector2 enemyPos;
    private Vector2 playerPos;
    bool followToDeath;

    // Use this for initialization
    void Start()
    {
        this.gameObject.name = "Thomas";
        enemyObject = this.gameObject;
        engageWithPlayerObject = this.gameObject.transform.GetChild(0).gameObject;
        enemyHealthMan = enemyObject.GetComponent<EnemyHealthManager>();
        playerEngagement = engageWithPlayerObject.GetComponent<EngagedWithPlayer>();
        stalkZoneObject = this.gameObject.transform.GetChild(7).gameObject;
        stalkZone = stalkZoneObject.GetComponent<RecognizeStalkZone>();

        rangedAttack = enemyObject.GetComponent<EnemyRangedAttack>();

        playerHealthMan = FindObjectOfType<PlayerHealthManager>();
        playerStaminaMan = FindObjectOfType<PlayerStaminaManager>();

        playerHealth = FindObjectOfType<PlayerHealthManager>();

        hurtPlayer = FindObjectOfType<HurtPlayerUpdated>();
        randomMove = FindObjectOfType<RandomMovement>();

        playerObject = GameObject.Find("Player");
        thePlayer = playerObject.GetComponent<PlayerController>();
        target = playerObject;

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

        //For now enemy will always have a correct line of sight and the fleeing system is not yet set up.
        fleeTwo = false;
        dodgingThree = false;

        followToDeath = false;
    }

    // Update is called once per frame
    void Update()
    {
        inRangeFive = rangedAttack.inRange;
        snipeSpotFour = !rangedAttack.lineOfSight;

        if (snipeSpotFour)
        {
            isAttackingOne = false;
        }
        if (enemyHealthMan.fredIsDead && enemyHealthMan.CurrentHealth <= 0)
        {
            deathSeven = true;
            actionDecision = 7;
            myRigidbody.velocity = Vector2.zero;
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

        if (CalculatePlayerDistance() && !playerEngagement.colliderOn && !playerEngagement.wallBlock)
        {
            followToDeath = true;
        }
        else
        {
            followToDeath = false;
        }

        if (followToDeath)
        {
            FollowingPlayer();
            ChooseDirection();
        }

        ChooseAction();

        if (staminaLock <= 0)
        {
            staminaLock = 2;
        }

        // Part of the crippling effect of a stamina lock
        if (staminaLockBool)
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
        anim.SetInteger("Action Decision", actionDecision);
        anim.SetBool("Fred is dead", enemyHealthMan.fredIsDead);
        anim.SetBool("Enqueue", rangedAttack.enqueue);

        if (enemyShield)
        {
            correctSideForDeathStrikeBool = false;
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

        // I think this code is an extra safe guard telling the enemy that if its not doing anything than it shouldn't be moving
        if (!playerEngagement.attacking && !following && !playerEngagement.colliderOn && !enemyMoving)
        {
            myRigidbody.velocity = Vector2.zero;
        }

        switch (actionControl)
        {
            case 0:
                // Enemy is attacking
                if (actionDecision == 0)
                {
                    myRigidbody.velocity = new Vector2(0, 0);
                    enemyShield = false;
                    attackLock = false;
                }

                // Running away from the player if the player is close enough after it throws 
                if (actionDecision == 1)
                {
                    myRigidbody.velocity = new Vector2(0, 0);
                    attackLock = true;
                    enemyMoving = false;
                }

                // Backing away from if it has already thrown and player is close enough
                if (actionDecision == 2)
                {
                    myRigidbody.velocity = new Vector2(-2, 0);
                    attackLock = true;
                }

                // Getting a clear line of sight to throw at player
                if (actionDecision == 3 && !dodgeOnlyOnceBool)
                {
                    myRigidbody.velocity = new Vector2(-75, 0);
                    enemyShield = false;
                    dodgeCounter -= Time.deltaTime;

                    dodgeOnlyOnceBool = true;
                }

                // Getting close enough to player to throw
                if (actionDecision == 4)
                {
                    enemyMoving = true;
                    following = true;

                }
                else
                {
                    enemyMoving = false;
                }

                break;

            case 1:
                if (actionDecision == 0)
                {
                    myRigidbody.velocity = new Vector2(0, 0);
                    enemyShield = false;
                    attackLock = false;
                }

                if (actionDecision == 1)
                {
                    myRigidbody.velocity = new Vector2(0, 0);
                    attackLock = true;
                    enemyMoving = false;
                }

                if (actionDecision == 2)
                {
                    myRigidbody.velocity = new Vector2(2, 0);
                    attackLock = true;
                }

                if (actionDecision == 3 && !dodgeOnlyOnceBool)
                {
                    attackLock = true;
                    myRigidbody.velocity = new Vector2(75, 0);
                    enemyShield = false;
                    dodgeCounter -= Time.deltaTime;
                    dodgeOnlyOnceBool = true;
                }
                if (actionDecision == 4)
                {
                    enemyMoving = true;
                    following = true;

                }
                else
                {
                    enemyMoving = false;
                }

                break;

            case 2:
                if (actionDecision == 0)
                {
                    myRigidbody.velocity = new Vector2(0, 0);
                    enemyShield = false;
                    attackLock = false;
                }

                if (actionDecision == 1)
                {
                    myRigidbody.velocity = new Vector2(0, 0);
                    attackLock = true;
                    enemyMoving = false;
                }

                if (actionDecision == 2)
                {
                    myRigidbody.velocity = new Vector2(0, 2);
                    attackLock = true;
                }

                if (actionDecision == 3 && !dodgeOnlyOnceBool)
                {
                    myRigidbody.velocity = new Vector2(0, 75);
                    enemyShield = false;
                    dodgeCounter -= Time.deltaTime;

                    dodgeOnlyOnceBool = true;
                }
                if (actionDecision == 4)
                {
                    enemyMoving = true;
                    following = true;

                }
                else
                {
                    enemyMoving = false;
                }

                break;

            case 3:
                if (actionDecision == 0)
                {
                    myRigidbody.velocity = new Vector2(0, 0);
                    enemyShield = false;
                    attackLock = false;
                }

                if (actionDecision == 1)
                {
                    myRigidbody.velocity = new Vector2(0, 0);
                    attackLock = true;
                    enemyMoving = false;
                }

                if (actionDecision == 2)
                {
                    myRigidbody.velocity = new Vector2(0, -2);
                    attackLock = true;
                }

                if (actionDecision == 3 && !dodgeOnlyOnceBool)
                {
                    myRigidbody.velocity = new Vector2(0, -75);
                    dodgeCounter -= Time.deltaTime;
                    dodgeOnlyOnceBool = true;
                }

                if (actionDecision == 4)
                {
                    enemyMoving = true;
                    following = true;
                }
                else
                {
                    enemyMoving = false;
                }

                break;
        }

        if (playerHealth.playerIsDead)
        {
            playerEngagement.engaged = false;
            following = false;
            myRigidbody.velocity = new Vector2(0, 0);
        }
    }

    // ActionDecisions: this is where the action decisions are made based on what it retrieves from the ActionPriorities() function below
    // This section is separate from action priorities because there are potentially combinations from action priorities that can lead to different outcomes
    public void ChooseAction()
    {
        if (deathSeven)
        {
            actionDecision = 7;
            return;
        }

        ActionPriorities();

        if (fleeTwo && !deathSeven)
        {
            actionDecision = 1;
            isAttackingOne = false;
        }
        else if (dodgingThree && !deathSeven)
        {
            actionDecision = 2;
            isAttackingOne = false;
        }
        else if (!inRangeFive || snipeSpotFour)
        {
            actionDecision = 4;
            isAttackingOne = false;
        }
        else
        {
            isAttackingOne = true;
            actionDecision = 0;
        }
    }
    public void ActionPriorities()
    /*#ActionDecisions: this is the core of the enemies action decisions,
           basing each of its priorities on whether or not the variables line up.*/
    {

        if (!deathSeven)
        {
            PlayerTooClose();
            if (fleeTwo)
            {
                fleeTwo = true;
                isAttackingOne = false;
                return;
            }
            else if (dodgingThree)
            {
                isAttackingOne = false;
                return;
            }
            else if (!inRangeFive)
            {
                isAttackingOne = false;
            }
            else if (snipeSpotFour)
            {
                isAttackingOne = false;
            }

        }
        //Check for clear line of sight here
        // if (!inRangeFive && !snipeSpotFour && !dodgingThree && !fleeTwo && !deathSeven && !isAttackingOne)
        // {
        //     actionDecision = 1;
        // }
    }

    public void engagedWithPlayerPrivateVariables(bool localAttackLock)
    {
        localAttackLock = attackLock;
        playerEngagement.enemyTestScriptVariables(localAttackLock);
    }

    public void PlayerTooClose()
    {
        if (rangedAttack.distanceToPlayer < 7 && !snipeSpotFour)
        {
            dodgingThree = true;
        }
        else
        {
            dodgingThree = false;
        }
    }

    public void FollowingPlayer()
    {
        following = true;

        if (following)
        {
            if (!deathSeven && enemyMoving)
            {
                if (!snipeSpotFour)
                {
                    rangedAttack.enqueue = false;
                    transform.position = Vector2.MoveTowards(transform.position,
                        playerObject.transform.position, speed * Time.deltaTime);
                }
                else
                {
                    if (rangedAttack.path != null && !rangedAttack.enqueue)
                    {
                        //enemyMoving = true;
                        foreach (Vector2 n in rangedAttack.path)
                        {
                            enemyPos = enemyObject.transform.position;
                            if (enemyPos != n)
                            {
                                transform.position = Vector2.MoveTowards(transform.position,
                                    n, (speed - 2f) * Time.deltaTime);
                            }
                            if (!snipeSpotFour)
                            {
                                rangedAttack.enqueue = false;
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
            // Quadrant 2
            if (trackingMasterX > 0)
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
                // Quadrant 1
                if (Math.Abs(trackingMasterX) > trackingMasterY)
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
        // Quadrant 4
        else if (trackingMasterY < 0)
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
            // Quadrant 3
            else if (trackingMasterX > 0)
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

        if (distanceToPlayer <= 15)
        {
            return true;
        }

        return false;
    }
}