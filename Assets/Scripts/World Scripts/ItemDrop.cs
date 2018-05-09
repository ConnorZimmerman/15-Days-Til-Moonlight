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
        // eventually add in method to do random calculations
        int randomNum = LuckCalculator();
        Debug.Log("Luck: " + randomNum);
        if (enemyObject.tag == "BasicRangedEnemy" && randomNum > 70)
        {
            GameObject randomDrop = GameObject.Find("daggers");
            GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation)
            ;
        }
        if (randomNum >= 90)
        {
            GameObject randomDrop = GameObject.Find("potion");
            GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation);
        }

        if (randomNum >= 135 && randomNum < 150)
        {
            GameObject randomDrop = GameObject.Find("simpleGloves");
            GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation);
        }
        else if (randomNum >= 151 && randomNum < 165)
        {
            GameObject randomDrop = GameObject.Find("simpleBoots");
            GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation);
        }
        else if (randomNum >= 165 && randomNum < 170)
        {
            GameObject randomDrop = GameObject.Find("ringOfRoses");
            GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation);
        }
        else if (randomNum >= 170 && randomNum < 175)
        {
            GameObject randomDrop = GameObject.Find("ringOfEarth");
            GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation);
        }
        else if (randomNum >= 175 && randomNum < 180)
        {
            GameObject randomDrop = GameObject.Find("ringOfKnowledge");
            GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation);
        }
        else if (randomNum >= 180 && randomNum < 185)
        {
            GameObject randomDrop = GameObject.Find("ringOfTheBull");
            GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation);
        }
        else if (randomNum >= 185 && randomNum < 195)
        {
            GameObject randomDrop = GameObject.Find("simpleHelmet");
            GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation);
        }
        else if (randomNum >= 195)
        {
            GameObject randomDrop = GameObject.Find("simpleChest");
            GameObject newItem = Instantiate(randomDrop, enemyObject.transform.position, enemyObject.transform.rotation);
        }

    }

    public int LuckCalculator()
    {
        int randomNum = (UnityEngine.Random.Range(0, 51)) + ((playerStatScript.intelligence));
        if (randomNum >= 40)
        {
            randomNum = (UnityEngine.Random.Range(50, 101)) + ((playerStatScript.intelligence));

            // tier 2
            if (randomNum >= 95)
            {
                if (playerStatScript.intelligence >= 10)
                {
                    randomNum = (UnityEngine.Random.Range(101, 201)) + ((playerStatScript.intelligence));
                }
                else
                {
                    randomNum = (UnityEngine.Random.Range(101, 201));
                }
                // tier 3
                if (randomNum >= 200)
                {
                    if (playerStatScript.intelligence >= 20)
                    {
                        randomNum = (UnityEngine.Random.Range(201, 301)) + ((playerStatScript.intelligence));
                    }
                    else
                    {
                        randomNum = (UnityEngine.Random.Range(201, 301));
                    }
                    // tier 4
                    if (randomNum >= 300)
                    {
                        if (playerStatScript.intelligence >= 30)
                        {
                            randomNum = (UnityEngine.Random.Range(301, 401)) + ((playerStatScript.intelligence));
                        }
                        else
                        {
                            randomNum = (UnityEngine.Random.Range(301, 401));
                        }
                    }
                    // tier 5
                    if (randomNum >= 400)
                    {
                        if (playerStatScript.intelligence >= 40)
                        {
                            randomNum = (UnityEngine.Random.Range(401, 501)) + ((playerStatScript.intelligence));
                        }
                        else
                        {
                            randomNum = (UnityEngine.Random.Range(401, 501));
                        }
                    }
                    // tier 6
                    if (randomNum >= 500)
                    {
                        if (playerStatScript.intelligence >= 50)
                        {
                            randomNum = (UnityEngine.Random.Range(501, 601)) + ((playerStatScript.intelligence));
                        }
                        else
                        {
                            randomNum = (UnityEngine.Random.Range(501, 601));
                        }
                    }
                }
            }
        }
        return randomNum;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            GetItem();
            itemSlotManagerScript.ItemPickUp(this.gameObject.tag);
            Destroy(gameObject);
        }
    }

}
