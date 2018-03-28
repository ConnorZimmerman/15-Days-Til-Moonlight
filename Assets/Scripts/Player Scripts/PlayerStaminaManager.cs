using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminaManager : MonoBehaviour
{
    public int playerMaxStamina;

    public int playerCurrentStamina;

    private PlayerController thePlayer;

    public ShieldBlock theShield;

    public DialogueManager dialog;

    public float staminaTimer;

    public bool staminaCharge;

    public bool staminaLock;

    public float playerStaminaPercent;

    public GameObject playerStaminaObject;
    public GameObject dmObject;
    private GlobalDataScript globalData;
    private PlayerStats playerStats;

    // Use this for initialization
    void Start()
    {
        //thePlayer = GetComponent<PlayerController>();
        dmObject = GameObject.Find("Dialogue Manager");
        dialog = dmObject.GetComponent<DialogueManager>();
        playerStats = FindObjectOfType<PlayerStats>();

        playerStaminaObject = GameObject.Find("Player");
        thePlayer = playerStaminaObject.GetComponent<PlayerController>();
        playerCurrentStamina = GlobalDataScript.globalPlayerCurrentStamina;
        playerMaxStamina = GlobalDataScript.globalPlayerDexterity * 50;
    }

    // Update is called once per frame
    void Update()
    {
        playerMaxStamina = playerStats.dexterity * 50;
        if (playerCurrentStamina < 0)
        {
            playerCurrentStamina = 0;
        }

        if (playerCurrentStamina > playerMaxStamina)
        {
            playerCurrentStamina = playerMaxStamina;
        }

        if (dialog.dialogActive == false && thePlayer.sprintActive == true && playerCurrentStamina > 0)
        {
            playerCurrentStamina -= 5 - DexterityModifier();
        }

        if (dialog.dialogActive == false && thePlayer.dashActive == true)
        {
            playerCurrentStamina -= 200;

        }

        //test condition
        //if (PlayerController.staminaAttackDrainBool == true)
        //{
        //    playerCurrentStamina -= 1;
        //}a



        //testing new conditions for stamina bug
        //if (thePlayer.damagePossible == true || thePlayer.attackBoolMouse == true)
        //if (thePlayer.damagePossible == true && PlayerController.staminaAttackDrainBool 
        //if (thePlayer.damagePossible == true && thePlayer.staminaAttackDrainBool)
        //|| thePlayer.attackBoolMouse == true

        // {
        //    playerCurrentStamina -= 400;
        //}

        // if (playerCurrentStamina <= 0)
        // {
        //     staminaTimer -= Time.deltaTime;
        //     staminaLock = true;
        // }

        // if (staminaTimer <= 0)
        // {
        //     staminaLock = false;
        //     staminaCharge = true;
        //     staminaTimer = 2;
        // }


        // if (thePlayer.dashActive == false && thePlayer.sprintActive == false
        //     && thePlayer.attackBool == false && thePlayer.attackBoolMouse == false && playerCurrentStamina <= 10000)
        // {
        //     if (staminaTimer == 2 && staminaLock == false && theShield.shieldOn == false)
        //     {
        //         playerCurrentStamina += 32;
        //         staminaCharge = true;
        //     }
        //     else if (theShield.shieldOn == true && staminaTimer == 2 && staminaLock == false)
        //     {
        //         playerCurrentStamina += 20;
        //         staminaCharge = true;
        //     }
        // }
        // else
        // {
        //     staminaCharge = false;
        // }

        playerStaminaPercent = (float)(double)playerCurrentStamina / playerMaxStamina * 100;

    }
    int DexterityModifier()
    {
        if (playerStats.dexterity >= 17 && playerStats.dexterity < 19)
        {
            return 1;
        }
        else if (playerStats.dexterity >= 19 && playerStats.dexterity < 21)
        {
            return 2;
        }
        else if (playerStats.dexterity >= 21 && playerStats.dexterity < 23)
        {
            return 3;
        }
        else if (playerStats.dexterity >= 23)
        {
            return 4;
        }
        return 0;
    }

    public void SetMaxStamina()
    {
        playerCurrentStamina = playerMaxStamina;
        GetComponent<Reload>();
    }
}
