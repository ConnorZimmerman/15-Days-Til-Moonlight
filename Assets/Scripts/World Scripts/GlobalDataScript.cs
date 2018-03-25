using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataScript : MonoBehaviour
{
    public static int globalPlayerCurrentHealth;
    public static int globalPlayerMaxHealth;
    public static int globalPlayerCurrentStamina;
    public static int globalPlayerMaxStamina;
    public static int globalPlayerLevel;
    public static int globalPlayerCurrentXp;
    public static int globalPlayerVitality;
    public static int globalPlayerStrength;
    public static int globalPlayerDexterity;
    public static int globalPlayerIntelligence;
    public static int globalPlayerDaggerCount;
    public static int globalPlayerPointsToSpend;
    public string globalPlayerStartPoint;
    public float globalPlayerLastMoveX;
    public float globalPlayerLastMoveY;
    public string globalPlayerCurLvl;

    //public static int globalSoundTrackInstance;
    
    void Awake()
    {
        globalPlayerCurrentHealth = PlayerPrefs.GetInt("Global Player Current Health", 5);
        globalPlayerMaxHealth = PlayerPrefs.GetInt("Global Player Max Health", 5);
        globalPlayerCurrentStamina = PlayerPrefs.GetInt("Global Player Current Stamina", 500);
        globalPlayerMaxStamina = PlayerPrefs.GetInt("Global Player Max Stamina", 500);
        globalPlayerCurrentXp = PlayerPrefs.GetInt("Global Player Current Xp", 75);
        globalPlayerLevel = PlayerPrefs.GetInt("Global Player Level", 11);
        globalPlayerPointsToSpend = PlayerPrefs.GetInt("Global Player Points To Spend", 0);
        globalPlayerVitality = PlayerPrefs.GetInt("Global Player Vitality", 5);
        globalPlayerStrength = PlayerPrefs.GetInt("Global Player Strength", 8);
        globalPlayerDexterity = PlayerPrefs.GetInt("Global Player Dexterity", 15);
        globalPlayerIntelligence = PlayerPrefs.GetInt("Global Player Intelligence", 5);
        globalPlayerDaggerCount = PlayerPrefs.GetInt("Global Player Dagger Count", 0);
        globalPlayerStartPoint = PlayerPrefs.GetString("Global Player Start Point", "SnowyA_StartPoint");
        globalPlayerLastMoveX = PlayerPrefs.GetFloat("Global Player Last Move X", 0);
        globalPlayerLastMoveY = PlayerPrefs.GetFloat("Global Player Last Move Y", -1);
        globalPlayerCurLvl = PlayerPrefs.GetString("Global Player Cur Lvl", "SnowyA");
        //globalSoundTrackInstance = PlayerPrefs.GetInt("Global Music Tracker", 1);
    }

}

//PlayerPrefs are how Unity permanently saves variables
//PlayerPrefs are named with a string name such as "XP Total" or "Player Current Health"
//the numeric value after the string name is what value will be used if none has been saved previously
//for example if the players current health never changed it might be 
//playerCurrentHealth = PlayerPrefs.GetFloat ("Player Current Health", 100);
//if the playerCurrentHealth has changed and was saved previously it will ignore the 100


//accesses saved variable into your public static float
//playerXPTotal = PlayerPrefs.GetFloat ("XP Total", 0);


