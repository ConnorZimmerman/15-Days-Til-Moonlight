using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int currentLevel;
    public int currentExp;
    public int playerDamage;
    public int[] toLevelUp;
    public int[] HPLevels;
    public int[] attackLevels;
    public int[] defenseLevels;
    public int currentHP;
    public int currentAttack;
    public int currentDefense;
    public int vitality;
    public int strength;
    public int dexterity;
    public int intelligence;
    private PauseMenuButtons pauseMenuButtonsScript;
    private PlayerHealthManager thePlayerHealth;
    // private GlobalDataScript globalData;
    public int pointsToSpend;

    // Use this for initialization
    void Start()
    {
        currentHP = HPLevels[1];
        currentAttack = attackLevels[1];
        currentDefense = defenseLevels[1];
        thePlayerHealth = FindObjectOfType<PlayerHealthManager>();
        vitality = GlobalDataScript.globalPlayerVitality;
        currentLevel = GlobalDataScript.globalPlayerLevel;
        currentExp = GlobalDataScript.globalPlayerCurrentXp;
        pointsToSpend = GlobalDataScript.globalPlayerPointsToSpend;
        strength = GlobalDataScript.globalPlayerStrength;
        dexterity = GlobalDataScript.globalPlayerDexterity;
        intelligence = GlobalDataScript.globalPlayerIntelligence;
        if (strength <= 10)
        {
            playerDamage = 2;
        }
        else if (strength >= 11)
        {
            playerDamage = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (strength <= 10)
        {
            playerDamage = 2;
        }
        else if (strength >= 11)
        {
            playerDamage = 3;
        }
        if (currentExp >= toLevelUp[currentLevel])
        {
            LevelUp();
        }
    }

    public void AddExperience(int experienceToAdd)
    {
        currentExp += experienceToAdd;
    }

    public void LevelUp()
    {
        currentLevel++;
        pointsToSpend++;
        // currentHP = HPLevels[currentLevel];

        // thePlayerHealth.playerMaxHealth = currentHP;
        // thePlayerHealth.playerCurrentHealth += currentHP - HPLevels[currentLevel - 1];

        // currentAttack = attackLevels[currentLevel];
        // currentDefense = defenseLevels[currentLevel];
    }
}
