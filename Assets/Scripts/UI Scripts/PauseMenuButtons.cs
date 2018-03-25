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
                strengthText.text = StrengthText(playerStats.strength);
            }
            else
            {
                strengthObject.SetActive(false);
            }
            if (eventSystem.currentSelectedGameObject == DexterityButton)
            {
                dexterityObject.SetActive(true);
                dexterityText.text = DexterityText(playerStats.dexterity);
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

    // public string VitalityText(int vitality){

    // }
    public string StrengthText(int strength)
    {
        string strengthText;
        if (strength == 3)
        {
            strengthText = "- 30 less stamina lost on block";
            return strengthText;
        }
        else if (strength == 4)
        {
            strengthText = "- It will take 2 damage to unshield you!\n- 30 less stamina lost on block";
            return strengthText;
        }
        else if (strength == 5)
        {
            strengthText = "- 30 less stamina lost on block";
            return strengthText;
        }
        else if (strength == 6)
        {
            strengthText = "- +50% stamina regained for each kill!\n- It will take 3 damage to unshield you!\n30 less stamina lost on block";
            return strengthText;
        }
        else if (strength == 7)
        {
            strengthText = "- 30 less stamina lost on block";
            return strengthText;
        }
        else if (strength == 8)
        {
            strengthText = "- Dash Strike: hold A and attack to +1 damage! (Requires dash)\n- 30 less stamina lost on block";
            return strengthText;
        }
        else if (strength == 9)
        {
            strengthText = "- It will take 4 damage to unshield you!\n- 30 less stamina lost on block";
            return strengthText;
        }
        else if (strength == 10)
        {
            strengthText = "- Rage: +1 to damage!\n- 30 less stamina lost on block";
            return strengthText;
        }
        return "None";
    }
    public string DexterityText(int dexterity)
    {
        string dexterityText;
        if (dexterity == 10)
        {
            dexterityText = "- +50 stamina";
            return dexterityText;
        }
        else if (dexterity == 11)
        {
            dexterityText = "- Can throw daggers!\n- +50 stamina";
            return dexterityText;
        }
        else if (dexterity == 12)
        {
            dexterityText = "- +50 stamina";
            return dexterityText;
        }
        else if (dexterity == 13)
        {
            dexterityText = "- Dash: press A to dash\n- +50 stamina";
            return dexterityText;
        }
        else if (dexterity == 14)
        {
            dexterityText = "- -10 stamina used to dash\n- +50 stamina";
            return dexterityText;
        }
        else if (dexterity == 15)
        {
            dexterityText = "- -10 stamina used to dash\n- +50 stamina";
            return dexterityText;
        }
        else if (dexterity == 16)
        {
            dexterityText = "- Teleport: press B to teleport\n- 30 less stamina lost on block";
            return dexterityText;
        }
        else if (dexterity == 17)
        {
            dexterityText = "- -10 stamina used to dash\n- +50 stamina";
            return dexterityText;
        }
        return "None";
    }
    // public string IntelligenceText(int intelligence)
    // {

    // }
}
