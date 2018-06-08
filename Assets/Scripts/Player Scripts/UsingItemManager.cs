using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingItemManager : MonoBehaviour
{
    private GameObject playerObject;
    public GameObject itemDesc;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerObject = GameObject.Find("Player");
        if (Input.GetButtonDown("UseItem"))
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
