using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive;
    public int currentDamage;
    public GameObject damageNumber;

    private PlayerStats thePS;

    public ShieldBlock shield;

    public Transform hitPoint;

    // Use this for initialization
    void Start()
    {

        shield = FindObjectOfType<ShieldBlock>();

        thePS = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update() { }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player" && shield.shieldOn == false)
        {
            currentDamage = damageToGive - thePS.defense;
            if (currentDamage <= 0)
            {
                currentDamage = 1;
            }

            other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(currentDamage);

            var clone = (GameObject)Instantiate(damageNumber, other.transform.position,
                Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
        }
    }


}
