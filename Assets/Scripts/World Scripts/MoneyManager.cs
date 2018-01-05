using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

    public Text moneyText;
    public int currentGold;

	// Use this for initialization
	void Start () {

        if (PlayerPrefs.HasKey("Current Money"))
        {
            currentGold = PlayerPrefs.GetInt("Currenty Money");

        }else
        {
            currentGold = 0;
            PlayerPrefs.SetInt("Current Money", 0);
        }

        moneyText.text = "Gold: " + currentGold;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddMoney(int goldToAdd)
    {
        currentGold += goldToAdd;
        PlayerPrefs.SetInt("CurrentMoney", currentGold);
        moneyText.text = "Gold: " + currentGold;
    }
}
