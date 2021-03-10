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

    // Player is currently doing ranged damange through an enemy script, this should be updated at some point 2-20-18
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "BasicRangedEnemy")
        {
            Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
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