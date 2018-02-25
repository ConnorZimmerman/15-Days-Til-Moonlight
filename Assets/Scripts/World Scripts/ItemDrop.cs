using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public string itemType;

    void Start()
    {
        itemType = this.tag;
    }
    public void GetItem()
    {
        if (itemType == "daggers")
        {
            int randomNum = UnityEngine.Random.Range(1, 6);
            FindObjectOfType<PlayerRangedAttack>().daggerCount += randomNum;
        }
        else if(itemType == "potion"){
            FindObjectOfType<PlayerHealthManager>().playerCurrentHealth += 3;
        }
    }

    public void CreateItem(GameObject enemyObject)
    {
        int randomNum = UnityEngine.Random.Range(0, 31);
        if (enemyObject.tag == "BasicRangedEnemy" && randomNum > 5)
        {
            GameObject randomDrop = GameObject.Find("Item_Pouch");
            GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation)
            ;
        }
        else if (randomNum == 30)
        {
            GameObject randomDrop = GameObject.Find("Potion");
            GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation)
            ;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            GetItem();
            Destroy(gameObject);
        }
    }

}
