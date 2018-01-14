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

    // Use this for initialization
    void Start()
    {
        //thePlayer = GetComponent<PlayerController>();
        dmObject = GameObject.Find("Dialogue Manager");
        dialog = dmObject.GetComponent<DialogueManager>();

        playerStaminaObject = GameObject.Find("Player");
        thePlayer = playerStaminaObject.GetComponent<PlayerController>();


    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentStamina < 0)
        {
            playerCurrentStamina = 0;
        }

        if (dialog.dialogActive == false && thePlayer.sprintActive == true && playerCurrentStamina > 0)
        {
            playerCurrentStamina -= 50;
        }

        if (dialog.dialogActive == false && thePlayer.dashActive == true)
        {
            playerCurrentStamina -= 2000;
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

        if (playerCurrentStamina <= 0)
        {
            staminaTimer -= Time.deltaTime;
            staminaLock = true;
        }

        if (staminaTimer <= 0)
        {
            staminaLock = false;
            staminaCharge = true;
            staminaTimer = 2;
        }
        

        if (thePlayer.dashActive == false && thePlayer.sprintActive == false
            && thePlayer.attackBool == false && thePlayer.attackBoolMouse == false && playerCurrentStamina <= 20000)
        {
            if (staminaTimer == 2 && staminaLock == false && theShield.shieldOn == false)
            {
                playerCurrentStamina += 8;
                staminaCharge = true;
            }
            else if (theShield.shieldOn == true && staminaTimer == 2 && staminaLock == false)
            {
                playerCurrentStamina += 6;
                staminaCharge = true;
            }
        }
        else
        {
            staminaCharge = false;
        }

        playerStaminaPercent = (float)(double)playerCurrentStamina / playerMaxStamina * 100;

    }

    public void SetMaxStamina()
    {
        playerCurrentStamina = playerMaxStamina;
        GetComponent<Reload>();
    }
}
