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
    private PlayerStaminaManager playerStaminaScript;
    public GameObject lastSelected;
    private PauseMenu pauseMenuScript;
    private ItemSlotManager itemSlotManagerScript;

    // Lvl Up Buttons
    private GameObject VitalityButton;
    public GameObject vitalityObject;
    public Text vitalityText;

    private GameObject StrengthButton;
    public GameObject strengthObject;
    public Text strengthText;

    private GameObject DexterityButton;
    public GameObject dexterityObject;
    public Text dexterityText;

    private GameObject IntelligenceButton;
    public GameObject intelligenceObject;
    public Text intelligenceText;

    // Inventory Buttons
    private GameObject ItemSlot0AButton;
    private GameObject ItemSlot0BButton;
    private GameObject ItemSlot0CButton;
    private GameObject ItemSlot0DButton;
    private GameObject ItemSlot0EButton;
    private GameObject ItemSlot1AButton;
    private GameObject ItemSlot1BButton;
    private GameObject ItemSlot1CButton;
    private GameObject ItemSlot1DButton;
    private GameObject ItemSlot1EButton;
    private GameObject ItemSlot2AButton;
    private GameObject ItemSlot2BButton;
    private GameObject ItemSlot2CButton;
    private GameObject ItemSlot2DButton;
    private GameObject ItemSlot2EButton;
    private GameObject ItemSlot3AButton;
    private GameObject ItemSlot3BButton;
    private GameObject ItemSlot3CButton;
    private GameObject ItemSlot3DButton;
    private GameObject ItemSlot3EButton;

    // Armor slots
    private GameObject HeadSlotButton;
    private GameObject BodyArmorButton;
    private GameObject GlovesButton;
    private GameObject BootsButton;
    private GameObject RingOneButton;
    private GameObject RingTwoButton;
    private GameObject RingThreeButton;
    private GameObject RingFourButton;
    public bool justSwitched;
    public GameObject currentSelectedGameObject;
    public Text descriptionText;

    // Use this for initialization
    void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        pauseMenuScript = FindObjectOfType<PauseMenu>();
        playerHealthScript = FindObjectOfType<PlayerHealthManager>();
        playerStaminaScript = FindObjectOfType<PlayerStaminaManager>();
        itemSlotManagerScript = FindObjectOfType<ItemSlotManager>();

        VitalityButton = GameObject.Find("VitalityButton");
        StrengthButton = GameObject.Find("StrengthButton");
        DexterityButton = GameObject.Find("DexterityButton");
        IntelligenceButton = GameObject.Find("IntelligenceButton");

        ItemSlot0AButton = GameObject.Find("ItemSlot0AButton");
        ItemSlot0BButton = GameObject.Find("ItemSlot0BButton");
        ItemSlot0CButton = GameObject.Find("ItemSlot0CButton");
        ItemSlot0DButton = GameObject.Find("ItemSlot0DButton");
        ItemSlot0EButton = GameObject.Find("ItemSlot0EButton");
        ItemSlot1AButton = GameObject.Find("ItemSlot1AButton");
        ItemSlot1BButton = GameObject.Find("ItemSlot1BButton");
        ItemSlot1CButton = GameObject.Find("ItemSlot1CButton");
        ItemSlot1DButton = GameObject.Find("ItemSlot1DButton");
        ItemSlot1EButton = GameObject.Find("ItemSlot1EButton");
        ItemSlot2AButton = GameObject.Find("ItemSlot2AButton");
        ItemSlot2BButton = GameObject.Find("ItemSlot2BButton");
        ItemSlot2CButton = GameObject.Find("ItemSlot2CButton");
        ItemSlot2DButton = GameObject.Find("ItemSlot2DButton");
        ItemSlot2EButton = GameObject.Find("ItemSlot2EButton");
        ItemSlot3AButton = GameObject.Find("ItemSlot3AButton");
        ItemSlot3BButton = GameObject.Find("ItemSlot3BButton");
        ItemSlot3CButton = GameObject.Find("ItemSlot3CButton");
        ItemSlot3DButton = GameObject.Find("ItemSlot3DButton");
        ItemSlot3EButton = GameObject.Find("ItemSlot3EButton");

        HeadSlotButton = GameObject.Find("HeadButton");
        BodyArmorButton = GameObject.Find("BodyArmorButton");
        GlovesButton = GameObject.Find("GlovesButton");
        BootsButton = GameObject.Find("BootsButton");
        RingOneButton = GameObject.Find("RingOneButton");
        RingTwoButton = GameObject.Find("RingTwoButton");
        RingThreeButton = GameObject.Find("RingThreeButton");
        RingFourButton = GameObject.Find("RingFourButton");

        descriptionText = GameObject.Find("DescriptionText").GetComponent<Text>();

        justSwitched = true;
    }

    // Update is called once per frame
    public void Update()
    {
        currentSelectedGameObject = eventSystem.currentSelectedGameObject;
        if (pauseMenuScript.pauseStatus)
        {
            if (eventSystem.currentSelectedGameObject == VitalityButton ||
            eventSystem.currentSelectedGameObject == StrengthButton ||
            eventSystem.currentSelectedGameObject == DexterityButton ||
            eventSystem.currentSelectedGameObject == IntelligenceButton ||
            eventSystem.currentSelectedGameObject == ItemSlot0AButton ||
            eventSystem.currentSelectedGameObject == ItemSlot0BButton ||
            eventSystem.currentSelectedGameObject == ItemSlot0CButton ||
            eventSystem.currentSelectedGameObject == ItemSlot0DButton ||
            eventSystem.currentSelectedGameObject == ItemSlot0EButton ||
            eventSystem.currentSelectedGameObject == ItemSlot1AButton ||
            eventSystem.currentSelectedGameObject == ItemSlot1BButton ||
            eventSystem.currentSelectedGameObject == ItemSlot1CButton ||
            eventSystem.currentSelectedGameObject == ItemSlot1DButton ||
            eventSystem.currentSelectedGameObject == ItemSlot1EButton ||
            eventSystem.currentSelectedGameObject == ItemSlot2AButton ||
            eventSystem.currentSelectedGameObject == ItemSlot2BButton ||
            eventSystem.currentSelectedGameObject == ItemSlot2CButton ||
            eventSystem.currentSelectedGameObject == ItemSlot2DButton ||
            eventSystem.currentSelectedGameObject == ItemSlot2EButton ||
            eventSystem.currentSelectedGameObject == ItemSlot3AButton ||
            eventSystem.currentSelectedGameObject == ItemSlot3BButton ||
            eventSystem.currentSelectedGameObject == ItemSlot3CButton ||
            eventSystem.currentSelectedGameObject == ItemSlot3DButton ||
            eventSystem.currentSelectedGameObject == ItemSlot3EButton ||
            eventSystem.currentSelectedGameObject == HeadSlotButton ||
            eventSystem.currentSelectedGameObject == BodyArmorButton ||
            eventSystem.currentSelectedGameObject == GlovesButton ||
            eventSystem.currentSelectedGameObject == BootsButton ||
            eventSystem.currentSelectedGameObject == RingOneButton ||
            eventSystem.currentSelectedGameObject == RingTwoButton ||
            eventSystem.currentSelectedGameObject == RingThreeButton ||
            eventSystem.currentSelectedGameObject == RingFourButton)
            {
                lastSelected = eventSystem.currentSelectedGameObject;
            }

            if (eventSystem.currentSelectedGameObject != VitalityButton && eventSystem.currentSelectedGameObject != StrengthButton && eventSystem.currentSelectedGameObject != DexterityButton && eventSystem.currentSelectedGameObject != IntelligenceButton ||
            eventSystem.currentSelectedGameObject != ItemSlot0AButton ||
            eventSystem.currentSelectedGameObject != ItemSlot0BButton ||
            eventSystem.currentSelectedGameObject != ItemSlot0CButton ||
            eventSystem.currentSelectedGameObject != ItemSlot0DButton ||
            eventSystem.currentSelectedGameObject != ItemSlot0EButton ||
            eventSystem.currentSelectedGameObject != ItemSlot1AButton ||
            eventSystem.currentSelectedGameObject != ItemSlot1BButton ||
            eventSystem.currentSelectedGameObject != ItemSlot1CButton ||
            eventSystem.currentSelectedGameObject != ItemSlot1DButton ||
            eventSystem.currentSelectedGameObject != ItemSlot1EButton ||
            eventSystem.currentSelectedGameObject != ItemSlot2AButton ||
            eventSystem.currentSelectedGameObject != ItemSlot2BButton ||
            eventSystem.currentSelectedGameObject != ItemSlot2CButton ||
            eventSystem.currentSelectedGameObject != ItemSlot2DButton ||
            eventSystem.currentSelectedGameObject != ItemSlot2EButton ||
            eventSystem.currentSelectedGameObject != ItemSlot3AButton ||
            eventSystem.currentSelectedGameObject != ItemSlot3BButton ||
            eventSystem.currentSelectedGameObject != ItemSlot3CButton ||
            eventSystem.currentSelectedGameObject != ItemSlot3DButton ||
            eventSystem.currentSelectedGameObject != ItemSlot3EButton ||
            eventSystem.currentSelectedGameObject != HeadSlotButton ||
            eventSystem.currentSelectedGameObject != BodyArmorButton ||
            eventSystem.currentSelectedGameObject != GlovesButton ||
            eventSystem.currentSelectedGameObject != BootsButton ||
            eventSystem.currentSelectedGameObject != RingOneButton ||
            eventSystem.currentSelectedGameObject != RingTwoButton ||
            eventSystem.currentSelectedGameObject != RingThreeButton ||
            eventSystem.currentSelectedGameObject != RingFourButton)
            {
                eventSystem.SetSelectedGameObject(lastSelected);
            }

            if (pauseMenuScript.lvlUpPanelStatus)
            {
                if (justSwitched)
                {
                    eventSystem.SetSelectedGameObject(GameObject.Find("VitalityButton"));
                }
                justSwitched = false;
                if (eventSystem.currentSelectedGameObject == VitalityButton || eventSystem.currentSelectedGameObject == StrengthButton || eventSystem.currentSelectedGameObject == DexterityButton || eventSystem.currentSelectedGameObject == IntelligenceButton || eventSystem.currentSelectedGameObject == ItemSlot0AButton)
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
                // Ensures that the last button cannot be scrolled off from
                if (eventSystem.currentSelectedGameObject != VitalityButton && eventSystem.currentSelectedGameObject != StrengthButton && eventSystem.currentSelectedGameObject != DexterityButton && eventSystem.currentSelectedGameObject != IntelligenceButton || eventSystem.currentSelectedGameObject != ItemSlot0AButton)
                {
                    eventSystem.SetSelectedGameObject(lastSelected);
                }
            }
            else if (pauseMenuScript.inventoryPanelStatus)
            {
                if (justSwitched)
                {
                    eventSystem.SetSelectedGameObject(GameObject.Find("ItemSlot0AButton"));
                }
                justSwitched = false;

                int itemNumber = MappingButtonNums(lastSelected.name);
                if (itemNumber < 20)
                {
                    descriptionText.text = itemSlotManagerScript.listOfSlots[itemNumber].itemDescription;
                    // Debug.Log("getting here");
                    // Debug.Log(itemSlotManagerScript.listOfSlots[itemNumber].itemDescription);
                    // Debug.Log(itemNumber);
                    // Debug.Log(itemNumber);
                    // descriptionText.text = itemSlotManagerScript.listOfSlots[itemNumber].itemDescription;
                }
                else
                {
                    descriptionText.text = itemSlotManagerScript.equippedArmor[itemNumber- 20].itemDescription;
                }
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
            playerStaminaScript.playerCurrentStamina += 50;
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
    public void ItemSlot0A()
    {
        // playerStats.vitality++;
        // playerHealthScript.playerCurrentHealth++;
    }

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
            dexterityText = "- +50 stamina";
            return dexterityText;
        }
        else if (dexterity == 15)
        {
            dexterityText = "- -10 stamina used to dash\n- +50 stamina";
            return dexterityText;
        }
        else if (dexterity == 16)
        {
            dexterityText = "- Teleport: press B to teleport\n- +50 stamina";
            return dexterityText;
        }
        else if (dexterity == 17)
        {
            dexterityText = "- -10 stamina used to dash\n- +50 stamina";
            return dexterityText;
        }
        else if (dexterity == 18)
        {
            dexterityText = "- +50 stamina";
            return dexterityText;
        }
        else if (dexterity == 19)
        {
            dexterityText = "- -10 stamina used to dash\n- +50 stamina";
            return dexterityText;
        }
        return "None";
    }
    // public string IntelligenceText(int intelligence)
    // {
    // }

    public int MappingButtonNums(string lastSelected)
    {
        int numToReturn = 0;
        if (lastSelected == "ItemSlot0AButton")
        {
            numToReturn = 0;
        }
        else if (lastSelected == "ItemSlot0BButton")
        {
            numToReturn = 1;
        }
        else if (lastSelected == "ItemSlot0CButton")
        {
            numToReturn = 2;
        }
        else if (lastSelected == "ItemSlot0DButton")
        {
            numToReturn = 3;
        }
        else if (lastSelected == "ItemSlot0EButton")
        {
            numToReturn = 4;
        }
        else if (lastSelected == "ItemSlot1AButton")
        {
            numToReturn = 5;
        }
        else if (lastSelected == "ItemSlot1BButton")
        {
            numToReturn = 6;
        }
        else if (lastSelected == "ItemSlot1CButton")
        {
            numToReturn = 7;
        }
        else if (lastSelected == "ItemSlot1DButton")
        {
            numToReturn = 8;
        }
        else if (lastSelected == "ItemSlot1EButton")
        {
            numToReturn = 9;
        }
        else if (lastSelected == "ItemSlot2AButton")
        {
            numToReturn = 10;
        }
        else if (lastSelected == "ItemSlot2BButton")
        {
            numToReturn = 11;
        }
        else if (lastSelected == "ItemSlot2CButton")
        {
            numToReturn = 12;
        }
        else if (lastSelected == "ItemSlot2DButton")
        {
            numToReturn = 13;
        }
        else if (lastSelected == "ItemSlot2EButton")
        {
            numToReturn = 14;
        }
        else if (lastSelected == "ItemSlot3AButton")
        {
            numToReturn = 15;
        }
        else if (lastSelected == "ItemSlot3BButton")
        {
            numToReturn = 16;
        }
        else if (lastSelected == "ItemSlot3CButton")
        {
            numToReturn = 17;
        }
        else if (lastSelected == "ItemSlot3DButton")
        {
            numToReturn = 18;
        }
        else if (lastSelected == "ItemSlot3EButton")
        {
            numToReturn = 19;
        }
        else if (lastSelected == "HeadButton")
        {
            numToReturn = 20;
        }
        else if (lastSelected == "BodyArmorButton")
        {
            numToReturn = 21;
        }
        else if (lastSelected == "GlovesButton")
        {
            numToReturn = 22;
        }
        else if (lastSelected == "BootButton")
        {
            numToReturn = 23;
        }
        else if (lastSelected == "RingOneButton")
        {
            numToReturn = 24;
        }
        else if (lastSelected == "RingTwoButton")
        {
            numToReturn = 25;
        }
        else if (lastSelected == "RingThreeButton")
        {
            numToReturn = 26;
        }
        else if (lastSelected == "RingFourButton")
        {
            numToReturn = 27;
        }
        return numToReturn;
    }

    // public int MapEquippedItemButtons(string lastSelected){
    //     int numToReturn = 0;
    //     return numToReturn;        
    // }
}
