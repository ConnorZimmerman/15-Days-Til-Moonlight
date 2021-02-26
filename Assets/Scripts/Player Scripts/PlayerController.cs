using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private PlayerStaminaManager staminaMan;
    private Animator anim;
    public Rigidbody2D myRigidbody;
    public bool playerMoving;
    public Vector2 lastMove;
    private Vector2 moveInput;
    private static bool playerExists;
    public bool damagePossible;
    public static bool attacking;
    public bool attackBool;
    public bool attackBoolMouse;
    public bool attackPossible;
    public bool preAttack;
    public bool recovAttack;
    public float preAttackCounter;
    public float attackTimeCounter;
    public float recovAttackCounter;
    public float attackTime;
    public string startPoint;
    private bool attackLock;
    private float axisHorizontal;
    private float axisVertical;
    public float axisInput;
    public GameObject swingBig;
    public bool canMove;
    private DialogueManager theDM;
    private bool directionRight;
    private bool directionLeft;
    private bool directionUp;
    private bool directionDown;
    public int directionInt;
    public BoxCollider2D boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;
    private SFXManager sfxMan;
    public bool dashActive;
    public bool dashPossible;
    public bool sprintActive;
    public bool sprintLock;
    public bool currentEnemyExists;
    private EnemyTestScript enemy;
    public bool damageBlock;
    public ShieldBlock playerShield;
    public GameObject shieldTell;
    public bool lockOn;
    public float lockOnHorizontal;
    public float lockOnVertical;
    public bool deathStrike;
    public int check;
    public int enemyInt;
    public int playerInt;
    private int playerNewHealth;
    private int playerOldHealth;
    private PlayerHealthManager playerHealth;
    public bool playerHurt;
    private HurtPlayerUpdated hurtPlayer;
    private HurtEnemy hurtEnemy;
    public float attackingCounterNew;
    public bool strikeBlock;
    public float strikeBlockCounter;
    public bool noDamageIsTaken;
    public GameObject playerBoundBoxObject;
    public GameObject enemyTargetObject;
    public GameObject dmObject;
    public bool specialMove;
    public bool wasSprint;
    public bool wasMoving;
    //public RaycastHit rayCastHitDodge;
    public RaycastHit2D rayCastHitDodge;
    public int[] rayCastDodgeArray;
    public Ray rayDodge;
    private int rayDodgeDistance;
    public GameObject rayCastHitObject;
    public string rayCastHitObjectString;
    private LayerMask layerMaskPlayer;
    private int layerMaskPlayerInt;
    private LayerMask layerMaskBounds;
    private int layerMaskBoundsInt;
    private LayerMask layerMaskEnemy;
    private int layerMaskEnemyInt;
    GameObject playerObject;
    public Transform playerTransform;
    private GameObject dummyGameObject;
    //doesn't work with this kind of collider gives incorrect collision location
    //public float dodgeDistanceFloat;
    public Vector2 wallDistanceVector;
    public float dodgeDistance;
    public float chargeDistantFloat;
    private EnemyDialogue enemyD;
    public bool soFast;
    public float sprintTimer;
    public bool sprintPossible;
    private PlayerStats playerStats;
    private TerrainManager terrainManager;
    private GlobalDataScript globalData;
    private EquipmentBuffManager equipmentBuffManagerScript;
    public float movementVertical;
    public float movementHorizontal;
    public bool movementDisabilityBool;
    public List<Transform> enemyList;
    public List<Transform> enemyListLeft;
    public List<Transform> enemyListUp;
    public List<Transform> enemyListRight;
    public List<Transform> enemyListDown;
    public Dictionary<int, bool> enemyDict;
    public int enemyCount;

    // This int will count every time a new enemy is locked to and reset once list is reset.
    public int enemyCounter;
    private bool newListBool;
    private int curEnemyInt;
    private bool switchEnemyBool;
    public Transform currentEnemyLocked;
    public GameObject damageBurst;
    private GameObject lockOnImage;

    // Use this for initialization
    void Start()
    {
        // dodgeDistanceFloat = 3;
        enemyD = FindObjectOfType<EnemyDialogue>();

        dummyGameObject = GameObject.Find("Dummy Object");
        //rayCastHitDodge.size;
        rayCastDodgeArray = new int[100];

        playerObject = GameObject.Find("Player");
        playerTransform = playerObject.transform;

        layerMaskPlayerInt = 1 << 9;
        layerMaskPlayer = ~layerMaskPlayerInt;
        layerMaskBoundsInt = 1 << 10;
        layerMaskBounds = ~layerMaskBoundsInt;
        layerMaskEnemyInt = 1 << 11;
        layerMaskEnemy = ~layerMaskEnemyInt;

        rayDodgeDistance = 4;

        playerStats = FindObjectOfType<PlayerStats>();

        playerShield = FindObjectOfType<ShieldBlock>();

        sfxMan = FindObjectOfType<SFXManager>();

        theDM = FindObjectOfType<DialogueManager>();

        staminaMan = GetComponent<PlayerStaminaManager>();

        enemy = FindObjectOfType<EnemyTestScript>();

        playerHealth = FindObjectOfType<PlayerHealthManager>();

        hurtPlayer = FindObjectOfType<HurtPlayerUpdated>();

        hurtEnemy = FindObjectOfType<HurtEnemy>();

        globalData = FindObjectOfType<GlobalDataScript>();

        playerBoundBoxObject = GameObject.Find("Bounds");

        enemyTargetObject = this.gameObject;
        dmObject = GameObject.Find("Dialogue Manager");
        theDM = dmObject.GetComponent<DialogueManager>();

        terrainManager = FindObjectOfType<TerrainManager>();

        damagePossible = false;
        attackPossible = true;
        dashPossible = true;

        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        if (!playerExists)
        {
            playerExists = true;
            //DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }

        canMove = true;

        lastMove = new Vector2(globalData.globalPlayerLastMoveX, globalData.globalPlayerLastMoveY);

        currentEnemyExists = true;

        if (boundBox == null)
        {
            //boundBox = FindObjectOfType<BoundsScript>().GetComponent<BoxCollider2D>();
            boundBox = playerBoundBoxObject.GetComponent<BoxCollider2D>();
            minBounds = boundBox.bounds.min;
            maxBounds = boundBox.bounds.max;
        }

        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;

        deathStrike = false;

        playerOldHealth = playerNewHealth;

        strikeBlock = false;

        strikeBlockCounter = 1f;

        noDamageIsTaken = true;

        preAttack = false;
        recovAttack = false;

        preAttackCounter = 0.2f;
        recovAttackCounter = 0.3f;
        attackTimeCounter = 10;

        attackingCounterNew = 0.06f;

        specialMove = false;

        wasMoving = false;
        wasSprint = false;
        sprintTimer = 0.2f;
        sprintPossible = false;

        // if (globalData.globalPlayerLockOn == 1)
        // {
        //     lockOn = false;
        // }
        // else
        // {
        //     lockOn = false;
        // }

        lockOn = false;
        newListBool = true;
        switchEnemyBool = true;

        equipmentBuffManagerScript = FindObjectOfType<EquipmentBuffManager>();

        lockOnImage = GameObject.Find("lockOnImage");
        lockOnImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerNewHealth = playerHealth.playerCurrentHealth;

        MovementDisability();
        DeterminePlayerDirection();

        if (recovAttack || preAttack)
        {
            attackBool = false;
        }

        if (strikeBlock)
        {
            strikeBlockCounter -= Time.deltaTime;
        }

        if (strikeBlockCounter <= 0)
        {
            strikeBlock = false;
            strikeBlockCounter = 1f;
        }

        if (strikeBlockCounter == 1)
        {
            noDamageIsTaken = false;
        }

        if (playerOldHealth < playerNewHealth)
        {
            playerHurt = true;
            playerOldHealth = playerNewHealth;
        }


        // lockOn code - should move to its own script
        if (Input.GetButtonUp("LockOn") && lockOn == false)
        {
            lockOn = true;
            newListBool = true;
            // AddEnemiesToLists();
            // FindClosestEnemy();
            LockOn();
        }
        else if (Input.GetButtonUp("LockOn") && lockOn == true)
        {
            lockOn = false;
            lockOnImage.SetActive(false);
        }

        if (currentEnemyLocked == null)
        {
            lockOn = false;
        }
        // Debug.Log("test: " + currentEnemyLocked);

        // allows player to switch between enemies in lock on system
        lockOnHorizontal = Input.GetAxisRaw("LockOnHorizontal");
        lockOnVertical = Input.GetAxisRaw("LockOnVertical");

        if (lockOnHorizontal > 0.2f && switchEnemyBool && lockOn || lockOnHorizontal < -0.2f && switchEnemyBool && lockOn || lockOnVertical > 0.2f && switchEnemyBool && lockOn || lockOnVertical < -0.2f && switchEnemyBool && lockOn)
        {
            string analogDirection = DetermineAnalogQuadrant(lockOnHorizontal, lockOnVertical);
            AddEnemiesToLists();

            if (analogDirection == "up")
            {
                currentEnemyLocked = FindNextClosestEnemy(enemyListUp);
            }
            else if (analogDirection == "right")
            {
                currentEnemyLocked = FindNextClosestEnemy(enemyListRight);
            }
            else if (analogDirection == "down")
            {
                currentEnemyLocked = FindNextClosestEnemy(enemyListDown);
            }
            else if (analogDirection == "left")
            {
                currentEnemyLocked = FindNextClosestEnemy(enemyListLeft);
            }
            else
            {
                Debug.Log("error");
            }

            ChooseLockOnDirection(currentEnemyLocked);
            switchEnemyBool = false;
        }

        // the switchEnemyBool forces FindNextClosestEnemy to only run once ^
        if (lockOnHorizontal < 0.2f && lockOnHorizontal > -0.2f && lockOnVertical < 0.2f && lockOnVertical > -0.2f)
        {
            switchEnemyBool = true;
        }


        if (!enemy.deathSeven)
        {
            check = enemy.moveDirectionX - directionInt;
            enemyInt = enemy.moveDirectionX;
            playerInt = directionInt;
        }

        // Debug.Log(playerInt);

        if (playerShield.shieldOn)
        {
            shieldTell.SetActive(true);
        }
        else
        {
            shieldTell.SetActive(false);
        }

        if (staminaMan.playerCurrentStamina <= 0)
        {
            // moveSpeed = 1f;
            // moveSpeed = 4.5f;
            sprintPossible = false;
            dashActive = false;
            sprintActive = false;
        }

        if (playerShield.shieldOn)
        {
            moveSpeed = 2.5f;
        }
        else if (playerStats.dexterity >= 17 && staminaMan.playerCurrentStamina >= 200 && Input.GetButtonDown("DashX")
            || staminaMan.playerCurrentStamina >= 200 && Input.GetButtonDown("Dash"))
        {
            soFast = true;

            if (directionUp)
            {
                rayCastHitDodge = Physics2D.Raycast(playerTransform.position, transform.up, 2.1f,
            layerMaskPlayer & layerMaskBounds & layerMaskEnemy);

                if (rayCastHitDodge.collider != null)
                {
                    rayCastHitObject = rayCastHitDodge.transform.gameObject;
                    rayCastHitObjectString = rayCastHitObject.name;

                    if (rayCastHitObjectString == "Collision")
                    {
                        chargeDistantFloat = rayCastHitDodge.distance;
                        playerTransform.position = new Vector2(playerTransform.position.x, playerTransform.position.y + chargeDistantFloat);
                        dashActive = true;
                        dashPossible = true;
                        rayCastHitObjectString = "Im Pickle Rick";
                    }
                }
                else if (rayCastHitDodge.collider == null)
                {
                    playerTransform.position = new Vector2(playerTransform.position.x, playerTransform.position.y + 2);
                    dashActive = true;
                    dashPossible = true;
                }
            }

            if (directionRight)
            {
                rayCastHitDodge = Physics2D.Raycast(playerTransform.position, transform.right, 2.1f,
            layerMaskPlayer & layerMaskBounds & layerMaskEnemy);

                if (rayCastHitDodge.collider != null)
                {
                    rayCastHitObject = rayCastHitDodge.transform.gameObject;
                    rayCastHitObjectString = rayCastHitObject.name;

                    if (rayCastHitObjectString == "Collision")
                    {
                        chargeDistantFloat = rayCastHitDodge.distance;
                        playerTransform.position = new Vector2(playerTransform.position.x
                            + chargeDistantFloat, playerTransform.position.y);
                        dashActive = true;
                        dashPossible = true;
                        rayCastHitObjectString = "Im Pickle Rick";
                    }
                }
                else if (rayCastHitDodge.collider == null)
                {
                    playerTransform.position = new Vector2(playerTransform.position.x + 2,
                        playerTransform.position.y);
                    dashActive = true;
                    dashPossible = true;
                }
            }

            if (directionDown)
            {
                rayCastHitDodge = Physics2D.Raycast(playerTransform.position, -transform.up, 2.1f,
            layerMaskPlayer & layerMaskBounds & layerMaskEnemy);

                if (rayCastHitDodge.collider != null)
                {
                    rayCastHitObject = rayCastHitDodge.transform.gameObject;
                    rayCastHitObjectString = rayCastHitObject.name;

                    if (rayCastHitObjectString == "Collision")
                    {
                        chargeDistantFloat = rayCastHitDodge.distance;
                        playerTransform.position = new Vector2(playerTransform.position.x,
                            playerTransform.position.y - chargeDistantFloat);
                        dashActive = true;
                        dashPossible = true;
                        rayCastHitObjectString = "Im Pickle Rick";
                    }
                }
                else if (rayCastHitDodge.collider == null)
                {
                    playerTransform.position = new Vector2(playerTransform.position.x,
                        playerTransform.position.y - 2);
                    dashActive = true;
                    dashPossible = true;
                }
            }

            if (directionLeft)
            {
                rayCastHitDodge = Physics2D.Raycast(playerTransform.position, -transform.right, 2.1f,
            layerMaskPlayer & layerMaskBounds & layerMaskEnemy);

                if (rayCastHitDodge.collider != null)
                {
                    rayCastHitObject = rayCastHitDodge.transform.gameObject;
                    rayCastHitObjectString = rayCastHitObject.name;

                    if (rayCastHitObjectString == "Collision")
                    {
                        chargeDistantFloat = rayCastHitDodge.distance;
                        playerTransform.position = new Vector2(playerTransform.position.x - chargeDistantFloat,
                            playerTransform.position.y);
                        dashActive = true;
                        dashPossible = true;
                        rayCastHitObjectString = "Im Pickle Rick";
                    }
                }
                else if (rayCastHitDodge.collider == null)
                {
                    playerTransform.position = new Vector2(playerTransform.position.x - 2,
                        playerTransform.position.y);
                    dashActive = true;
                    dashPossible = true;
                }
            }
        }
        else if (playerStats.dexterity >= 14 && staminaMan.playerCurrentStamina > 50 && Input.GetButtonDown("Sprint") && playerMoving || playerStats.dexterity >= 14 && Input.GetButtonDown("SprintX") && playerMoving && !sprintPossible || playerStats.dexterity >= 14 && staminaMan.playerCurrentStamina > 50 && Input.GetKeyDown(KeyCode.LeftShift) && playerMoving)
        {
            if (sprintTimer > 0)
            {
                sprintPossible = true;
            }
        }
        else if (staminaMan.playerCurrentStamina > 10 && !movementDisabilityBool)
        {
            moveSpeed = 4.5f;
            dashActive = false;
            sprintActive = false;
            soFast = false;
            // sprintPossible = false;
        }
        else
        {
            sprintActive = false;
            soFast = false;
            // moveSpeed = 2;
            // sprintPossible = false;
        }

        if (sprintPossible && staminaMan.playerCurrentStamina >= 50)
        {
            sprintTimer -= Time.deltaTime;
            moveSpeed = 8f;
            sprintActive = true;
            soFast = true;
        }

        if (sprintTimer <= 0)
        {
            sprintPossible = false;
            sprintTimer = 0.2f;
        }

        if (staminaMan.playerCurrentStamina <= 200 && Input.GetButtonDown("DashX")
            || staminaMan.playerCurrentStamina <= 200 && Input.GetButtonDown("Dash"))
        {
            dashPossible = false;
        }

        // if (lockOn == false)
        // {
        //     if (directionUp == true)
        //     {
        //         directionInt = 0;
        //     }

        //     if (directionRight == true)
        //     {
        //         directionInt = 1;
        //     }

        //     if (directionDown == true)
        //     {
        //         directionInt = 2;
        //     }

        //     if (directionLeft == true)
        //     {
        //         directionInt = 3;
        //     }
        // }

        playerMoving = false;

        if (!theDM.dialogActive)
        {
            canMove = true;
            anim.speed = 1;
        }

        if (theDM.dialogActive)
        {
            anim.speed = 0;
        }

        if (!canMove)
        {
            myRigidbody.velocity = Vector2.zero;
            anim.SetBool("PlayerMoving", false);
            return;
        }

        axisInput = Input.GetAxisRaw("Attack");
        axisHorizontal = Input.GetAxisRaw("Horizontal");
        axisVertical = Input.GetAxisRaw("Vertical");

        if (axisHorizontal > 0.2f)
        {
            axisHorizontal = 0.5f;
        }

        if (axisHorizontal < -0.2f)
        {
            axisHorizontal = -0.5f;
        }

        if (axisVertical > 0.2f)
        {
            axisVertical = 0.5f;
        }

        if (axisVertical < -0.2f)
        {
            axisVertical = -0.5f;
        }

        //if (!attacking || !preAttack || !recovAttack)
        if (preAttackCounter == 0.2f && recovAttackCounter == 0.3f && attackingCounterNew == 0.06f)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")).normalized;

            if (axisHorizontal > 0.2f || axisHorizontal < -0.2f
                || axisVertical > 0.2f || axisVertical < -0.2f && moveInput != Vector2.zero)
            {
                myRigidbody.velocity = new Vector2(moveInput.x * moveSpeed,
                    moveInput.y * moveSpeed);
                playerMoving = true;
                wasMoving = true;
                if (lockOn == false)
                {
                    lastMove = new Vector2(axisHorizontal, axisVertical);
                }
            }
            else if (preAttackCounter < 0.2f)
            {
                wasMoving = true;
            }
            else
            {
                myRigidbody.velocity = Vector2.zero;
                wasMoving = false;
            }
        }

        //if (!attackLock && axisInput <= -0.2f && staminaMan.playerCurrentStamina > 400)
        if (!attackLock && axisInput <= -0.2f
            && recovAttackCounter == 0.3f && staminaMan.playerCurrentStamina >= 300 || !attackLock && Input.GetMouseButtonDown(0) && recovAttackCounter == 0.3f && staminaMan.playerCurrentStamina >= 300)
        {
            preAttack = true;
            attacking = true;
        }
        // else
        // {
        //     GameObject.Find("StaminaTell").SetActive(true);
        // }

        if (preAttack)
        {
            preAttackCounter -= Time.deltaTime;
            //preAttackCounter -= Time.frameCount;
        }

        if (playerStats.dexterity >= 14 && Input.GetButton("Sprint") && wasMoving || playerStats.dexterity >= 14 && Input.GetButton("SprintX") && wasMoving || playerStats.dexterity >= 14 && Input.GetKeyDown(KeyCode.LeftShift) && wasMoving)
        {
            wasSprint = true;
        }
        else
        {
            wasSprint = false;
        }

        if (preAttackCounter <= 0 && preAttack)
        {
            if (directionInt == 0)
            {
                playerTransform.position = new Vector2(playerTransform.position.x,
                playerTransform.position.y + 0.05f);
            }

            if (directionInt == 1)
            {
                playerTransform.position = new Vector2(playerTransform.position.x + 0.05f,
                playerTransform.position.y);
            }

            if (directionInt == 2)
            {
                playerTransform.position = new Vector2(playerTransform.position.x,
                playerTransform.position.y - 0.05f);
            }

            if (directionInt == 3)
            {
                playerTransform.position = new Vector2(playerTransform.position.x - 0.05f,
                playerTransform.position.y);
            }

            attackPossible = true;
            damagePossible = true;
            sfxMan.playerAttack.Play();
            //attackTimeCounter = attackTime; //I don't remember what this does.
            attackBool = true;

            myRigidbody.velocity = Vector2.zero;

            anim.SetBool("Attack", true);

            attackingCounterNew -= Time.deltaTime;

            if (attackingCounterNew <= 0)
            {
                preAttack = false;
                attackLock = true;

                staminaMan.playerCurrentStamina -= 300;
                //staminaAttackDrainBool = true;
            }
        }
        else
        {
            attackBool = false;
            damagePossible = false;
            anim.SetBool("Attack", false);
        }

        // else if (attackLock == false && staminaMan.playerCurrentStamina < 400)
        // {
        //     attackPossible = false;
        // }

        if (attackLock)
        {
            preAttackCounter = .2f;
            recovAttack = true;

            //staminaAttackDrainBool = false;
        }

        if (recovAttack)
        {
            recovAttackCounter -= Time.deltaTime;
        }

        //*************trying to create a backward slash******************
        if (axisInput > -0.2)
        {
            if (axisInput <= -0.2f)
            {
                // ReverseAttack();
            }
        }

        if (recovAttackCounter <= 0)
        {
            attackingCounterNew = 0.06f;
            recovAttack = false;
            recovAttackCounter = 0.3f;
        }
        if (!attacking)
        {
            damagePossible = false;
        }

        // if (staminaMan.playerCurrentStamina < 400)
        // {
        //     attacking = false;
        // }
        // else if (attackLock == false && staminaMan.playerCurrentStamina < 400)
        // {
        //     attackPossible = false;
        // }
        // else
        // {
        //     attackBool = false;
        // }

        if (axisInput >= 0f)
        {
            attackLock = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && staminaMan.playerCurrentStamina > 400)
        {
            sfxMan.playerAttack.Play();
            attacking = true;
            attackBoolMouse = true;
            myRigidbody.velocity = Vector2.zero;
            anim.SetBool("Attack", true);
            damagePossible = true;
            attackPossible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && staminaMan.playerCurrentStamina < 400)
        {
            // attackPossible = false;
            attackBoolMouse = false;
            attacking = false;
        }
        else
        {
            attackBoolMouse = false;
        }

        float clampX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
        float clampY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
        transform.position = new Vector3(clampX, clampY, transform.position.z);

        // if (lockOn == false)
        // {
        //     anim.SetFloat("MoveX", axisHorizontal);
        //     anim.SetFloat("MoveY", axisVertical);
        // }
        // else
        // {
        // }
        if (lockOn)
        {
            ChooseLockOnDirection(currentEnemyLocked);
        }
        // Debug.Log("Last Move********** :" + lastMove);

        anim.SetFloat("MoveX", lockOnHorizontal);
        anim.SetFloat("MoveY", lockOnVertical);

        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
        anim.SetBool("Blocking", playerShield.shieldOn);
        anim.SetBool("LockOn", lockOn);
        anim.SetBool("Preemptive Attack", preAttack);
        anim.SetBool("Recovery Attack", recovAttack);
        anim.SetBool("Attack Possible", attackPossible);
        // anim.SetInteger("directionIntX", directionInt);
        // anim.SetInteger("directionIntY", directionInt);
    }


    /*
    void LateUpdate()
    {
        if (staminaAttackDrainBool)
        {
            staminaAttackDrainBool = false;
        }
    }
    */
    // public void ReverseAttack()
    // {
    //     // Debug.Log("*************************************************************************************");
    //     attackingCounterNew = 0.06f;
    //     recovAttack = false;

    //     attacking = true;
    //     // if (recovAttackCounter <= 0)
    //     // {
    //     //     preAttackCounter -= Time.deltaTime;
    //     //     //preAttackCounter -= Time.frameCount;
    //     // }
    //     if (preAttackCounter <= 0 && preAttack)
    //     {
    //         if (directionInt == 0)
    //         {
    //             playerTransform.position = new Vector2(playerTransform.position.x,
    //             playerTransform.position.y + 0.1f);
    //         }
    //         if (directionInt == 1)
    //         {
    //             playerTransform.position = new Vector2(playerTransform.position.x + 0.1f,
    //             playerTransform.position.y);
    //         }
    //         if (directionInt == 2)
    //         {
    //             playerTransform.position = new Vector2(playerTransform.position.x,
    //             playerTransform.position.y - 0.1f);
    //         }
    //         if (directionInt == 3)
    //         {
    //             playerTransform.position = new Vector2(playerTransform.position.x - 0.1f,
    //             playerTransform.position.y);
    //         }
    //         attackPossible = true;
    //         damagePossible = true;
    //         sfxMan.playerAttack.Play();
    //         //attackTimeCounter = attackTime; //I don't remember what this does.
    //         attackBool = true;
    //         /* if(specialMove)
    //          {
    //              moveSpeed = 100000;
    //              //myRigidbody.velocity = new Vector2(0, 3);
    //              specialMove = false;

    //          }else
    //          {
    //              moveSpeed = 4.5f;
    //              myRigidbody.velocity = Vector2.zero;
    //          }*/

    //         myRigidbody.velocity = Vector2.zero;

    //         anim.SetBool("Attack", true);

    //         attackingCounterNew -= Time.deltaTime;

    //         if (attackingCounterNew <= 0)
    //         {
    //             preAttack = true;
    //             attackLock = true;

    //             // staminaMan.playerCurrentStamina -= 400;
    //             //staminaAttackDrainBool = true;
    //         }
    //     }

    //     else if (attackLock == false && staminaMan.playerCurrentStamina < 400)
    //     {
    //         attackPossible = false;
    //     }
    //     else
    //     {
    //         attackBool = false;
    //         damagePossible = false;
    //         anim.SetBool("Attack", false);
    //     }
    //     if (attackLock)
    //     {
    //         preAttackCounter = .2f;
    //         recovAttack = true;

    //         //staminaAttackDrainBool = false;
    //     }
    //     if (recovAttack)
    //     {
    //         recovAttackCounter -= Time.deltaTime;
    //     }
    //     if (recovAttackCounter <= 0)
    //     {
    //         attackingCounterNew = 0.06f;
    //         recovAttack = false;
    //         //attackTimeCounter = 10;
    //         recovAttackCounter = 0.3f;
    //     }
    //     if (!attacking)
    //     {
    //         damagePossible = false;
    //     }
    // }

    public void SetBounds(BoxCollider2D newBounds)
    {
        boundBox = newBounds;

        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
    }

    // meant to encourage player to face correct direction when moving
    public void MovementDisability()
    {
        movementHorizontal = Input.GetAxisRaw("Horizontal");
        movementVertical = Input.GetAxisRaw("Vertical");

        if (lockOn)
        {
            if (directionInt == 0 && movementVertical <= 0 || directionInt == 1 && movementHorizontal <= 0 || directionInt == 2 && movementVertical >= 0 || directionInt == 3 && movementHorizontal >= 0)
            {
                movementDisabilityBool = true;
                moveSpeed = 3.45f;
            }
            else if (directionInt == 0 && movementHorizontal != 0 || directionInt == 1 && movementVertical != 0 || directionInt == 2 && movementHorizontal != 0 || directionInt == 3 && movementVertical != 0)
            {
                movementDisabilityBool = true;
                moveSpeed = 4.5f;
            }
            else
            {
                movementDisabilityBool = true;
                moveSpeed = 4.75f;
            }
        }
        else
        {
            movementDisabilityBool = false;
            moveSpeed = 4.5f;
        }
    }

    // This method will add enemies to 4 separate lists representing 4 quadrants surrounding the player. 
    public void AddEnemiesToLists()
    {
        int dictCounter = 0;
        enemyListUp = new List<Transform>();
        enemyListRight = new List<Transform>();
        enemyListDown = new List<Transform>();
        enemyListLeft = new List<Transform>();
        // Dictionary will allow check for if enemy has already been locked when finding next closest enemy.
        enemyDict = new Dictionary<int, bool>();
        GameObject enemyMasterParentObject = GameObject.Find("Enemies");

        foreach (Transform enemyParentObject in enemyMasterParentObject.GetComponentInChildren<Transform>())
        {
            foreach (Transform enemy in enemyParentObject.GetComponentInChildren<Transform>())
            {
                string enemyQuadrant = DetermineQuadrant(enemy);

                // Only add enemies to list if they are not dead.
                if (enemy.GetComponent<EnemyHealthManager>().CurrentHealth > 0)
                {
                    if (enemyQuadrant == "up")
                    {
                        enemyListUp.Add(enemy);
                    }
                    else if (enemyQuadrant == "right")
                    {
                        enemyListRight.Add(enemy);
                    }
                    else if (enemyQuadrant == "down")
                    {
                        enemyListDown.Add(enemy);
                    }
                    else if (enemyQuadrant == "left")
                    {
                        enemyListLeft.Add(enemy);
                    }
                    else
                    {
                        Debug.Log("enemy is not in quadrant");
                    }
                    enemyList.Add(enemy);
                    enemyDict[dictCounter] = false;
                    dictCounter++;
                }
            }
        }
        enemyCount = enemyList.Count;
        enemyCounter = 0;
    }

    // This method will receive a string which will represent which quadrant to reshape dictionary to: upLeft being the upper left quadrant where -x & +y.
    public void ResetEnemyDict()
    {
        int dictCounter = 0;
        enemyDict = new Dictionary<int, bool>();
        GameObject enemyMasterParentObject = GameObject.Find("Enemies");
        foreach (Transform enemyParentObject in enemyMasterParentObject.GetComponentInChildren<Transform>())
        {
            foreach (Transform enemy in enemyParentObject.GetComponentInChildren<Transform>())
            {
                enemyDict[dictCounter] = false;
                dictCounter++;
            }
        }
        enemyCount = enemyList.Count;
        enemyCounter = 0;
    }

    public Transform FindClosestEnemy()
    {
        float distance = 0;
        Transform closestEnemy = null;

        // Sets initial closest enemy to compare against
        foreach (Transform enemy in enemyList)
        {
            if (enemy != null)
            {
                distance = Vector3.Distance(enemy.transform.position, this.transform.position);
                closestEnemy = enemy;
            }
        }

        foreach (Transform enemy in enemyList)
        {
            if (enemy != null)
            {
                if (Vector3.Distance(enemy.transform.position, this.transform.position) < distance)
                {
                    closestEnemy = enemy;
                    distance = Vector3.Distance(enemy.transform.position, this.transform.position);
                }
            }
        }
        if (closestEnemy == null)
        {
            lockOn = false;
        }
        currentEnemyLocked = closestEnemy;
        return closestEnemy;
    }
    // public Transform FindNextClosestEnemy(List<Transform> enemyListQuadrant, int curEnemyInt)
    public Transform FindNextClosestEnemy(List<Transform> enemyListQuadrant)
    {
        Transform closestEnemy = null;
        // This variable is to test whether the loop below has searched every item other than the current enemy.
        int endingEnemyInt = curEnemyInt;
        curEnemyInt++;
        int curClosestEnemyInt = curEnemyInt;
        float curClosestDistance = 0;

        if (curEnemyInt + 1 >= enemyListQuadrant.Count)
        {
            curEnemyInt = 0;
        }
        // Setting next closest enemy to the next enemy on the list as a default.
        if (enemyListQuadrant.Count > 0)
        {
            closestEnemy = enemyListQuadrant[curEnemyInt];
            curClosestDistance = Vector3.Distance(closestEnemy.transform.position, this.transform.position);
        }
        else
        {
            enemyListQuadrant = enemyList;
            closestEnemy = enemyListQuadrant[curEnemyInt];
            curClosestDistance = Vector3.Distance(closestEnemy.transform.position, this.transform.position);
        }

        // only set enemy if they are close enough to player
        int loopCounter = 0;

        while (curEnemyInt + 1 != endingEnemyInt && !(curEnemyInt + 1 == enemyListQuadrant.Count && endingEnemyInt == 0) && loopCounter < enemyListQuadrant.Count)
        {
            curEnemyInt++;

            if (curEnemyInt >= enemyListQuadrant.Count)
            {
                curEnemyInt = 0;
            }

            if (Vector3.Distance(enemyListQuadrant[curEnemyInt].transform.position, this.transform.position) < curClosestDistance && enemyDict[curEnemyInt] == false)
            {
                closestEnemy = enemyListQuadrant[curEnemyInt];
                curClosestDistance = Vector3.Distance(enemyListQuadrant[curEnemyInt].transform.position, this.transform.position);
                curClosestEnemyInt = curEnemyInt;
            }

            loopCounter++;
        }

        enemyCounter++;

        // If the whole enemy list has been checked then we want to reset the dictionary.
        if (enemyCounter >= enemyListQuadrant.Count)
        {
            ResetEnemyDict();
        }

        if (curEnemyInt >= enemyListQuadrant.Count)
        {
            curEnemyInt = -1;
            // closestEnemy = enemyList[curEnemyInt];
        }
        // else
        // {
        //     closestEnemy = enemyList[curEnemyInt];
        // }

        if (Vector3.Distance(closestEnemy.transform.position, this.transform.position) > 10)
        {
            closestEnemy = FindClosestEnemy();
            Debug.Log("getting here too often I think " + curEnemyInt + " " + enemyListQuadrant.Count);
        }
        else
        {
            enemyDict[curClosestEnemyInt] = true;
        }

        // Debug.Log(curEnemyInt);
        // Debug.Log("Ummmmmmmmmmm " + enemyList.Count);


        // sets initial closest enemy to compare against
        // foreach (Transform enemy in enemyList)
        // {
        //     if (enemy != null)
        //     {
        //         distance = Vector3.Distance(enemy.transform.position, this.transform.position);
        //         closestEnemy = enemy;
        //     }
        // }

        // foreach (Transform enemy in enemyList)
        // {
        //     if (enemy != null)
        //     {
        //         if (Vector3.Distance(enemy.transform.position, this.transform.position) < distance)
        //         {
        //             closestEnemy = enemy;
        //             distance = Vector3.Distance(enemy.transform.position, this.transform.position);
        //         }
        //     }
        // }
        // if (closestEnemy == null)
        // {
        //     lockOn = false;
        // }
        // currentEnemyLocked = closestEnemy;
        return closestEnemy;
    }


    public void LockOn()
    {
        // newListBool = true;
        if (newListBool)
        {
            AddEnemiesToLists();
            currentEnemyLocked = FindClosestEnemy();
        }
        // ChooseLockOnDirection(currentEnemyLocked);
        newListBool = false;

        // Debug.Log(FindClosestEnemy());

        // if (lockOn)
        // {
        //     lockOnHorizontal = Input.GetAxisRaw("LockOnHorizontal");
        //     lockOnVertical = Input.GetAxisRaw("LockOnVertical");

        //     if (lockOnHorizontal > 0.2f)
        //     {
        //         lockOnHorizontal = 0.5f;
        //         directionInt = 1;
        //     }

        //     if (lockOnHorizontal < -0.2f)
        //     {
        //         lockOnHorizontal = -0.5f;
        //         directionInt = 3;
        //     }

        //     if (lockOnVertical > 0.2f)
        //     {
        //         lockOnVertical = 0.5f;
        //         directionInt = 0;
        //     }

        //     if (lockOnVertical < -0.2f)
        //     {
        //         lockOnVertical = -0.5f;
        //         directionInt = 2;
        //     }

        //     if (lockOnHorizontal > 0.2f || lockOnHorizontal < -0.2f
        //     || lockOnVertical > 0.2f || lockOnVertical < -0.2f)
        //     {
        //         lastMove = new Vector2(lockOnHorizontal, lockOnVertical);
        //     }
        // }
    }

    public void ChooseLockOnDirection(Transform enemy)
    {
        if (enemy.GetComponent<EnemyHealthManager>().CurrentHealth <= 0)
        {
            lockOnImage.SetActive(false);
            // lockOn = false;
            enemyList.Remove(enemy);
            try
            {
                AddEnemiesToLists();
                enemy = FindClosestEnemy();
            }
            catch
            {
                lockOn = false;
                lockOnImage.SetActive(false);
                Debug.Log("it caught");
                return;
            }
            // lockOnImage.SetActive(true);
            // lockOnImage.transform.position = enemy.position;
            return;
        }

        if (Vector3.Distance(enemy.transform.position, this.transform.position) > 7)
        {
            lockOn = false;
            lockOnImage.SetActive(false);
            return;
        }


        lockOnImage.SetActive(true);
        lockOnImage.transform.position = enemy.position;

        float enemyTrackX = enemy.transform.position.x;
        float enemyTrackY = enemy.transform.position.y;

        float trackingMasterX = enemyTrackX - transform.position.x;
        float trackingMasterY = enemyTrackY - transform.position.y;

        if (trackingMasterY > 0)
        {
            if (trackingMasterX > 0) //Quadrant 2
            {
                if (trackingMasterX < trackingMasterY)
                {
                    directionInt = 0;
                    lockOnVertical = 0.5f;
                    lockOnHorizontal = 0f;
                    // Debug.Log("test: getting here");
                }
                else
                {
                    directionInt = 1;
                    lockOnHorizontal = 0.5f;
                    lockOnVertical = 0f;
                }
            }
            else if (trackingMasterX < 0)
            {
                if (Math.Abs(trackingMasterX) > trackingMasterY) //Quadrant 1
                {
                    directionInt = 3;
                    lockOnHorizontal = -0.5f;
                    lockOnVertical = 0f;
                }
                else
                {
                    directionInt = 0;
                    lockOnVertical = 0.5f;
                    lockOnHorizontal = 0;
                    // Debug.Log("test: getting here");
                }
            }
        }
        else if (trackingMasterY < 0) //Quadrant 4
        {
            if (trackingMasterX < 0)
            {
                if (trackingMasterX > trackingMasterY)
                {
                    directionInt = 2;
                    lockOnVertical = -0.5f;
                    lockOnHorizontal = 0f;
                }
                else if (trackingMasterX < trackingMasterY)
                {
                    directionInt = 3;
                    lockOnHorizontal = -0.5f;
                    lockOnVertical = 0f;
                }
            }
            else if (trackingMasterX > 0) //Quadrant 3
            {
                if (Math.Abs(trackingMasterY) > trackingMasterX)
                {
                    directionInt = 2;
                    lockOnVertical = -0.5f;
                    lockOnHorizontal = 0f;
                }
                else
                {
                    directionInt = 1;
                    lockOnHorizontal = 0.5f;
                    lockOnVertical = 0f;
                }
            }
        }
        lastMove = new Vector2(lockOnHorizontal, lockOnVertical);
    }

    // Will return which list the enemy should be added to (Up, Down, Left, Right)
    public string DetermineQuadrant(Transform enemy)
    {
        float enemyTrackX = enemy.transform.position.x;
        float enemyTrackY = enemy.transform.position.y;

        float trackingMasterX = enemyTrackX - transform.position.x;
        float trackingMasterY = enemyTrackY - transform.position.y;

        if (trackingMasterY > 0) //Top
        {
            if (trackingMasterX > 0) //Quadrant 2
            {
                if (trackingMasterX < trackingMasterY)
                {
                    return "up";
                }
                else
                {
                    return "right";
                }
            }
            else if (trackingMasterX < 0)
            {
                if (Math.Abs(trackingMasterX) > trackingMasterY) //Quadrant 1
                {
                    return "left";
                }
                else
                {
                    return "up";
                }
            }
        }
        else if (trackingMasterY < 0)  //Bottom
        {
            if (trackingMasterX < 0) //Quadrant 4
            {
                if (trackingMasterX > trackingMasterY)
                {
                    return "left";
                }
                else if (trackingMasterX < trackingMasterY)
                {
                    return "down";
                }
            }
            else if (trackingMasterX > 0) //Quadrant 3
            {
                if (Math.Abs(trackingMasterY) > trackingMasterX)
                {
                    return "down";
                }
                else
                {
                    return "right";
                }
            }
        }
        return "none";
    }

    // Will return which direction the right analog stick is inputting.
    public string DetermineAnalogQuadrant(float x, float y)
    {
        if (y >= 0) //Top
        {
            if (x >= 0) //Quadrant 2
            {
                if (x < y)
                {
                    return "up";
                }
                else
                {
                    return "right";
                }
            }
            else if (x <= 0)
            {
                if (Math.Abs(x) >= y) //Quadrant 1
                {
                    return "left";
                }
                else
                {
                    return "up";
                }
            }
        }
        else if (y <= 0)  //Bottom
        {
            if (x <= 0) //Quadrant 4
            {
                if (x >= y)
                {
                    return "left";
                }
                else if (x < y)
                {
                    return "down";
                }
            }
            else if (x >= 0) //Quadrant 3
            {
                if (Math.Abs(y) > x)
                {
                    return "down";
                }
                else
                {
                    return "right";
                }
            }
        }
        return "none";
    }


    public int DeterminePlayerDirection()
    {
        lockOnHorizontal = Input.GetAxisRaw("Horizontal");
        lockOnVertical = Input.GetAxisRaw("Vertical");

        if (!lockOn)
        {

            if (lockOnHorizontal > 0.2f)
            {
                lockOnHorizontal = 0.5f;
                directionInt = 1;
            }

            if (lockOnHorizontal < -0.2f)
            {
                lockOnHorizontal = -0.5f;
                directionInt = 3;
            }

            if (lockOnVertical > 0.2f)
            {
                lockOnVertical = 0.5f;
                directionInt = 0;
            }

            if (lockOnVertical < -0.2f)
            {
                lockOnVertical = -0.5f;
                directionInt = 2;
            }

            if (lockOnHorizontal > 0.2f || lockOnHorizontal < -0.2f
            || lockOnVertical > 0.2f || lockOnVertical < -0.2f)
            {
                lastMove = new Vector2(lockOnHorizontal, lockOnVertical);
            }
        }
        return directionInt;
    }
}





