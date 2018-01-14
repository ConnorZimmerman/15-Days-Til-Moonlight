using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{

    public string levelToLoad;

    public string exitPoint;

    private PlayerController thePlayer;

    private PlayerHealthManager playerHealth;

    public GameObject startPoint;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        playerHealth = FindObjectOfType<PlayerHealthManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            //SceneManager.LoadScene(levelToLoad);
            //            GlobalDataScript.globalPlayerCurrentHealth = playerHealth.playerCurrentHealth;
            PlayerPrefs.SetInt("Global Player Current Health", playerHealth.playerCurrentHealth);
            //PlayerPrefs.SetInt("Global Music Tracker", 0);
            SceneManager.LoadScene("Lvl 2", LoadSceneMode.Single);
            thePlayer.startPoint = exitPoint;
        }
    }
}
