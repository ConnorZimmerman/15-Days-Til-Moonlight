using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    private PlayerStaminaManager playerStamina;

    public Text staminaTell;
    public float staminaTellCounter;
    public bool staminaTellCounterStart;

    private PlayerController thePlayer;

    // Use this for initialization
    void Start()
    {
        playerStamina = FindObjectOfType<PlayerStaminaManager>();

        thePlayer = FindObjectOfType<PlayerController>();

        staminaTellCounter = 1;
        staminaTellCounterStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStamina.staminaLock || staminaTellCounter < 1 && staminaTellCounter > 0)
        {
            staminaTell.text = "-STAMINA";
        }
        else if (thePlayer.attackPossible == false || thePlayer.dashPossible == false)
        {
            staminaTellCounterStart = true;
        }
        else
        {
            staminaTell.text = "";
        }

        if (staminaTellCounterStart)
        {
            staminaTellCounter -= Time.deltaTime;
        }

        if (staminaTellCounter <= 0)
        {
            thePlayer.attackPossible = true;
            thePlayer.dashPossible = true;
            staminaTellCounter = 1;
            staminaTellCounterStart = false;
        }

        if (staminaTellCounter == 1)
        {
            staminaTellCounterStart = false;
            thePlayer.attackPossible = true;
            thePlayer.dashPossible = true;

            if (playerStamina.staminaLock == false)
            {
                staminaTell.text = "";
            }
        }
    }
}
