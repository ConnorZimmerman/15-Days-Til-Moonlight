using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTracker : MonoBehaviour
{
    public string pathName;
    private GameObject enemyObject;
    private EnemyRangedAttack rangedAttack;
    private PlayerPathTracker thePlayerTracker;
    private bool stillOutOfSight;

    // Use this for initialization
    void Start()
    {
        thePlayerTracker = FindObjectOfType<PlayerPathTracker>();
        enemyObject = this.gameObject.transform.parent.gameObject;
        rangedAttack = enemyObject.GetComponent<EnemyRangedAttack>();

        // stillOutOfSight is created to not have the player going back and forth when in range but player out of sight between enemies path and players path
        stillOutOfSight = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if (!rangedAttack.inRange)
        // {
        //     pathName = thePlayerTracker.pathName;
        //     stillOutOfSight = true;
        // }
    }
    // public void OnTriggerStay2D(Collider2D other)
    // {
    //     if (rangedAttack.inRange  && !stillOutOfSight)
    //     {
    //         if (other.gameObject.tag == "PathMaster")
    //         {
    //             pathName = other.gameObject.name;
    //         }
    //     }
    //     else
    //     {
    //         pathName = thePlayerTracker.pathName;
    //     }

    // }
    // public void OnTriggerStay2D(Collider2D other)
    // {

    //     if (other.gameObject.tag == "PathMaster")
    //     {
    //         pathName = thePlayerTracker.pathName;
    //     }
    // }

}