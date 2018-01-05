using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllWaypoints : MonoBehaviour
{
    public List<Transform> waypoints;
    public List<Transform> pathOneMaster;
    public List<Transform> pathTwoMaster;
    public List<Transform> pathThreeMaster;
    public List<Transform> pathFourMaster;
    public List<Transform> pathFiveMaster;
    public List<Transform> pathSixMaster;
    public List<Transform> pathSevenMaster;
    public List<Transform> pathEightMaster;
    public List<Transform> pathNineMaster;
    public GameObject waypointsParent;
    // Use this for initialization
    void Start()
    {
        // waypointsParent = GameObject.Find("WayPointsMaster");
        // foreach (Transform waypoint in waypointsParent.GetComponentInChildren<Transform>())
        // {
        //     waypoints.Add(waypoint);
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
