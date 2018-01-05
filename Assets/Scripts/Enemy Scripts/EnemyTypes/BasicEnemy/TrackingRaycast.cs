using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingRaycast : MonoBehaviour
{
    private EnemyTestScript enemyScript;
    private GameObject enemyObject;
    private GameObject playerObject;
    private float distanceToPlayer;
    public bool lineOfSight;
    private Vector2 enemyPos;
    private Vector2 playerPos;
    //public List<Node> pathFound;
    public Vector2[] path;
    private Pathfinding pathfinder;

    public bool enqueue;

    // Use this for initialization
    void Start()
    {
        enemyObject = this.gameObject;
        enemyScript = enemyObject.GetComponent<EnemyTestScript>();
        playerObject = GameObject.Find("Player");
        pathfinder = FindObjectOfType<Pathfinding>();
        enqueue = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyScript.following && enemyScript.enemyMoving)
        {
            distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);
            Vector3 targetDir = playerObject.transform.position - enemyObject.transform.position;

            if (!enemyScript.isPathfinding)
            {
                RaycastHit2D hit = Physics2D.Raycast(enemyObject.transform.position, targetDir, distanceToPlayer, 1 << 8 | 1 << 9);
                if (hit.collider.tag == "Player")
                {
                    lineOfSight = true;
                }
                else
                {
                    lineOfSight = false;
                }
            }
        }
        if (!lineOfSight)
        {
            lineOfSight = false;
            enemyPos = enemyObject.transform.position;
            playerPos = playerObject.transform.position;
            if (!enqueue)
            {
                enqueue = true;
                PathRequestManager.RequestPath(enemyPos, playerPos, OnPathFound);
            }
            // pathFound = pathfinder.FindPath(enemyPos, playerPos);
        }
    }
    public void OnPathFound(Vector2[] newPath)
    {
        path = newPath;
        // StopCoroutine("FollowPath");
        // StartCoroutine("FollowPath");

    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            Gizmos.color = Color.black;
            //Gizmos.DrawWireCube(path[i], Vector2.one);

        }
    }
}
