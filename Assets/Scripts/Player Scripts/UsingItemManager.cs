using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingItemManager : MonoBehaviour
{
    private GameObject playerObject;
    public GameObject itemDesc;

    // Update is called once per frame
    void Update()
    {
        playerObject = GameObject.Find("Player");
        if (Input.GetButtonDown("UseItem") || Input.GetKeyDown("x"))
        {
            if (ItemSlotManager.potionCount > 0)
            {
                FindObjectOfType<PlayerHealthManager>().playerCurrentHealth += 3;
                ItemSlotManager.potionCount--;
                var clone = (GameObject)Instantiate(itemDesc, playerObject.transform.position,
                    Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingItemFind>().itemType = " health";
                clone.GetComponent<FloatingItemFind>().daggerCount = 3;
            }
        }
    }
}