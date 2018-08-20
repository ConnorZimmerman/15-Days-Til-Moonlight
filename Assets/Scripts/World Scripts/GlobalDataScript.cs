using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
    public int globalPlayerLockOn;

    //public static int globalSoundTrackInstance;

    void Awake()
    {
        globalPlayerCurrentHealth = PlayerPrefs.GetInt("Global Player Current Health", 5);
        globalPlayerMaxHealth = PlayerPrefs.GetInt("Global Player Max Health", 5);
        globalPlayerCurrentStamina = PlayerPrefs.GetInt("Global Player Current Stamina", 500);
        globalPlayerMaxStamina = PlayerPrefs.GetInt("Global Player Max Stamina", 500);
        globalPlayerCurrentXp = PlayerPrefs.GetInt("Global Player Current Xp", 75);
        globalPlayerLevel = PlayerPrefs.GetInt("Global Player Level", 11);
        globalPlayerPointsToSpend = PlayerPrefs.GetInt("Global Player Points To Spend", 10);
        globalPlayerVitality = PlayerPrefs.GetInt("Global Player Vitality", 5);
        globalPlayerStrength = PlayerPrefs.GetInt("Global Player Strength", 3);
        globalPlayerDexterity = PlayerPrefs.GetInt("Global Player Dexterity", 10);
        globalPlayerIntelligence = PlayerPrefs.GetInt("Global Player Intelligence", 5);
        globalPlayerDaggerCount = PlayerPrefs.GetInt("Global Player Dagger Count", 10);
        globalPlayerStartPoint = PlayerPrefs.GetString("Global Player Start Point", "SnowyA_StartPoint");
        globalPlayerLastMoveX = PlayerPrefs.GetFloat("Global Player Last Move X", 0);
        globalPlayerLastMoveY = PlayerPrefs.GetFloat("Global Player Last Move Y", -1);
        globalPlayerCurLvl = PlayerPrefs.GetString("Global Player Cur Lvl", "SnowyA");
        globalPlayerLockOn = PlayerPrefs.GetInt("Global Player Lock On", 1);
        //globalSoundTrackInstance = PlayerPrefs.GetInt("Global Music Tracker", 1);
    }

    public void Save(List<ItemSlot> inventory, List<ItemSlot> equippedList)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Create);

        PlayerData data = new PlayerData(inventory, equippedList);

        bf.Serialize(file, data);
        file.Close();
    }

    public static Dictionary<string, List<string>> Load()
    {
        Dictionary<string, List<string>> playerDataDict = new Dictionary<string, List<string>>();

        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            playerDataDict.Add("playerInventory", data.inventory);
            playerDataDict.Add("equippedInventory", data.equippedArmor);

            return playerDataDict;
        }
        else
        {
            Debug.Log("No file to return");
            return new Dictionary<string, List<string>>();
        }
    }
}

[Serializable]
class PlayerData
{
    public List<string> inventory;
    public List<string> equippedArmor;
    // public string type;

    public PlayerData(List<ItemSlot> oldInventory, List<ItemSlot> equippedList)
    {
        this.inventory = new List<string>();
        this.equippedArmor = new List<string>();
        // Debug.Log(oldInventory[0].slotStatus);
        // type = inventory[0].type;
        foreach (ItemSlot item in oldInventory.ToArray())
        {
            this.inventory.Add(item.itemName);
        }
        foreach (ItemSlot item in equippedList.ToArray())
        {
            this.equippedArmor.Add(item.itemName);
        }
    }

}



