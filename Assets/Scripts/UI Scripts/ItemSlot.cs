using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


[Serializable]
public class ItemSlot : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public string slotStatus;
    // public Image itemImage;
    public int parentIndex;
    public ItemSlot(string itemName = "", string itemDescription = "", string slotStatus = "open", int parentIndex = 0)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.slotStatus = slotStatus;
        // this.itemImage = itemImage;
        this.parentIndex = parentIndex;
    }
}
