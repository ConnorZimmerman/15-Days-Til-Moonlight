using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotManager : MonoBehaviour
{
    public List<ItemSlot> listOfSlots = new List<ItemSlot>();
    public GameObject itemSection;
    public int slotIndex = 0;
    public int rowIndex = 1;

    // Use this for initialization
    void Awake()
    {
        for (int i = 0; i < 20; i++)
        {
            listOfSlots.Add(new ItemSlot());
        }
        itemSection = GameObject.Find("ItemSection");
        foreach (Transform inventoryRow in itemSection.transform)
        {
            foreach (Transform itemSlot in inventoryRow.transform)
            {
                itemSlot.GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("Image").GetComponent<Image>().sprite;
            }
        }
    }

    public void ItemPickUp(string itemName)
    {
        // Debug.Log("Getting here" + listOfSlots.Count);
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
                slotIndex = i;
                break;
            }
        }
        slotIndex -= (rowIndex * 5);
        // Debug.Log("Row " + rowIndex);
        // Debug.Log("Slot " + slotIndex);
        if (itemName == "potion")
        {

            itemSection.transform.GetChild(rowIndex).GetChild(slotIndex).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("PotionImage").GetComponent<Image>().sprite;
        }
        else
        {
            itemSection.transform.GetChild(rowIndex).GetChild(slotIndex).GetChild(0).GetChild(1).GetComponent<Image>().sprite = GameObject.Find("DaggerImage").GetComponent<Image>().sprite;
        }
    }

    public void ItemSelect()
    {

    }
}

