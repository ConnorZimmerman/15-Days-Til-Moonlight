using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenuButtons : MonoBehaviour
{
    public EventSystem eventSystem;
    private PlayerStats playerStats;
    private PlayerHealthManager playerHealthScript;
    private GameObject lastSelected;
    private PauseMenu pauseMenuScript;
    private GameObject VitalityButton;
    private GameObject StrengthButton;
    private GameObject DexterityButton;
    private GameObject IntelligenceButton;
    public GameObject vitalityObject;
    public Text vitalityText;
    public GameObject strengthObject;
    public Text strengthText;
    public GameObject dexterityObject;
    public Text dexterityText;
    public GameObject intelligenceObject;
    public Text intelligenceText;

    // Use this for initialization
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        pauseMenuScript = FindObjectOfType<PauseMenu>();
        playerHealthScript = FindObjectOfType<PlayerHealthManager>();
        VitalityButton = GameObject.Find("VitalityButton");
        StrengthButton = GameObject.Find("StrengthButton");
        DexterityButton = GameObject.Find("DexterityButton");
        IntelligenceButton = GameObject.Find("IntelligenceButton");
    }

    // Update is called once per frame
    public void Update()
    {

        if (pauseMenuScript.pauseStatus)
        {
            if (eventSystem.currentSelectedGameObject == VitalityButton || eventSystem.currentSelectedGameObject == StrengthButton || eventSystem.currentSelectedGameObject == DexterityButton || eventSystem.currentSelectedGameObject == IntelligenceButton)
            {
                lastSelected = eventSystem.currentSelectedGameObject;
            }
            if (eventSystem.currentSelectedGameObject == VitalityButton)
            {
                vitalityObject.SetActive(true);
                vitalityText.text = "Will increase max health by 1";
            }
            else
            {
                vitalityObject.SetActive(false);
            }
            if (eventSystem.currentSelectedGameObject == StrengthButton)
            {
                strengthObject.SetActive(true);
                strengthText.text = "Will decrease stamina loss by 5%";
            }
            else
            {
                strengthObject.SetActive(false);
            }
            if (eventSystem.currentSelectedGameObject == DexterityButton)
            {
                dexterityObject.SetActive(true);
                dexterityText.text = "Will increase max stamina by 10%";
            }
            else
            {
                dexterityObject.SetActive(false);
            }
            if (eventSystem.currentSelectedGameObject == IntelligenceButton)
            {
                intelligenceObject.SetActive(true);
                intelligenceText.text = "Will increase drop rate probability by 1%";
            }
            else
            {
                intelligenceObject.SetActive(false);
            }
            if (eventSystem.currentSelectedGameObject != VitalityButton && eventSystem.currentSelectedGameObject != StrengthButton && eventSystem.currentSelectedGameObject != DexterityButton && eventSystem.currentSelectedGameObject != IntelligenceButton)
            {
                eventSystem.SetSelectedGameObject(lastSelected);
            }
        }
    }
    public void Vitality()
    {
        if (playerStats.pointsToSpend > 0)
        {
            playerStats.vitality++;
            playerHealthScript.playerCurrentHealth++;
            playerStats.pointsToSpend--;
        }
    }
    public void Strength()
    {
        if (playerStats.pointsToSpend > 0)
        {
            playerStats.strength++;
            playerStats.pointsToSpend--;
        }
    }
    public void Dexterity()
    {
        if (playerStats.pointsToSpend > 0)
        {
            playerStats.dexterity++;
            playerStats.pointsToSpend--;
        }

    }
    public void Intelligence()
    {
        if (playerStats.pointsToSpend > 0)
        {
            playerStats.intelligence++;
            playerStats.pointsToSpend--;
        }
    }
}
