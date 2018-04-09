using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    private PlayerStats playerStats;
    private PauseMenuButtons pauseMenuButtonsScript;
    public GameObject pauseMenu;
    public GameObject lvlUpPanel;
    private GameObject inventoryPanel;
    public bool pauseStatus;
	public bool lvlUpPanelStatus;
	private bool inventoryPanelStatus;
    public Text menuText;
    public Text toSpendText;

    // Use this for initialization
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        pauseMenuButtonsScript = FindObjectOfType<PauseMenuButtons>();
        pauseMenu = GameObject.Find("LvlUpMenu");
        lvlUpPanel = GameObject.Find("LvlUpPanel");
        inventoryPanel = GameObject.Find("InventoryPanel");
        pauseMenu.SetActive(false);
        inventoryPanel.SetActive(false);
        // lvlUpPanel.SetActive(false);
        pauseStatus = false;
		lvlUpPanelStatus = true;
		inventoryPanelStatus = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && !pauseStatus)
        {
            pauseMenu.SetActive(true);
            pauseStatus = true;
        }
        else if (Input.GetButtonDown("Pause") && pauseStatus)
        {
            pauseMenu.SetActive(false);
            pauseStatus = false;
        }

        if (pauseStatus && lvlUpPanelStatus && Input.GetButtonDown("RSwitch") || pauseStatus && lvlUpPanelStatus && Input.GetButtonDown("LSwitch"))
        {
            lvlUpPanel.SetActive(false);
			lvlUpPanelStatus = false;
            inventoryPanel.SetActive(true);
			inventoryPanelStatus = true;
            pauseMenuButtonsScript.justSwitched = true;
        }
		else if(pauseStatus && inventoryPanelStatus && Input.GetButtonDown("RSwitch") || pauseStatus && inventoryPanelStatus && Input.GetButtonDown("LSwitch")){
			inventoryPanel.SetActive(false);
			inventoryPanelStatus = false;
			lvlUpPanel.SetActive(true);
			lvlUpPanelStatus = true;
            pauseMenuButtonsScript.justSwitched = true;
		}

        menuText.text = string.Format("Current Level: {0}\nExperience: {1} / {2}\n\nVitality: {3}\nStrength: {4}\nDexterity: {5}\nIntelligence: {6}", playerStats.currentLevel, playerStats.currentExp, playerStats.toLevelUp[playerStats.currentLevel], playerStats.vitality, playerStats.strength, playerStats.dexterity, playerStats.intelligence);

        toSpendText.text = string.Format("Points to spend: {0}", playerStats.pointsToSpend);

    }

}



