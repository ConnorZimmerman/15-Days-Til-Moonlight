using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealthManager : MonoBehaviour
{

    public int playerMaxHealth;

    public int playerCurrentHealth;
    public int oldPlayerCurrentHealth;
    public bool playerDamage;

    private bool flashActive;
    public float flashLength;
    private float flashCounter;

    private SpriteRenderer playerSprite;
    private PlayerStats playerStatsScript;

    public float waitToReload;

    public bool playerIsDead;

    public float playerHealthPercent;

    private GlobalDataScript globalData;

    // private PlayerController thePlayer;

    // Use this for initialization
    void Start()
    {
        playerDamage = false;
        playerSprite = GetComponent<SpriteRenderer>();
        playerStatsScript = FindObjectOfType<PlayerStats>();
        playerCurrentHealth = GlobalDataScript.globalPlayerCurrentHealth;
        playerMaxHealth = GlobalDataScript.globalPlayerVitality;
        oldPlayerCurrentHealth = playerCurrentHealth;
        // thePlayer = FindObjectOfType<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {
        playerMaxHealth = playerStatsScript.vitality;
        if (playerCurrentHealth <= 0)
        {
            playerIsDead = true;
            //PlayerController.staminaAttackDrainBool = false;
            //thePlayer.staminaAttackDrainBool = false;

            gameObject.SetActive(false);

            //return;

            //GetComponent<Reload>();
        }

        if (playerCurrentHealth > playerMaxHealth)
        {
            playerCurrentHealth = playerMaxHealth;
        }

        if (playerCurrentHealth < oldPlayerCurrentHealth)
        {
            playerDamage = true;

            oldPlayerCurrentHealth = playerCurrentHealth;
        }

        if (flashActive)
        {
            if (flashCounter > flashLength * .66f)
            {
                playerSprite.color = new Color(playerSprite.color.r,
            playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * .33f)
            {
                playerSprite.color = new Color(playerSprite.color.r,
            playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > 0)
            {
                playerSprite.color = new Color(playerSprite.color.r,
            playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else
            {
                playerSprite.color = new Color(playerSprite.color.r,
            playerSprite.color.g, playerSprite.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;
        }


        playerHealthPercent = (float)(double)playerCurrentHealth / playerMaxHealth * 100;
    }


    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;
        flashActive = true;
        flashCounter = flashLength;
    }

    public void SetMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }
}
