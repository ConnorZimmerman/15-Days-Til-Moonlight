using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public string itemType;
    public GameObject itemDesc;
    private GameObject playerObject;
    private PlayerStats playerStatScript;
    private ItemSlotManager itemSlotManagerScript;

    void Start()
    {
        itemType = this.tag;
        playerObject = GameObject.Find("Player");
        playerStatScript = FindObjectOfType<PlayerStats>();
        itemSlotManagerScript = FindObjectOfType<ItemSlotManager>();
    }
    public void GetItem()
    {
        if (itemType == "daggers")
        {
            int randomNum = UnityEngine.Random.Range(1, 6);
            FindObjectOfType<PlayerRangedAttack>().daggerCount += randomNum;
            var clone = (GameObject)Instantiate(itemDesc, playerObject.transform.position,
                Quaternion.Euler(Vector3.zero));
            if (randomNum > 1)
            {
                clone.GetComponent<FloatingItemFind>().itemType = " daggers";
            }
            else
            {
                clone.GetComponent<FloatingItemFind>().itemType = " dagger";
            }
            clone.GetComponent<FloatingItemFind>().daggerCount = randomNum;
        }
        else if (itemType == "potion")
        {
            // FindObjectOfType<PlayerHealthManager>().playerCurrentHealth += 3;
            // var clone = (GameObject)Instantiate(itemDesc, playerObject.transform.position,
            //     Quaternion.Euler(Vector3.zero));
            // clone.GetComponent<FloatingItemFind>().itemType = " health";
            // clone.GetComponent<FloatingItemFind>().daggerCount = 3;
        }
    }

    public void CreateItem(GameObject enemyObject)
    {
        int randomNum = (UnityEngine.Random.Range(0, 51)) + ((playerStatScript.intelligence * 2)- 10);
        if (enemyObject.tag == "BasicRangedEnemy" && randomNum > 10)
        {
            GameObject randomDrop = GameObject.Find("daggers");
            GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation)
            ;
        }
        if (randomNum >= 50)
        {
            GameObject randomDrop = GameObject.Find("potion");
            GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation)
            ;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            GetItem();
            // Debug.Log(this.gameObject.tag);
            itemSlotManagerScript.ItemPickUp(this.gameObject.tag);
            Destroy(gameObject);
        }
    }

}
