using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBuffManager : MonoBehaviour
{
    private ItemSlotManager itemSlotManagerScript;

    // Use this for initialization
    void Awake()
    {
        itemSlotManagerScript = FindObjectOfType<ItemSlotManager>();
    }

    // // Update is called once per frame
    // void Update () {

    // }
    public int PlayerDefenseCalculator()
    {
        int defense = 0;
        foreach (ItemSlot item in itemSlotManagerScript.equippedArmor)
        {
            defense += item.defenseAmount;
        }
        return defense;
    }
}
