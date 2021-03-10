using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemSlot : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public string slotStatus;
    public int parentIndex;
    public int defenseAmount;

    public ItemSlot(string itemName = "", string itemDescription = "", string slotStatus = "open", int parentIndex = 0, int defenseAmount = 0)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.slotStatus = slotStatus;
        this.parentIndex = parentIndex;
        this.defenseAmount = 0;
    }
}