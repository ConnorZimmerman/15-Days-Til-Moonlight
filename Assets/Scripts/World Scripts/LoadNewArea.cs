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
    public GameObject startPoint;
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
        curLvl = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && curLvl == "Main")
        {
            //SceneManager.LoadScene(levelToLoad);
            //            GlobalDataScript.globalPlayerCurrentHealth = playerHealth.playerCurrentHealth;
            PlayerPrefs.SetInt("Global Player Current Health", playerHealth.playerCurrentHealth);
            PlayerPrefs.SetInt("Global Player Max Health", playerHealth.playerMaxHealth);
            PlayerPrefs.SetInt("Global Player Current Stamina", playerStamina.playerCurrentStamina);
            PlayerPrefs.SetInt("Global Player Max Stamina", playerStamina.playerMaxStamina);
            PlayerPrefs.SetInt("Global Player Current Xp", playerStatsScript.currentExp);
            PlayerPrefs.SetInt("Global Player Level", playerStatsScript.currentLevel);
            PlayerPrefs.SetInt("Global Player Points To Spend", playerStatsScript.pointsToSpend);
            PlayerPrefs.SetInt("Global Player Vitality", playerStatsScript.vitality);
            PlayerPrefs.SetInt("Global Player Dagger Count", playerRangedAttack.daggerCount);
            //PlayerPrefs.SetInt("Global Music Tracker", 0);
            SceneManager.LoadScene("Lvl 2", LoadSceneMode.Single);
            thePlayer.startPoint = exitPoint;
        }
        else if (other.gameObject.name == "Player" && curLvl == "Lvl 2")
        {
            PlayerPrefs.SetInt("Global Player Current Health", playerHealth.playerCurrentHealth);
            PlayerPrefs.SetInt("Global Player Max Health", playerHealth.playerMaxHealth);
            PlayerPrefs.SetInt("Global Player Current Stamina", playerStamina.playerCurrentStamina);
            PlayerPrefs.SetInt("Global Player Max Stamina", playerStamina.playerMaxStamina);
            PlayerPrefs.SetInt("Global Player Current Xp", playerStatsScript.currentExp);
            PlayerPrefs.SetInt("Global Player Level", playerStatsScript.currentLevel);
            PlayerPrefs.SetInt("Global Player Points To Spend", playerStatsScript.pointsToSpend);
            PlayerPrefs.SetInt("Global Player Vitality", playerStatsScript.vitality);
            PlayerPrefs.SetInt("Global Player Dagger Count", playerRangedAttack.daggerCount);
            SceneManager.LoadScene("Lvl 3", LoadSceneMode.Single);
            thePlayer.startPoint = exitPoint;
        }
    }
}
