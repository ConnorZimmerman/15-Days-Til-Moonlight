using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{

    public static void GetItem()
    {
        FindObjectOfType<PlayerRangedAttack>().daggerCount += 3;
    }

    public static void CreateItem(GameObject enemyObject)
    {
        Debug.Log("In Item Script");
		int randomNum = UnityEngine.Random.Range(0, 2);
		if(randomNum == 1){
	        GameObject randomDrop = GameObject.Find("Item_Pouch");
    	    GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation);
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
