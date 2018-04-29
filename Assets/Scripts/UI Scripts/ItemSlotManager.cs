using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotManager : MonoBehaviour
{
    private GlobalDataScript globalDataScript;
    private PauseMenuButtons pauseMenuButtonsScript;
    private PlayerStats playerStatScript;
    public List<ItemSlot> listOfSlots;
    public GameObject itemSection;
    public List<ItemSlot> equippedArmor;
    public GameObject equippedSection;
    public int slotIndex = 0;
    public int rowIndex = 1;
    public int counter = 0;
    public static int potionCount;

    // Use this for initialization
    void Awake()
    {
        globalDataScript = FindObjectOfType<GlobalDataScript>();
        pauseMenuButtonsScript = FindObjectOfType<PauseMenuButtons>();
        playerStatScript = FindObjectOfType<PlayerStats>();

        Dictionary<string, List<string>> playerDataDict = GlobalDataScript.Load();

        for (int i = 0; i < 20; i++)
        {
            listOfSlots.Add(new ItemSlot());
        }

        itemSection = GameObject.Find("ItemSection");

        if (playerDataDict["playerInventory"].Count > 0)
        {
            foreach (Transform inventoryRow in itemSection.transform)
            {
                foreach (Transform itemSlot in inventoryRow.transform)
                {
                    itemSlot.GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find(PopulateItemSlot(playerDataDict["playerInventory"][counter], counter)).GetComponent<Image>().sprite;

                    counter++;
                }
            }
        }
        else
        {
            foreach (Transform inventoryRow in itemSection.transform)
            {
                foreach (Transform itemSlot in inventoryRow.transform)
                {
                    itemSlot.GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;
                }
            }
        }

        for (int i = 0; i < 8; i++)
        {
            equippedArmor.Add(new ItemSlot());
        }

        equippedSection = GameObject.Find("EquippedSection");

        counter = 0;

        foreach (Transform itemSlot in equippedSection.transform)
        {
            itemSlot.GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find(PopulateEquippedSlot(playerDataDict["equippedInventory"][counter], counter)).GetComponent<Image>().sprite;
            counter++;
        }
    }

    public void ItemPickUp(string itemName)
    {
        if (itemName != "potion")
        {
            rowIndex = 0;
            for (int i = 0; i < listOfSlots.Count; i++)
            {
                if (i >= 5 && i < 10)
                {
                    rowIndex = 1;
                }
                else if (i >= 10 && i < 15)
                {
                    rowIndex = 2;
                }
                else if (i >= 15)
                {
                    rowIndex = 3;
                }

                slotIndex = i;
                if (listOfSlots[i].slotStatus == "open")
                {
                    listOfSlots[i].itemName = itemName;
                    PopulateItemSlot(itemName, i);
                    break;
                }
            }
        }

        slotIndex -= (rowIndex * 5);

        if (itemName == "potion")
        {
            potionCount++;
        }
        else
        {
            itemSection.transform.GetChild(rowIndex).GetChild(slotIndex).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find(itemName + "Image").GetComponent<Image>().sprite;
        }
    }

    public void ItemSelect()
    {
        int itemNumber = pauseMenuButtonsScript.MappingButtonNums(pauseMenuButtonsScript.lastSelected.name);

        if (itemNumber < 20)
        {
            if (listOfSlots[itemNumber].itemName == "simpleHelmet")
            {
                equippedSection = GameObject.Find("EquippedSection");

                equippedSection.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("simpleHelmetImage").GetComponent<Image>().sprite;

                PopulateEquippedSlot(listOfSlots[itemNumber].itemName, 0);
                ClearInventorySpot(itemNumber);
            }
            else if (listOfSlots[itemNumber].itemName == "simpleGloves")
            {
                equippedSection = GameObject.Find("EquippedSection");

                equippedSection.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("simpleGlovesImage").GetComponent<Image>().sprite;

                PopulateEquippedSlot(listOfSlots[itemNumber].itemName, 2);
                ClearInventorySpot(itemNumber);
            }
            else if (listOfSlots[itemNumber].itemName == "simpleBoots")
            {
                equippedSection = GameObject.Find("EquippedSection");

                equippedSection.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("simpleBootsImage").GetComponent<Image>().sprite;

                PopulateEquippedSlot(listOfSlots[itemNumber].itemName, 3);
                ClearInventorySpot(itemNumber);
            }
            else if (listOfSlots[itemNumber].itemName == "simpleChest")
            {
                equippedSection = GameObject.Find("EquippedSection");

                equippedSection.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("simpleChestImage").GetComponent<Image>().sprite;

                PopulateEquippedSlot(listOfSlots[itemNumber].itemName, 1);
                ClearInventorySpot(itemNumber);
            }
            else if (listOfSlots[itemNumber].itemName == "ringOfRoses")
            {
                equippedSection = GameObject.Find("EquippedSection");

                equippedSection.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("ringOfRosesImage").GetComponent<Image>().sprite;

                PopulateEquippedSlot(listOfSlots[itemNumber].itemName, 4);
                ClearInventorySpot(itemNumber);
            }
            else if (listOfSlots[itemNumber].itemName == "ringOfEarth")
            {
                equippedSection = GameObject.Find("EquippedSection");

                equippedSection.transform.GetChild(5).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("ringOfEarthImage").GetComponent<Image>().sprite;

                PopulateEquippedSlot(listOfSlots[itemNumber].itemName, 5);
                ClearInventorySpot(itemNumber);
            }
            else if (listOfSlots[itemNumber].itemName == "ringOfKnowledge")
            {
                equippedSection = GameObject.Find("EquippedSection");

                equippedSection.transform.GetChild(6).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("ringOfKnowledgeImage").GetComponent<Image>().sprite;

                PopulateEquippedSlot(listOfSlots[itemNumber].itemName, 6);
                ClearInventorySpot(itemNumber);
            }
            else if (listOfSlots[itemNumber].itemName == "ringOfTheBull")
            {
                equippedSection = GameObject.Find("EquippedSection");

                equippedSection.transform.GetChild(7).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("ringOfTheBullImage").GetComponent<Image>().sprite;

                PopulateEquippedSlot(listOfSlots[itemNumber].itemName, 7);
                ClearInventorySpot(itemNumber);
            }
        }
        if (itemNumber > 19)
        {
            equippedSection = GameObject.Find("EquippedSection");

            if (itemNumber == 20)
            {
                if (equippedArmor[0].itemName == "simpleHelmet")
                {
                    ItemPickUp("simpleHelmet");
                }

                equippedArmor[0].itemName = "";
                equippedArmor[0].slotStatus = "open";
                equippedArmor[0].itemDescription = "";
                equippedArmor[0].defenseAmount = 0;

                equippedSection.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;
            }
            else if (itemNumber == 22)
            {
                if (equippedArmor[2].itemName == "simpleGloves")
                {
                    ItemPickUp("simpleGloves");
                }

                equippedArmor[2].itemName = "";
                equippedArmor[2].slotStatus = "open";
                equippedArmor[2].itemDescription = "";
                equippedArmor[2].defenseAmount = 0;

                equippedSection.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;
            }
            else if (itemNumber == 23)
            {
                if (equippedArmor[3].itemName == "simpleBoots")
                {
                    ItemPickUp("simpleBoots");
                }

                equippedArmor[3].itemName = "";
                equippedArmor[3].slotStatus = "open";
                equippedArmor[3].itemDescription = "";
                equippedArmor[3].defenseAmount = 0;

                equippedSection.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;
            }
            else if (itemNumber == 21)
            {
                if (equippedArmor[1].itemName == "simpleChest")
                {
                    ItemPickUp("simpleChest");
                }

                equippedArmor[1].itemName = "";
                equippedArmor[1].slotStatus = "open";
                equippedArmor[1].itemDescription = "";
                equippedArmor[1].defenseAmount = 0;

                equippedSection.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;
            }
            else if (itemNumber == 24)
            {
                if (equippedArmor[4].itemName == "ringOfRoses")
                {
                    ItemPickUp("ringOfRoses");
                }

                equippedArmor[4].itemName = "";
                equippedArmor[4].slotStatus = "open";
                equippedArmor[4].itemDescription = "";
                equippedArmor[4].defenseAmount = 0;
                playerStatScript.vitality -= 1;

                equippedSection.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;
            }
            else if (itemNumber == 25)
            {
                if (equippedArmor[5].itemName == "ringOfEarth")
                {
                    ItemPickUp("ringOfEarth");
                }

                equippedArmor[5].itemName = "";
                equippedArmor[5].slotStatus = "open";
                equippedArmor[5].itemDescription = "";
                equippedArmor[5].defenseAmount = 0;
                playerStatScript.dexterity -= 1;

                equippedSection.transform.GetChild(5).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;
            }
            else if (itemNumber == 26)
            {
                if (equippedArmor[6].itemName == "ringOfKnowledge")
                {
                    ItemPickUp("ringOfKnowledge");
                }

                equippedArmor[6].itemName = "";
                equippedArmor[6].slotStatus = "open";
                equippedArmor[6].itemDescription = "";
                equippedArmor[6].defenseAmount = 0;
                playerStatScript.intelligence -= 1;

                equippedSection.transform.GetChild(6).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;
            }
            else if (itemNumber == 27)
            {
                if (equippedArmor[7].itemName == "ringOfTheBull")
                {
                    ItemPickUp("ringOfTheBull");
                }

                equippedArmor[7].itemName = "";
                equippedArmor[7].slotStatus = "open";
                equippedArmor[7].itemDescription = "";
                equippedArmor[7].defenseAmount = 0;
                playerStatScript.strength -= 1;

                equippedSection.transform.GetChild(7).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;
            }
        }
    }

    public string PopulateItemSlot(string itemName, int counter = 0)
    {
        string imageName = "Image";
        if (itemName == "daggers")
        {
            listOfSlots[counter].slotStatus = "closed";
            listOfSlots[counter].itemName = "daggers";
            listOfSlots[counter].itemDescription = "Daggers can be thrown at enemies by clicking x.";
            imageName = "daggersImage";
        }
        else if (itemName == "potion")
        {
            listOfSlots[counter].slotStatus = "closed";
            listOfSlots[counter].itemName = "potion";
            listOfSlots[counter].itemDescription = "Potions can heal you for 2-4 health.";
            imageName = "potionImage";
        }
        else if (itemName == "simpleHelmet")
        {
            listOfSlots[counter].slotStatus = "closed";
            listOfSlots[counter].itemName = "simpleHelmet";
            listOfSlots[counter].itemDescription = "+2 to defense";
            imageName = "simpleHelmetImage";
        }
        else if (itemName == "simpleGloves")
        {
            listOfSlots[counter].slotStatus = "closed";
            listOfSlots[counter].itemName = "simpleGloves";
            listOfSlots[counter].itemDescription = "+1 to defense";
            imageName = "simpleGlovesImage";
        }
        else if (itemName == "simpleBoots")
        {
            listOfSlots[counter].slotStatus = "closed";
            listOfSlots[counter].itemName = "simpleBoots";
            listOfSlots[counter].itemDescription = "+1 to defense";
            imageName = "simpleBootsImage";
        }
        else if (itemName == "simpleChest")
        {
            listOfSlots[counter].slotStatus = "closed";
            listOfSlots[counter].itemName = "simpleChest";
            listOfSlots[counter].itemDescription = "+4 to defense";
            imageName = "simpleChestImage";
        }
        else if (itemName == "ringOfRoses")
        {
            listOfSlots[counter].slotStatus = "closed";
            listOfSlots[counter].itemName = "ringOfRoses";
            listOfSlots[counter].itemDescription = "+1 to vitality";
            imageName = "ringOfRosesImage";
        }
        else if (itemName == "ringOfEarth")
        {
            listOfSlots[counter].slotStatus = "closed";
            listOfSlots[counter].itemName = "ringOfEarth";
            listOfSlots[counter].itemDescription = "+1 to dexterity";
            imageName = "ringOfEarthImage";
        }
        else if (itemName == "ringOfKnowledge")
        {
            listOfSlots[counter].slotStatus = "closed";
            listOfSlots[counter].itemName = "ringOfKnowledge";
            listOfSlots[counter].itemDescription = "+1 to intelligence";
            imageName = "ringOfKnowledgeImage";
        }
        else if (itemName == "ringOfTheBull")
        {
            listOfSlots[counter].slotStatus = "closed";
            listOfSlots[counter].itemName = "ringOfTheBull";
            listOfSlots[counter].itemDescription = "+1 to strength";
            imageName = "ringOfTheBullImage";
        }
        return imageName;
    }

    public string PopulateEquippedSlot(string itemName, int counter = 0)
    {
        string imageName = "Image";
        if (itemName == "simpleHelmet")
        {
            equippedArmor[counter].slotStatus = "closed";
            equippedArmor[counter].itemName = "simpleHelmet";
            equippedArmor[counter].itemDescription = "+2 to defense";
            equippedArmor[counter].defenseAmount = 2;
            imageName = "simpleHelmetImage";
        }
        else if (itemName == "simpleGloves")
        {
            equippedArmor[counter].slotStatus = "closed";
            equippedArmor[counter].itemName = "simpleGloves";
            equippedArmor[counter].itemDescription = "+1 to defense";
            equippedArmor[counter].defenseAmount = 1;
            imageName = "simpleGlovesImage";
        }
        else if (itemName == "simpleBoots")
        {
            equippedArmor[counter].slotStatus = "closed";
            equippedArmor[counter].itemName = "simpleBoots";
            equippedArmor[counter].itemDescription = "+1 to defense";
            equippedArmor[counter].defenseAmount = 1;
            imageName = "simpleBootsImage";
        }
        else if (itemName == "simpleChest")
        {
            equippedArmor[counter].slotStatus = "closed";
            equippedArmor[counter].itemName = "simpleChest";
            equippedArmor[counter].itemDescription = "+4 to defense";
            equippedArmor[counter].defenseAmount = 4;
            imageName = "simpleChestImage";
        }
        else if (itemName == "ringOfRoses")
        {
            equippedArmor[counter].slotStatus = "closed";
            equippedArmor[counter].itemName = "ringOfRoses";
            equippedArmor[counter].itemDescription = "+1 to vitality";
            playerStatScript.vitality += 1;

            imageName = "ringOfRosesImage";
        }
        else if (itemName == "ringOfEarth")
        {
            equippedArmor[counter].slotStatus = "closed";
            equippedArmor[counter].itemName = "ringOfEarth";
            equippedArmor[counter].itemDescription = "+1 to dexterity";
            playerStatScript.dexterity += 1;

            imageName = "ringOfEarthImage";
        }
        else if (itemName == "ringOfKnowledge")
        {
            equippedArmor[counter].slotStatus = "closed";
            equippedArmor[counter].itemName = "ringOfKnowledge";
            equippedArmor[counter].itemDescription = "+1 to intelligence";
            playerStatScript.intelligence += 1;

            imageName = "ringOfKnowledgeImage";
        }
        else if (itemName == "ringOfTheBull")
        {
            equippedArmor[counter].slotStatus = "closed";
            equippedArmor[counter].itemName = "ringOfTheBull";
            equippedArmor[counter].itemDescription = "+1 to strength";
            playerStatScript.strength += 1;

            imageName = "ringOfTheBullImage";
        }
        return imageName;
    }

    public void ClearInventorySpot(int itemNumber)
    {
        rowIndex = 0;
        if (itemNumber >= 5 && itemNumber < 10)
        {
            rowIndex = 1;
        }
        else if (itemNumber >= 10 && itemNumber < 15)
        {
            rowIndex = 2;
        }
        else if (itemNumber >= 15)
        {
            rowIndex = 3;
        }
        itemSection = GameObject.Find("ItemSection");

        listOfSlots[itemNumber].itemName = "";
        listOfSlots[itemNumber].itemDescription = "";
        listOfSlots[itemNumber].slotStatus = "open";

        itemNumber -= (rowIndex * 5);

        itemSection.transform.GetChild(rowIndex).GetChild(itemNumber).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;


    }

    public void InventoryReset()
    {
        GameObject.Find("LvlUpMenu").SetActive(true);

        itemSection = GameObject.Find("ItemSection");
        counter = 0;

        foreach (Transform inventoryRow in itemSection.transform)
        {
            foreach (Transform itemSlot in inventoryRow.transform)
            {
                itemSlot.GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;

                listOfSlots[counter].itemName = "";
                listOfSlots[counter].itemDescription = "";
                listOfSlots[counter].slotStatus = "open";

                counter++;
            }
        }

        equippedSection = GameObject.Find("EquippedSection");

        counter = 0;

        foreach (Transform itemSlot in equippedSection.transform)
        {
            itemSlot.GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;

            equippedArmor[counter].itemName = "";
            equippedArmor[counter].slotStatus = "open";
            equippedArmor[counter].itemDescription = "";
            equippedArmor[counter].defenseAmount = 0;

            counter++;
        }
    }
}

