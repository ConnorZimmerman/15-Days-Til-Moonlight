using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotManager : MonoBehaviour
{
    private GlobalDataScript globalDataScript;
    public List<ItemSlot> listOfSlots;
    // public List<ItemSlot> equippedArmor;
    public GameObject itemSection;
    public int slotIndex = 0;
    public int rowIndex = 1;
    public int counter = 0;
    public static int potionCount;

    // Use this for initialization
    void Awake()
    {
        globalDataScript = FindObjectOfType<GlobalDataScript>();
        // try
        // {
        List<string> loadedList = GlobalDataScript.Load();
        Debug.Log(loadedList.Count);
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
        if (loadedList.Count > 0)
        {
            foreach (Transform inventoryRow in itemSection.transform)
            {
                foreach (Transform itemSlot in inventoryRow.transform)
                {
                    itemSlot.GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find(PopulateItemSlot(loadedList[counter])).GetComponent<Image>().sprite;
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
        // }
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
                    listOfSlots[i].slotStatus = "closed";
                    listOfSlots[i].itemName = itemName;
                    slotIndex = i;
                    break;
                }
            }
        }
        slotIndex -= (rowIndex * 5);

        if (itemName == "potion")
        {
            potionCount++;
            // itemSection.transform.GetChild(rowIndex).GetChild(slotIndex).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("PotionImage").GetComponent<Image>().sprite;
        }
        else
        {
            itemSection.transform.GetChild(rowIndex).GetChild(slotIndex).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("DaggerImage").GetComponent<Image>().sprite;
        }
    }

    public void ItemSelect()
    {

    }

    public string PopulateItemSlot(string itemName)
    {
        string imageName = "Image";
        if (itemName == "daggers")
        {

            listOfSlots[counter].slotStatus = "closed";
            listOfSlots[counter].itemName = "daggers";
            listOfSlots[counter].itemDescription = "Daggers can be thrown at enemies by clicking x.";
            imageName = "DaggerImage";
        }
        else if (itemName == "potion")
        {

            listOfSlots[counter].slotStatus = "closed";
            listOfSlots[counter].itemName = "potion";
            listOfSlots[counter].itemDescription = "Potions can heal you for 2-4 health.";
            imageName = "PotionImage";
        }
        return imageName;
    }
}

