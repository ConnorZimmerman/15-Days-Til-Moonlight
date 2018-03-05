using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	private PlayerStats playerStats;
	public GameObject pauseMenu;
	public bool pauseStatus;
	public Text menuText;
	public Text toSpendText;

	// Use this for initialization
	void Start () {
		playerStats = FindObjectOfType<PlayerStats>();
		pauseMenu = GameObject.Find("LvlUpMenu");
		pauseMenu.SetActive(false);
		pauseStatus = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Pause") && !pauseStatus){
			pauseMenu.SetActive(true);
			pauseStatus = true;
		}
		else if(Input.GetButtonDown("Pause") && pauseStatus){
			pauseMenu.SetActive(false);
			pauseStatus = false;
		}

		if(pauseStatus){

		}

		menuText.text = string.Format("Current Level: {0}\nExperience: {1} / {2}\n\nVitality: {3}\nStrength: {4}\nDexterity: {5}\nIntelligence: {6}", playerStats.currentLevel, playerStats.currentExp, playerStats.toLevelUp[playerStats.currentLevel], playerStats.vitality, playerStats.strength, playerStats.dexterity, playerStats.intelligence);

		toSpendText.text = string.Format("Points to spend: {0}", 0);

	}

}



