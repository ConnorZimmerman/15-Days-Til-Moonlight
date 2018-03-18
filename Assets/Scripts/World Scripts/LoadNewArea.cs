using System.Collections;
using System.Collections.Generic;
// using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{
    public string levelToLoad;
    public string exitPoint;
    private PlayerController thePlayer;
    private PlayerHealthManager playerHealth;
    private PlayerStaminaManager playerStamina;
    private PlayerRangedAttack playerRangedAttack;
    private PlayerStats playerStatsScript;
    private PlayerStartPoint startPoint;
    // public GameObject startPoint;
    public static int counter;
    public string curLvl;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        playerHealth = FindObjectOfType<PlayerHealthManager>();
        playerStamina = FindObjectOfType<PlayerStaminaManager>();
        playerRangedAttack = FindObjectOfType<PlayerRangedAttack>();
        playerStatsScript = FindObjectOfType<PlayerStats>();
        startPoint = FindObjectOfType<PlayerStartPoint>();
        curLvl = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Snowy A Exits
        if (other.gameObject.name == "Player" && curLvl == "SnowyA" && this.gameObject.name == "SnowyBEntry")
        {
            SetAllForLvl();
            //PlayerPrefs.SetInt("Global Music Tracker", 0);
            SceneManager.LoadScene("SnowyB", LoadSceneMode.Single);
            PlayerPrefs.SetString("Global Player Start Point", "Snowy_B_A_Entrance");
        }
        else if (other.gameObject.name == "Player" && curLvl == "SnowyA" && this.gameObject.name == "SnowyCEntry")
        {
            SetAllForLvl();
            SceneManager.LoadScene("Lvl 2", LoadSceneMode.Single);
        }
        // Snowy B Exits
        else if (other.gameObject.name == "Player" && curLvl == "SnowyB" && this.gameObject.name == "SnowyAEntry")
        {
            SetAllForLvl();
            SceneManager.LoadScene("SnowyA", LoadSceneMode.Single);
            PlayerPrefs.SetString("Global Player Start Point", "Snowy_A_B_Entrance");

        }
        //Snowy A Exit
        // else if(other.gameObject.name == "Player" && curLvl == "SnowyA" && this.gameObject.name == "SnowyBEntry"){
        //     SetAllForLvl();
        //     SceneManager.LoadScene("SnowyB", LoadSceneMode.Single);
        //     PlayerPrefs.SetString("Global Player Start Point", "Snowy_B_A_Entrance");
        // }
        else if (other.gameObject.name == "Player" && curLvl == "Main")
        {
            SetAllForLvl();
            SceneManager.LoadScene("Lvl 2", LoadSceneMode.Single);
        }
        else if (other.gameObject.name == "Player" && curLvl == "Lvl 2")
        {
            SetAllForLvl();
            SceneManager.LoadScene("Lvl 3", LoadSceneMode.Single);
        }
    }
    public void SetAllForLvl()
    {
        PlayerPrefs.SetInt("Global Player Current Health", playerHealth.playerCurrentHealth);
        PlayerPrefs.SetInt("Global Player Max Health", playerHealth.playerMaxHealth);
        PlayerPrefs.SetInt("Global Player Current Stamina", playerStamina.playerCurrentStamina);
        PlayerPrefs.SetInt("Global Player Max Stamina", playerStamina.playerMaxStamina);
        PlayerPrefs.SetInt("Global Player Current Xp", playerStatsScript.currentExp);
        PlayerPrefs.SetInt("Global Player Level", playerStatsScript.currentLevel);
        PlayerPrefs.SetInt("Global Player Points To Spend", playerStatsScript.pointsToSpend);
        PlayerPrefs.SetInt("Global Player Vitality", playerStatsScript.vitality);
        PlayerPrefs.SetInt("Global Player Strength", playerStatsScript.strength);
        PlayerPrefs.SetInt("Global Player Dexterity", playerStatsScript.dexterity);
        PlayerPrefs.SetInt("Global Player Intelligence", playerStatsScript.intelligence);
        PlayerPrefs.SetInt("Global Player Dagger Count", playerRangedAttack.daggerCount);
        PlayerPrefs.SetFloat("Global Player Last Move X", thePlayer.lastMove.x);
        PlayerPrefs.SetFloat("Global Player Last Move Y", thePlayer.lastMove.y);
        PlayerPrefs.SetString("Global Player Cur Lvl", curLvl);
    }

}
