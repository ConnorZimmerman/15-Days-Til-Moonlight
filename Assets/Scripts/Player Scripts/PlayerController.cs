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
    //new variable test for stamina bug
    //public static bool staminaAttackDrainBool;
    // public bool staminaAttackDrainBool;
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
    //public float trackX;
    //public float trackY;
    //public GameObject targetX;
    //public GameObject targetY;
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

        if (globalData.globalPlayerLockOn == 1)
        {
            lockOn = true;
        }
        else
        {
            lockOn = false;
        }

        equipmentBuffManagerScript = FindObjectOfType<EquipmentBuffManager>();

        // Debug.Log(equipmentBuffManagerScript.PlayerDefenseCalculator());
    }

    // Update is called once per frame
    void Update()
    {
        playerNewHealth = playerHealth.playerCurrentHealth;
        //playerHealth.oldPlayerCurrentHealth = playerHealth.playerCurrentHealth;

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

        if (Input.GetButtonUp("LockOn") && lockOn == false)
        {
            lockOn = true;
        }
        else if (Input.GetButtonUp("LockOn") && lockOn == true)
        {
            lockOn = false;
        }

        if (lockOn)
        {
            lockOnHorizontal = Input.GetAxisRaw("LockOnHorizontal");
            lockOnVertical = Input.GetAxisRaw("LockOnVertical");

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
        if (!enemy.deathSeven)
        {
            check = enemy.moveDirectionX - directionInt;
            enemyInt = enemy.moveDirectionX;
            playerInt = directionInt;
        }

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
            moveSpeed = 4.5f;
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
        else if (playerStats.dexterity >= 14 && staminaMan.playerCurrentStamina > 50 && Input.GetButtonDown("Sprint") && playerMoving || playerStats.dexterity >= 14 && Input.GetButtonDown("SprintX") && playerMoving && !sprintPossible)
        {
            if (sprintTimer > 0)
            {
                sprintPossible = true;
            }
        }
        else if (staminaMan.playerCurrentStamina > 10)
        {
            moveSpeed = 4.5f;
            dashActive = false;
            sprintActive = false;
            soFast = false;
            // sprintPossible = false;
        }
        else
        {
            soFast = false;
            // moveSpeed = 2;
            // sprintPossible = false;
        }

        if (sprintPossible && staminaMan.playerCurrentStamina >= 50)
        {
            sprintTimer -= Time.deltaTime;
            moveSpeed = 12f;
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

        if (lockOn == false)
        {
            if (directionUp == true)
            {
                directionInt = 0;
            }
            if (directionRight == true)
            {
                directionInt = 1;
            }
            if (directionDown == true)
            {
                directionInt = 2;
            }
            if (directionLeft == true)
            {
                directionInt = 3;
            }
        }

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

            if (Input.GetAxisRaw("Horizontal") > 0.2f)
            {
                directionRight = true;
                directionLeft = false;
                directionUp = false;
                directionDown = false;
            }
            else if (Input.GetAxisRaw("Horizontal") < -0.2f)
            {
                directionLeft = true;
                directionRight = false;
                directionUp = false;
                directionDown = false;
            }

            if (Input.GetAxisRaw("Vertical") > 0.2f)
            {
                directionUp = true;
                directionLeft = false;
                directionRight = false;
                directionDown = false;
            }
            else if (Input.GetAxisRaw("Vertical") < -0.2f)
            {
                directionDown = true;
                directionUp = false;
                directionLeft = false;
                directionRight = false;
            }
        }

        //if (!attackLock && axisInput <= -0.2f && staminaMan.playerCurrentStamina > 400)
        if (!attackLock && axisInput <= -0.2f
            && recovAttackCounter == 0.3f)
        {
            preAttack = true;
            attacking = true;
        }
        if (preAttack)
        {
            preAttackCounter -= Time.deltaTime;
            //preAttackCounter -= Time.frameCount;
        }
        if (playerStats.dexterity >= 14 && Input.GetButton("Sprint") && wasMoving || playerStats.dexterity >= 14 && Input.GetButton("SprintX") && wasMoving)
        {
            wasSprint = true;
        }
        else
        {
            wasSprint = false;
        }

        //if (preAttackCounter <= 0)
        //{
        //preAttack = false;
        //}


        //moved down
        //if (preAttackCounter <= 0 && attackTimeCounter > 0)


        //if (preAttackCounter <= 0)
        if (preAttackCounter <= 0 && preAttack)
        {

            //moved from above
            //preAttack = false;
            //specialMove = true;
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
            /* if(specialMove)
             {
                 moveSpeed = 100000;
                 //myRigidbody.velocity = new Vector2(0, 3);
                 specialMove = false;

             }else
             {
                 moveSpeed = 4.5f;
                 myRigidbody.velocity = Vector2.zero;
             }*/

            myRigidbody.velocity = Vector2.zero;

            anim.SetBool("Attack", true);

            attackingCounterNew -= Time.deltaTime;

            if (attackingCounterNew <= 0)
            {
                preAttack = false;
                attackLock = true;

                // staminaMan.playerCurrentStamina -= 1000;
                //staminaAttackDrainBool = true;
            }
        }

        // else if (attackLock == false && staminaMan.playerCurrentStamina < 400)
        // {
        //     attackPossible = false;
        // }
        else
        {
            attackBool = false;
            damagePossible = false;
            anim.SetBool("Attack", false);
        }

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
            //attackTimeCounter = 10;
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
            attackPossible = false;
            attackBoolMouse = false;
            attacking = false;
        }
        else
        {
            attackBoolMouse = false;
        }

        float clampX = Mathf.Clamp(transform.position.x, minBounds.x,
           maxBounds.x);
        float clampY = Mathf.Clamp(transform.position.y, minBounds.y,
            maxBounds.y);
        transform.position = new Vector3(clampX, clampY, transform.position.z);

        if (lockOn == false)
        {
            anim.SetFloat("MoveX", axisHorizontal);
            anim.SetFloat("MoveY", axisVertical);
        }
        else
        {
            anim.SetFloat("MoveX", lockOnHorizontal);
            anim.SetFloat("MoveY", lockOnVertical);
        }

        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
        anim.SetBool("Blocking", playerShield.shieldOn);
        anim.SetBool("LockOn", lockOn);
        anim.SetBool("Preemptive Attack", preAttack);
        anim.SetBool("Recovery Attack", recovAttack);
        anim.SetBool("Attack Possible", attackPossible);
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
}






