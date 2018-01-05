using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : MonoBehaviour
{

    public int value;
    public MoneyManager theMM;

    // Use this for initialization
    void Start()
    {

        theMM = FindObjectOfType<MoneyManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            theMM.AddMoney(value);
            Destroy(gameObject);
        }
    }
}
