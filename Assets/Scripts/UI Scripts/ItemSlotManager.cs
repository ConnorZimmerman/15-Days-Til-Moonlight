using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotManager : MonoBehaviour
{
    private GlobalDataScript globalDataScript;
    private PauseMenuButtons pauseMenuButtonsScript;
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
        // try
        // {
        Dictionary<string, List<string>> playerDataDict = GlobalDataScript.Load();
        // Debug.Log(playerDataDict["playerInventory"].Count);
        // }
        // catch
        // {
        //     Debug.Log("empty");
        // }
        // Debug.Log("List of Slots" + listOfSlots.Count);
        // if (loadedList.Count > 0)
        // {
        //     Debug.Log("hi");
        // }
        // Debug.Log("Loaded" + loadedList.Count);
        // if (listOfSlots.Count == 0)
        // {
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
                    // Debug.Log(counter + "item: " + playerDataDict["playerInventory"][counter]);
                    // Debug.Log(counter + "item: " + playerDataDict["playerInventory"].Count);
                    itemSlot.GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find(PopulateItemSlot(playerDataDict["playerInventory"][counter], counter)).GetComponent<Image>().sprite;
                    // itemSlot.GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;
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
                if (listOfSlots[i].slotStatus == "open")
                {
                    listOfSlots[i].itemName = itemName;
                    PopulateItemSlot(itemName, i);
                    slotIndex = i;
                    break;
                }
            }
        }

        slotIndex -= (rowIndex * 5);

        if (itemName == "potion")
        {
            potionCount++;
            // itemSection.transform.GetChild(rowIndex).GetChild(slotIndex).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("potionImage").GetComponent<Image>().sprite;
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
                equippedArmor[0].itemName = "simpleHelmet";
                equippedArmor[0].slotStatus = "closed";
                equippedArmor[0].itemDescription = "+2 to defense";
                equippedSection = GameObject.Find("EquippedSection");

                equippedSection.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("simpleHelmetImage").GetComponent<Image>().sprite;

                itemSection = GameObject.Find("ItemSection");
                listOfSlots[itemNumber].itemName = "";
                listOfSlots[itemNumber].itemDescription = "";
                listOfSlots[itemNumber].slotStatus = "open";

                // eventually add a method to clear list spot
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

                itemNumber -= (rowIndex * 5);

                itemSection.transform.GetChild(rowIndex).GetChild(itemNumber).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;

            }
        }
        if (itemNumber > 19)
        {
            if (equippedArmor[0].itemName == "simpleHelmet")
            {
                equippedArmor[0].itemName = "";
                equippedArmor[0].slotStatus = "open";
                equippedArmor[0].itemDescription = "";
                equippedSection = GameObject.Find("EquippedSection");

                equippedSection.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;

                ItemPickUp("simpleHelmet");
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
        return imageName;
    }
}

