using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    private WaypointRaycast waypointRaycast;
    private AllWaypoints allWaypoints;
    public List<Transform> waypoints;
    public GameObject enemyObject;
    public Transform curWaypoint;
    public Transform closestWayPointToPlayer;
    public Transform tempWaypoint;
    private float distanceToPlayer;
    private float distanceToEnemy;
    private bool barrierCheck;
    public int count;
    public GameObject waypointsParent;
    public bool stillChecking;
    private bool notInList = true;
    public Transform finalWaypoint;
    private PathTracker pathTracker;
    public List<Transform> pathList;
    public List<Transform> discardPathList;
    private PlayerPathTracker thePlayerTracker;

    // Use this for initialization
    void Start()
    {
        enemyObject = this.gameObject;
        allWaypoints = FindObjectOfType<AllWaypoints>();
        pathTracker = enemyObject.GetComponentInChildren<PathTracker>();
        thePlayerTracker = FindObjectOfType<PlayerPathTracker>();

        //waypointsParent = GameObject.Find("WayPointsMaster");

        barrierCheck = false;
        stillChecking = false;
        notInList = true;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    { }
    public Transform PathToPlayer()
    {
        //Stops PathToPlayer() from being called on multiple times without it finishing each search
        stillChecking = true;
        discardPathList.Clear();

        //1) Create necessary lists and other variables
        FindPathList(discardPathList);

        Transform[] closestTwoWayPointsToEnemy = new Transform[2];
        finalWaypoint = pathList[0];
        enemyObject = this.gameObject;

        //2) Find the two closest waypoints to enemy, then choose which is closer to player, check if enemy can get to that waypoint, if not, remove from list, and repeat until waypoint is found.
        while (!barrierCheck)
        {
            count++;
            // Extra check to make sure the while loop does not crash
            if (pathList.Count == 0)
            {
                Debug.Log("loop crash Empty");
                stillChecking = false;
                return finalWaypoint;
            }
            ClosestTwoWayPointsToEnemy(pathList, closestTwoWayPointsToEnemy);

            // 3) Checks which of the two waypoints is closer to the player 
            float distanceToPlayerOne = Vector3.Distance(closestTwoWayPointsToEnemy[0].transform.position, transform.position);
            float distanceToPlayerTwo = Vector3.Distance(closestTwoWayPointsToEnemy[1].transform.position, transform.position);
            if (distanceToPlayerOne < distanceToPlayerTwo)
            {
                finalWaypoint = closestTwoWayPointsToEnemy[0];
            }
            else
            {
                finalWaypoint = closestTwoWayPointsToEnemy[1];
            }

            // 4) Checks if closest waypoint to player has a barrier blocking it
            barrierCheck = PathOpenToEnemy(finalWaypoint);
            Debug.Log(barrierCheck);

            //5) If there is a barrier, the 2nd waypoint is checked 
            if (!barrierCheck)
            {
                if (distanceToPlayerOne > distanceToPlayerTwo)
                {
                    finalWaypoint = closestTwoWayPointsToEnemy[0];
                }
                else
                {
                    finalWaypoint = closestTwoWayPointsToEnemy[1];
                }
            }
            else
            {
                stillChecking = false;
                return finalWaypoint;
            }

            barrierCheck = PathOpenToEnemy(finalWaypoint);
            Debug.Log(barrierCheck);

            //6) if there is still a barrier both of closest are removed and the loop restarted until the temp list is empty or a suitable waypoint is found
            if (!barrierCheck)
            {
                discardPathList.Add(closestTwoWayPointsToEnemy[0]);
                discardPathList.Add(closestTwoWayPointsToEnemy[1]);
            }
            else
            {
                stillChecking = false;
                return finalWaypoint;
            }
            if (count == 100)
            {
                Debug.Log("loop crash");
                break;
            }
        }
        stillChecking = false;
        return finalWaypoint;
    }
    public Transform[] ClosestTwoWayPointsToEnemy(List<Transform> tempList, Transform[] closestTwoWayPointsToEnemy)
    {
        // Sets two default closest waypoints
        if (tempList.Count > 0)
        {
            closestTwoWayPointsToEnemy[0] = tempList[0];
            closestTwoWayPointsToEnemy[1] = tempList[1];
        }

        float currentClosestDistanceOne = Vector3.Distance(transform.position, closestTwoWayPointsToEnemy[0].transform.position);
        float currentClosestDistanceTwo = Vector3.Distance(transform.position, closestTwoWayPointsToEnemy[1].transform.position);

        foreach (Transform waypoint in tempList)
        {
            distanceToPlayer = Vector3.Distance(waypoint.transform.position, transform.position);
            if (distanceToPlayer < currentClosestDistanceOne)
            {
                currentClosestDistanceOne = distanceToPlayer;
                closestTwoWayPointsToEnemy[0] = waypoint;
            }
            else if (distanceToPlayer < currentClosestDistanceTwo)
            {
                currentClosestDistanceTwo = distanceToPlayer;
                closestTwoWayPointsToEnemy[1] = waypoint;
            }
        }
        return closestTwoWayPointsToEnemy;
    }

    public bool PathOpenToEnemy(Transform waypoint)
    {
        distanceToEnemy = Vector3.Distance(waypoint.transform.position, enemyObject.transform.position);
        Vector3 targetDir = enemyObject.transform.position - waypoint.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(waypoint.transform.position, targetDir, distanceToEnemy, 1 << 8 | 1 << 11);

        if (hit.collider.tag == "BasicRangedEnemy")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FindPathList(List<Transform> discardPathList)
    {
        pathList.Clear();
        string pathName = thePlayerTracker.pathName;

        if (pathName == "PathOneMaster")
        {
            foreach (Transform waypoint in allWaypoints.pathOneMaster)
            {
                if (discardPathList.Count != 0)
                {
                    for (int i = 0; i < discardPathList.Count; i++)
                    {
                        if (waypoint == discardPathList[i])
                        {
                            break;
                        }
                        if (i + 1 == discardPathList.Count)
                        {
                            pathList.Add(waypoint);
                        }
                    }
                }
                else
                {
                    pathList.Add(waypoint);
                }
            }
            return;
        }
        else if (pathName == "PathTwoMaster")
        {
            foreach (Transform waypoint in allWaypoints.pathTwoMaster)
            {
                if (discardPathList.Count != 0)
                {
                    for (int i = 0; i < discardPathList.Count; i++)
                    {
                        if (waypoint == discardPathList[i])
                        {
                            break;
                        }
                        if (i + 1 == discardPathList.Count)
                        {
                            pathList.Add(waypoint);
                        }
                    }
                }
                else
                {
                    pathList.Add(waypoint);
                }
            }
            return;
        }
        else if (pathName == "PathThreeMaster")
        {
            foreach (Transform waypoint in allWaypoints.pathThreeMaster)
            {
                if (discardPathList.Count != 0)
                {
                    for (int i = 0; i < discardPathList.Count; i++)
                    {
                        if (waypoint == discardPathList[i])
                        {
                            break;
                        }
                        if (i + 1 == discardPathList.Count)
                        {
                            pathList.Add(waypoint);
                        }
                    }
                }
                else
                {
                    pathList.Add(waypoint);
                }
            }
            return;
        }
        else if (pathName == "PathFourMaster")
        {
            foreach (Transform waypoint in allWaypoints.pathFourMaster)
            {
                if (discardPathList.Count != 0)
                {
                    for (int i = 0; i < discardPathList.Count; i++)
                    {
                        if (waypoint == discardPathList[i])
                        {
                            break;
                        }
                        if (i + 1 == discardPathList.Count)
                        {
                            pathList.Add(waypoint);
                        }
                    }
                }
                else
                {
                    pathList.Add(waypoint);
                }
            }
            return;
        }
        else if (pathName == "PathFiveMaster")
        {
            foreach (Transform waypoint in allWaypoints.pathFiveMaster)
            {
                if (discardPathList.Count != 0)
                {
                    for (int i = 0; i < discardPathList.Count; i++)
                    {
                        if (waypoint == discardPathList[i])
                        {
                            break;
                        }
                        if (i + 1 == discardPathList.Count)
                        {
                            pathList.Add(waypoint);
                        }
                    }
                }
                else
                {
                    pathList.Add(waypoint);
                }
            }
            return;
        }
        else if (pathName == "PathSixMaster")
        {
            foreach (Transform waypoint in allWaypoints.pathSixMaster)
            {
                if (discardPathList.Count != 0)
                {
                    for (int i = 0; i < discardPathList.Count; i++)
                    {
                        if (waypoint == discardPathList[i])
                        {
                            break;
                        }
                        if (i + 1 == discardPathList.Count)
                        {
                            pathList.Add(waypoint);
                        }
                    }
                }
                else
                {
                    pathList.Add(waypoint);
                }
            }
            return;
        }
        else if (pathName == "PathSevenMaster")
        {
            foreach (Transform waypoint in allWaypoints.pathEightMaster)
            {
                if (discardPathList.Count != 0)
                {
                    for (int i = 0; i < discardPathList.Count; i++)
                    {
                        if (waypoint == discardPathList[i])
                        {
                            break;
                        }
                        if (i + 1 == discardPathList.Count)
                        {
                            pathList.Add(waypoint);
                        }
                    }
                }
                else
                {
                    pathList.Add(waypoint);
                }
            }
            return;
        }
        else if (pathName == "PathEightMaster")
        {
            foreach (Transform waypoint in allWaypoints.pathEightMaster)
            {
                if (discardPathList.Count != 0)
                {
                    for (int i = 0; i < discardPathList.Count; i++)
                    {
                        if (waypoint == discardPathList[i])
                        {
                            break;
                        }
                        if (i + 1 == discardPathList.Count)
                        {
                            pathList.Add(waypoint);
                        }
                    }
                }
                else
                {
                    pathList.Add(waypoint);
                }
            }
            return;
        }
        else
        {
            foreach (Transform waypoint in allWaypoints.pathNineMaster)
            {
                if (discardPathList.Count != 0)
                {
                    for (int i = 0; i < discardPathList.Count; i++)
                    {
                        if (waypoint == discardPathList[i])
                        {
                            break;
                        }
                        if (i + 1 == discardPathList.Count)
                        {
                            pathList.Add(waypoint);
                        }
                    }
                }
                else
                {
                    pathList.Add(waypoint);
                }
            }
            return;
        }
    }
}