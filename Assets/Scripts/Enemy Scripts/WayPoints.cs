using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    {
    }
    public Transform PathToPlayer()
    {
        //Stops PathToPlayer() from being called on multiple times without it finishing each search
        stillChecking = true;
        discardPathList.Clear();

        //1) Create necessary lists and other variables
        //List<Transform> tempList = new List<Transform>();
        FindPathList(discardPathList);

        Transform[] closestTwoWayPointsToEnemy = new Transform[2];
        finalWaypoint = pathList[0];
        enemyObject = this.gameObject;

        //2) Find the two closest waypoints to enemy, then choose which is closer to player, check if enemy can get to that waypoint, if not, remove from list, and repeat until waypoint is found.
        while (!barrierCheck)
        {
            count++;
            //Extra check to make sure the while loop does not crash
            if (pathList.Count == 0)
            {
                Debug.Log("loop crash Empty");
                stillChecking = false;
                return finalWaypoint;
            }
            ClosestTwoWayPointsToEnemy(pathList, closestTwoWayPointsToEnemy);

            //3) Checks which of the two waypoints is closer to the player 
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

            //4) Checks if closest waypoint to player has a barrier blocking it
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
        //sets two default closest waypoints
        //Transform[] closestTwoWayPointsToEnemy = new Transform[2];
        if (tempList.Count > 0)
        {
            closestTwoWayPointsToEnemy[0] = tempList[0];
            closestTwoWayPointsToEnemy[1] = tempList[1];
        }
        //Debug.Log(tempList.Count);
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
        //Debug.DrawRay(waypoint.transform.position, targetDir, Color.green, distanceToEnemy);
        //Debug.Log(hit.collider.name);
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

    // public Transform ClosestWayPointToPlayer(List<Transform> tempList)
    // {
    //     //sets default closest waypoint and grabs player object
    //     GameObject thePlayer = GameObject.Find("Player");
    //     if (tempList.Count > 0)
    //     {
    //         closestWayPointToPlayer = tempList[0];
    //     }
    //     //Debug.Log(tempList.Count);
    //     float currentClosestDistance = Vector3.Distance(thePlayer.transform.position, closestWayPointToPlayer.transform.position);

    //     foreach (Transform waypoint in tempList)
    //     {
    //         distanceToPlayer = Vector3.Distance(waypoint.transform.position, thePlayer.transform.position);
    //         if (distanceToPlayer < currentClosestDistance)
    //         {
    //             currentClosestDistance = distanceToPlayer;
    //             closestWayPointToPlayer = waypoint;
    //         }
    //     }
    //     return closestWayPointToPlayer;
    // }

    // public Transform ClosestWayPointToCurWaypoint(Transform curWaypoint, List<Transform> pathList, List<Transform> tempList)
    // {
    //     //Grab the four closest waypoints to current waypoint
    //     List<Transform> closestWaypoints = curWaypoint.GetComponent<WaypointRaycast>().closestFourWaypoints;

    //     //Use first waypoint on list as a default for others to compare to
    //     Transform closestWayPointToCurWaypoint = closestWaypoints[0];
    //     float currentClosestDistance = Vector3.Distance(enemyObject.transform.position, closestWayPointToCurWaypoint.transform.position);

    //     //Find the closest waypoint to the enemy out of the 4 closest waypoints to current waypoint
    //     foreach (Transform waypoint in closestWaypoints)
    //     {
    //         distanceToEnemy = Vector3.Distance(waypoint.transform.position, enemyObject.transform.position);
    //         if (distanceToEnemy < currentClosestDistance)
    //         {
    //             foreach (Transform pathWaypoint in pathList)
    //             {
    //                 if (curWaypoint == pathWaypoint)
    //                 {
    //                     notInList = false;
    //                 }
    //                 else
    //                 {
    //                     notInList = true;
    //                     closestWayPointToCurWaypoint = waypoint;
    //                     currentClosestDistance = distanceToEnemy;
    //                 }
    //             }
    //         }
    //         if (notInList)
    //         {
    //             closestWayPointToCurWaypoint = waypoint;
    //             currentClosestDistance = distanceToEnemy;
    //         }
    //         else
    //         {
    //             tempList.Remove(curWaypoint);
    //             closestWayPointToCurWaypoint = ClosestWayPointToPlayer(tempList);
    //             //closestWayPointToCurWaypoint = closestWayPointToPlayer;
    //         }
    //     }
    //     return closestWayPointToCurWaypoint;
    // }
    // public List<Transform> PathToPlayer()
    // {
    //     stillChecking = true;
    //     //1) Create necessary lists and other variables
    //     List<Transform> tempList = waypoints;
    //     List<Transform> pathList = new List<Transform>();
    //     enemyObject = this.gameObject;

    //     //2) Finds the closest waypoint to player overriding default waypoint
    //     while (!barrierCheck)
    //     {
    //         closestWayPointToPlayer = ClosestWayPointToPlayer(tempList);

    //         //3) Check if closest waypoint to player has a barrier blocking it
    //         //Grabs script from waypoint being check
    //         waypointRaycast = closestWayPointToPlayer.GetComponent<WaypointRaycast>();
    //         barrierCheck = waypointRaycast.PathOpenToPlayer(closestWayPointToPlayer);

    //         //4) If there is a barrier, the waypoint is removed from the temp list and the next closest waypoint will be checked 
    //         if (!barrierCheck)
    //         {
    //             tempList.Remove(closestWayPointToPlayer);
    //         }

    //         //Extra check to make sure the while loop does not crash
    //         if (tempList.Count == 0)
    //         {
    //             Debug.Log("loop crash 1");
    //             stillChecking = false;
    //             break;
    //         }
    //     }
    //     //5) If there is not a barrier, waypoint is added to path list and removed from temp list
    //     if (barrierCheck)
    //     {
    //         pathList.Add(closestWayPointToPlayer);
    //         tempList.Remove(closestWayPointToPlayer);
    //     }
    //     //Reset barrierCheck to be used again
    //     barrierCheck = false;
    //     curWaypoint = closestWayPointToPlayer;

    //     //6) Next, there's a check to see if enemy can get to most recently added waypoint and if not, the rest of the path list will be built until the enemy can reach first waypoint
    //     while (!barrierCheck)
    //     {
    //         count++;

    //         waypointRaycast = curWaypoint.GetComponent<WaypointRaycast>();
    //         barrierCheck = waypointRaycast.PathOpenToEnemy(enemyObject);

    //         //If enemy cannot get to most recently added waypoint and the current waypoint is not the closest waypoint to the player then the waypoint is added to the path list and the next closest waypoint to current waypoint becomes current waypoint
    //         if (!barrierCheck && curWaypoint != closestWayPointToPlayer)
    //         {
    //             //Check to see if waypoint has already been added to pathlist
    //             // if (notInList)
    //             // {
    //             pathList.Add(curWaypoint);
    //             tempWaypoint = ClosestWayPointToCurWaypoint(curWaypoint, pathList, tempList);
    //             tempList.Remove(curWaypoint);
    //             curWaypoint = tempWaypoint;
    //             // }
    //             // else
    //             // {
    //             //     pathList =  new List<Transform>();
    //             //     tempList.Remove(curWaypoint);
    //             //     curWaypoint = ClosestWayPointToPlayer(tempList);
    //             // }
    //             //Debug.Log(curWaypoint);
    //         }
    //         else if (curWaypoint == closestWayPointToPlayer)
    //         {
    //             tempWaypoint = ClosestWayPointToCurWaypoint(curWaypoint, pathList, tempList);
    //             curWaypoint = tempWaypoint;
    //         }
    //         else
    //         {
    //             pathList.Add(curWaypoint);
    //             stillChecking = false;
    //             return pathList;
    //         }
    //         if (tempList.Count == 0)
    //         {
    //             Debug.Log("loop crash 2 EMPTY");
    //             break;
    //         }
    //         if (count == 100)
    //         {
    //             Debug.Log("loop crash 2");
    //             break;
    //         }
    //         // Debug.Log(closestWayPointToPlayer);
    //         // Debug.Log(barrierCheck);
    //     }
    //     return pathList;
    //     // raycast.PathOpen(A);
    //     //stillChecking = false;
    //     //return pathList;
    // }

    // public Transform ClosestWayPointToPlayer(List<Transform> tempList)
    // {
    //     //sets default closest waypoint and grabs player object
    //     GameObject thePlayer = GameObject.Find("Player");
    //     if (tempList.Count > 0)
    //     {
    //         closestWayPointToPlayer = tempList[0];
    //     }
    //     //Debug.Log(tempList.Count);
    //     float currentClosestDistance = Vector3.Distance(thePlayer.transform.position, closestWayPointToPlayer.transform.position);

    //     foreach (Transform waypoint in tempList)
    //     {
    //         distanceToPlayer = Vector3.Distance(waypoint.transform.position, thePlayer.transform.position);
    //         if (distanceToPlayer < currentClosestDistance)
    //         {
    //             currentClosestDistance = distanceToPlayer;
    //             closestWayPointToPlayer = waypoint;
    //         }
    //     }
    //     return closestWayPointToPlayer;
    // }

    // public Transform ClosestWayPointToCurWaypoint(Transform curWaypoint, List<Transform> pathList, List<Transform> tempList)
    // {
    //     //Grab the four closest waypoints to current waypoint
    //     List<Transform> closestWaypoints = curWaypoint.GetComponent<WaypointRaycast>().closestFourWaypoints;

    //     //Use first waypoint on list as a default for others to compare to
    //     Transform closestWayPointToCurWaypoint = closestWaypoints[0];
    //     float currentClosestDistance = Vector3.Distance(enemyObject.transform.position, closestWayPointToCurWaypoint.transform.position);

    //     //Find the closest waypoint to the enemy out of the 4 closest waypoints to current waypoint
    //     foreach (Transform waypoint in closestWaypoints)
    //     {
    //         distanceToEnemy = Vector3.Distance(waypoint.transform.position, enemyObject.transform.position);
    //         if (distanceToEnemy < currentClosestDistance)
    //         {
    //             foreach (Transform pathWaypoint in pathList)
    //             {
    //                 if (curWaypoint == pathWaypoint)
    //                 {
    //                     notInList = false;
    //                 }
    //                 else
    //                 {
    //                     notInList = true;
    //                     closestWayPointToCurWaypoint = waypoint;
    //                     currentClosestDistance = distanceToEnemy;
    //                 }
    //             }
    //         }
    //         if (notInList)
    //         {
    //             closestWayPointToCurWaypoint = waypoint;
    //             currentClosestDistance = distanceToEnemy;
    //         }
    //         else
    //         {
    //             tempList.Remove(curWaypoint);
    //             closestWayPointToCurWaypoint = ClosestWayPointToPlayer(tempList);
    //             //closestWayPointToCurWaypoint = closestWayPointToPlayer;
    //         }
    //     }
    //     return closestWayPointToCurWaypoint;
    // }
}
