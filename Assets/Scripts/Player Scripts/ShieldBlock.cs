using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlock : MonoBehaviour
{
    public BoxCollider2D shieldBlock;
    private HurtPlayer damageControl;
    public int damageTest;
    public bool shieldOn;
    private float axisInput;
    private PlayerStaminaManager playerStaminaMan;
    private PlayerController thePlayer;
    private PlayerStats playerStatsScript;
    public int shieldBlocksLeft;
    public bool shieldLockBool; //bool to make player blocking more dynamic (shield is turned off when hit)
    public float shieldBlockTimer;

    // Use this for initialization
    void Start()
    {
        playerStaminaMan = FindObjectOfType<PlayerStaminaManager>();

        thePlayer = FindObjectOfType<PlayerController>();

        FindObjectOfType<HurtPlayer>();
        playerStatsScript = FindObjectOfType<PlayerStats>();

        shieldOn = false;

        shieldLockBool = false;
        shieldBlocksLeft = ShieldBlocksLeft();
        shieldBlockTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        axisInput = Input.GetAxisRaw("BlockX");

        if (playerStaminaMan.playerCurrentStamina <= 0)
        {
            shieldBlock.isTrigger = true;
            shieldOn = false;
        }

        if (!shieldOn)
        {
            shieldBlocksLeft = ShieldBlocksLeft();
        }

        if (shieldBlocksLeft <= 0)
        {
            shieldLockBool = true;
        }

        if (axisInput >= 0.2f && playerStaminaMan.playerCurrentStamina > 0
            && thePlayer.preAttackCounter == 0.2f && thePlayer.recovAttackCounter == 0.3f
            && thePlayer.attackingCounterNew == 0.06f && !shieldLockBool)
        {
            shieldBlock.isTrigger = false;
            shieldBlockTimer -= Time.deltaTime;

            // if (shieldBlockTimer > 0)
            // {
            shieldOn = true;
            // }
            // else
            // {
            //     shieldBlockTimer = 1f;
            //     shieldOn = false;
            //     shieldLockBool = true;
            // }
            // thePlayer.lockOn = true;
        }
        else
        {
            // thePlayer.lockOn = false;
        }

        if (axisInput <= 0f || shieldLockBool)
        {
            shieldBlock.isTrigger = true;
            shieldOn = false;
            shieldBlockTimer = 1f;
        }

        if (axisInput <= 0)
        {
            shieldLockBool = false;
        }

        if (Input.GetButton("Block") && shieldOn == false && playerStaminaMan.playerCurrentStamina > 0)
        {
            shieldBlock.isTrigger = false;
            shieldOn = true;
        }
        else if (Input.GetButton("Block") && shieldOn == true)
        {
            shieldBlock.isTrigger = true;
            shieldOn = false;
        }
    }

    int ShieldBlocksLeft()
    {
        if (playerStatsScript.strength >= 4 && playerStatsScript.strength < 6)
        {
            shieldBlocksLeft = 2;
        }
        else if (playerStatsScript.strength >= 6 && playerStatsScript.strength <= 9)
        {
            shieldBlocksLeft = 3;
        }
        else if (playerStatsScript.strength >= 10)
        {
            shieldBlocksLeft = 4;
        }
        else
        {
            shieldBlocksLeft = 1;
        }
        return shieldBlocksLeft;
    }
}
