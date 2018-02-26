using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    public int oldCurrentHealth; /*This variable was created to know when the enemy has been hurt
    it is used by functions in the enemyTestScript*/

    public int expToGive;

    public EnemyTestScript enemy;
    public PlayerStats thePlayerStats; //player stats added to give xp upon enemies death
                                       // public PlayerController thePlayer;
    public GameObject thePlayer;

    public float enemyHealthPercent; /*All health/stamina has percentages for the enemy to calculate
    its priorities more efficiently*/

    public bool fredIsDead; //Fred is the enemy
    private float deathCounter; //how long the death animation lasts for before deleting Fred

    private GameObject enemyObject;
    private EnemyMasterScript enemyMaster;
    public bool setCurrentHealthAtStart;
    public BoxCollider2D bodyCollider;
    public ItemDrop itemDropScript;

    // static ItemDrop instance;

    // Use this for initialization
    void Start()
    {
        enemyObject = this.gameObject;
        enemyMaster = enemyObject.GetComponent<EnemyMasterScript>();
        thePlayerStats = FindObjectOfType<PlayerStats>();
        bodyCollider = enemyObject.GetComponent<BoxCollider2D>();
        thePlayer = GameObject.Find("Player");
        itemDropScript = FindObjectOfType<ItemDrop>();

        deathCounter = 2;

        fredIsDead = false;

        setCurrentHealthAtStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (setCurrentHealthAtStart)
        {
            MaxHealth = enemyMaster.enemyMaxHealth;
            CurrentHealth = MaxHealth;
            oldCurrentHealth = CurrentHealth;
            setCurrentHealthAtStart = false;
        }

        if (CurrentHealth <= 0)
        {
            Destroy(bodyCollider);
            if (deathCounter == 2)
            {
                thePlayer.GetComponent<PlayerStaminaManager>().playerCurrentStamina += 2000;
            }
            deathCounter -= Time.deltaTime;
            fredIsDead = true;
        }

        if(CurrentHealth > MaxHealth){
            CurrentHealth = MaxHealth;
        }

        if (deathCounter <= 0)
        {
            //thePlayer.currentEnemyExists = false; //turned off 10/10 (not sure if problem)
            thePlayerStats.AddExperience(expToGive);
            itemDropScript.CreateItem(enemyObject);
            deathCounter = 2;
            Destroy(gameObject);
        }

        enemyHealthPercent = (float)(double)CurrentHealth / MaxHealth * 100; /*(float)(double) was
        just something I found online on how to convert to percentages (not sure how it works)*/
    }

    public void HurtEnemy(int damageToGive) //damage from player
    {
        CurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        CurrentHealth = enemyMaster.enemyMaxHealth;
    }
}