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
    public static int globalPlayerDaggerCount;
    public static int globalPlayerPointsToSpend;

    //public static int globalSoundTrackInstance;
    // Use this for initialization
    void Awake()
    {
        globalPlayerCurrentHealth = PlayerPrefs.GetInt("Global Player Current Health", 10);
        globalPlayerMaxHealth = PlayerPrefs.GetInt("Global Player Max Health", 10);
        globalPlayerCurrentStamina = PlayerPrefs.GetInt("Global Player Current Stamina", 10000);
        globalPlayerMaxStamina = PlayerPrefs.GetInt("Global Player Max Stamina", 10000);
        globalPlayerCurrentXp = PlayerPrefs.GetInt("Global Player Current Xp", 0);
        globalPlayerLevel = PlayerPrefs.GetInt("Global Player Level", 1);
        globalPlayerPointsToSpend = PlayerPrefs.GetInt("Global Player Points To Spend", 0);
        globalPlayerVitality = PlayerPrefs.GetInt("Global Player Vitality", 10);
        globalPlayerDaggerCount = PlayerPrefs.GetInt("Global Player Dagger Count", 5);
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


