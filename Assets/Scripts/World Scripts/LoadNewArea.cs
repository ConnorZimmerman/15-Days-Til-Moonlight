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
        curLvl = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && curLvl == "Main")
        {
            //SceneManager.LoadScene(levelToLoad);
            //            GlobalDataScript.globalPlayerCurrentHealth = playerHealth.playerCurrentHealth;
            PlayerPrefs.SetInt("Global Player Current Health", playerHealth.playerCurrentHealth);
            PlayerPrefs.SetInt("Global Player Current Stamina", playerStamina.playerCurrentStamina);
            //PlayerPrefs.SetInt("Global Music Tracker", 0);
            SceneManager.LoadScene("Lvl 2", LoadSceneMode.Single);
            thePlayer.startPoint = exitPoint;
        }
        else if (other.gameObject.name == "Player" && curLvl == "Lvl 2")
        {
            PlayerPrefs.SetInt("Global Player Current Health", playerHealth.playerCurrentHealth);
            PlayerPrefs.SetInt("Global Player Current Stamina", playerStamina.playerCurrentStamina);
            SceneManager.LoadScene("Lvl 3", LoadSceneMode.Single);
            thePlayer.startPoint = exitPoint;
        }
    }
}
