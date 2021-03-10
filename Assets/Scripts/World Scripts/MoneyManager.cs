using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text moneyText;
    public int currentGold;
    private PlayerHealthManager playerHealth;

    // Use this for initialization
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealthManager>();

        if (PlayerPrefs.HasKey("Current Money"))
        {
            currentGold = PlayerPrefs.GetInt("Currenty Money");

        }
        else
        {
            currentGold = 0;
            PlayerPrefs.SetInt("Current Money", 0);
        }

        moneyText.text = "Gold: " + currentGold;
    }

    public void AddMoney(int goldToAdd)
    {
        playerHealth.playerCurrentHealth += 3;
        //currentGold += goldToAdd;
        PlayerPrefs.SetInt("CurrentMoney", currentGold);
        moneyText.text = "Gold: " + currentGold;
    }
}