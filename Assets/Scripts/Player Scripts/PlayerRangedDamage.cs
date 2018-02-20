using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedDamage : MonoBehaviour
{
    private EngagedWithPlayer playerEngagement;
    private GameObject thisKnife;
    private SFXManager sfxMan;
    public GameObject swordClash;
    public Transform swordClashPoint;
    public Transform hitPoint;
    public GameObject enemyObject;
    public Vector3 targetDir;
    public bool rangedDeathStrike;
    private PlayerController thePlayer;
    public int knifeDirection;
    private ShieldBlock playerShield;
    private float distanceToPlayer;
    private GameObject playerObject;
    public GameObject damageBurst;

    // Use this for initialization
    void Start()
    {
        playerEngagement = FindObjectOfType<EngagedWithPlayer>();
        thePlayer = FindObjectOfType<PlayerController>();
        playerObject = GameObject.Find("Player");
        playerShield = thePlayer.GetComponentInChildren<ShieldBlock>();
        thisKnife = this.gameObject;
        sfxMan = FindObjectOfType<SFXManager>();
        rangedDeathStrike = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculates whether or not knife can hit player based on players shield and position relative to knife

        //Quadrant 1
        // if (targetDir.x < 0 && targetDir.y > 0)
        // {
        //     if (Mathf.Abs(targetDir.x) > targetDir.y)
        //     {
        //         knifeDirection = 3;
        //     }
        //     else if (Mathf.Abs(targetDir.x) < targetDir.y)
        //     {
        //         knifeDirection = 0;
        //     }
        // }
        // //Quadrant 2
        // else if (targetDir.x > 0 && targetDir.y > 0)
        // {
        //     if (targetDir.x < targetDir.y)
        //     {
        //         knifeDirection = 0;
        //     }
        //     else if (targetDir.x > targetDir.y)
        //     {
        //         knifeDirection = 1;
        //     }
        // }
        // //Quardrant 3
        // else if (targetDir.x > 0 && targetDir.y < 0)
        // {
        //     if (targetDir.x > Mathf.Abs(targetDir.y))
        //     {
        //         knifeDirection = 1;
        //     }
        //     else if (targetDir.x < Mathf.Abs(targetDir.y))
        //     {
        //         knifeDirection = 2;
        //     }
        // }
        // //Quadrant 4
        // else if (targetDir.x < 0 && targetDir.y < 0)
        // {
        //     if (targetDir.x < targetDir.y)
        //     {
        //         knifeDirection = 3;
        //     }
        //     else if (targetDir.x > targetDir.y)
        //     {
        //         knifeDirection = 2;
        //     }
        // }

        // if (playerShield.shieldOn)
        // {
        //     if (knifeDirection - thePlayer.directionInt == -2
        //         || knifeDirection - thePlayer.directionInt == 2)
        //     {
        //         rangedDeathStrike = false;
        //     }
        //     else
        //     {
        //         rangedDeathStrike = true;
        //     }
        // }
        // else
        // {
        //     rangedDeathStrike = true;
        // }

    }

    //player is currently doing ranged damange through an enemy script, this should be updated at some point 2-20-18
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "BasicRangedEnemy")
        {
            Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
            playerEngagement.doingDamage(1, thisKnife, this.gameObject);
        }
        else if (other.gameObject.tag == "Wall")
        {
            distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);
            if (distanceToPlayer < 12)
            {
                sfxMan.swordsColliding.volume = ((distanceToPlayer - 12) / -12) * ((distanceToPlayer - 12) / -12) - .1f;
                sfxMan.swordsColliding.Play();
            }
            Instantiate(swordClash, hitPoint.position, hitPoint.rotation);
            thisKnife.SetActive(false);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "BasicRangedEnemy")
        {
            thisKnife.SetActive(false);
        }
    }
}