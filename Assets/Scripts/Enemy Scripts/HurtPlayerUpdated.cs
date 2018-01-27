using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerUpdated : MonoBehaviour
{

   // public int damageToGive;
    //public int currentDamage;
    public GameObject bloodBurst;
    public Transform hitPoint;
    public GameObject damageNumber;
    public GameObject swordClash;
    public Transform swordClashPoint;

    private ShieldBlock shield;
    public bool shieldOn;

    private PlayerStats thePS;

    private EnemyTestScript enemy;

    public bool colliderOn;

    public bool attacking;

    private SFXManager sfxMan;


    private PlayerStaminaManager playerStaminaMan;

    private EnemyHealthManager enemyHealth;
    // public EnemyStaminaManager enemyStamina;

    //private bool noShieldBlood;

    public bool hit;

    public bool deathStrike;

    private PlayerController thePlayer;
    private PlayerHealthManager playerHealth;
    private HurtEnemy hurtEnemy;

    public int check;
    public int playerInt;
    public int enemyInt;

    private bool showBlood;
    public bool playerStaminaDrain;

    public bool strikeBlock;

    public bool faceOff;
    public float waitOnStrikeBlock;

    public float enemyAttackCounter;

    public bool enemyDamagePossible;

    private EngagedWithPlayer playerEngagement;

    // private EnemyStaminaManager enemyStaminaMan;

    public int counter;

    public bool enemyWeaponColliderBool;


    // Use this for initialization
    void Start()
    {
        playerStaminaMan = FindObjectOfType<PlayerStaminaManager>();

        thePlayer = FindObjectOfType<PlayerController>();

        sfxMan = FindObjectOfType<SFXManager>();

        shield = FindObjectOfType<ShieldBlock>();

        thePS = FindObjectOfType<PlayerStats>();

        enemy = FindObjectOfType<EnemyTestScript>();

        playerHealth = FindObjectOfType<PlayerHealthManager>();
        hurtEnemy = FindObjectOfType<HurtEnemy>();

        enemyHealth = FindObjectOfType<EnemyHealthManager>();

        playerEngagement = FindObjectOfType<EngagedWithPlayer>();

        // enemyStaminaMan = FindObjectOfType<EnemyStaminaManager>();

        deathStrike = false;

        showBlood = false;
        playerStaminaDrain = false;

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyWeaponColliderBool = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyWeaponColliderBool = false;
        }
    }


}




