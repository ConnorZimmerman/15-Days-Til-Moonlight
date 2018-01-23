using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject projectile;
    public float throwForce;
    // Use this for initialization
    void Start()
    {
        playerObject = GameObject.Find("Player");
        throwForce = 275;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Throw"))
        {
            GameObject newKnife = Instantiate(projectile, playerObject.transform.position, playerObject.transform.rotation);
            newKnife.GetComponent<RangedDamage>().targetDir = new Vector2(90, 90);
            newKnife.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, throwForce));
        }
    }
}
