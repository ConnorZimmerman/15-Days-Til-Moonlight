using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointRaycast : MonoBehaviour
{
    private Transform curWaypoint;
    public float distanceToWaypoint;
    private float distanceToPlayer;
    private float distanceToEnemy;
    public GameObject waypointsParent;
    public List<Transform> closestFourWaypoints;
    public float furthestWayPointDistance;
    public int indexOfFurthestOut;

    // Use this for initialization
    void Start()
    {
        curWaypoint = this.gameObject.transform;

        //Finds the four closest waypoints to current waypoint at start so list can more easily be checked later
        // waypointsParent = GameObject.Find("WayPointsMaster");
        // foreach (Transform waypoint in waypointsParent.GetComponentInChildren<Transform>())
        // {
        //     //Makes sure we don't add the waypoint that is compiling the list
        //     if (waypoint != curWaypoint)
        //     {
        //         //Adds the first 4 waypoints so the others have a distance to compare to
        //         if (closestFourWaypoints.Count != 4)
        //         {
        //             closestFourWaypoints.Add(waypoint);
        //             continue;
        //         }
        //         //Find the furthest out of the four waypoints
        //         furthestWayPointDistance = 0;
        //         for (int i = 0; i < closestFourWaypoints.Count; i++)
        //         {
        //             distanceToWaypoint = Vector3.Distance(curWaypoint.transform.position, closestFourWaypoints[i].transform.position);
        //             if (distanceToWaypoint > furthestWayPointDistance)
        //             {
        //                 furthestWayPointDistance = distanceToWaypoint;
        //                 indexOfFurthestOut = i;
        //             }
        //         }
        //         //Compare the rest of the waypoints to the distance of the waypoints in the current list and replaces the furthest 
        //         for (int i = 0; i < closestFourWaypoints.Count; i++)
        //         {
        //             distanceToWaypoint = Vector3.Distance(curWaypoint.transform.position, waypoint.transform.position);
        //             if (distanceToWaypoint < furthestWayPointDistance)
        //             {
        //                 closestFourWaypoints[indexOfFurthestOut] = waypoint;
        //                 break;
        //             }
        //         }

        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool PathOpenToWaypoint(GameObject otherWaypoint)
    {
        distanceToWaypoint = Vector3.Distance(transform.position, otherWaypoint.transform.position);
        Vector2 targetDir = otherWaypoint.transform.position - curWaypoint.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDir, distanceToWaypoint, ~1 >> 12 | ~1 >> 8);

        if (hit.collider.tag == "WayPoint")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PathOpenToPlayer(Transform waypoint)
    {
        GameObject playerObject = GameObject.Find("Player");
        distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);
        Vector3 targetDir = playerObject.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDir, distanceToPlayer, ~1 << 8 | ~1 << 9);
        if (hit.collider.tag == "Player")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // public bool PathOpenToEnemy(GameObject enemyObject)
    // {
    //     distanceToEnemy = Vector3.Distance(transform.position, enemyObject.transform.position);
    //     Vector3 targetDir = enemyObject.transform.position - transform.position;
    //     RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDir, distanceToEnemy, ~1 << 8 | ~1 << 11);
    //     if (hit.collider.tag == "Enemy")
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }
}
