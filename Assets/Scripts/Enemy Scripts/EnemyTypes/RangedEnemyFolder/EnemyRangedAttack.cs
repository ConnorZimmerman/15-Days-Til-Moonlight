using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    private BasicRangedEnemy rangedEnemy;
    private EngagedWithPlayer playerEngagement;
    private WayPoints waypoints;
    public float attackRange;
    public GameObject playerObject;
    public GameObject projectile;
    public float throwForce;
    //public bool targetInSight;
    public float timeUntilAttack;
    private GameObject enemyObject;
    public GameObject rotatingObject;
    public bool inRange;
    public bool on;
    public float distanceToPlayer;
    private float directionOfRay;
    public bool lineOfSight;
    //private List<Transform> wayPointPath;
    public Transform waypointPath;
    private Pathfinding pathfinder;
    private Vector2 enemyPos;
    private Vector2 playerPos;
    private float pathfinderTimer;
    //public List<Node> pathFound;
    public Vector2[] path;
    public bool enqueue;
    public Vector3 targetDir;
    // Use this for initialization
    void Start()
    {
        playerEngagement = FindObjectOfType<EngagedWithPlayer>();
        playerObject = GameObject.Find("Player");
        enemyObject = this.gameObject;
        rangedEnemy = enemyObject.GetComponent<BasicRangedEnemy>();
        rotatingObject = this.gameObject.transform.GetChild(8).gameObject;
        throwForce = 255;
        inRange = false;
        lineOfSight = true;

        //waypoints = enemyObject.GetComponent<WayPoints>();
        pathfinder = FindObjectOfType<Pathfinding>();

        pathfinderTimer = 0;

        enqueue = false;

        attackRange = 8;

        // player_layer_mask = LayerMask.GetMask("Player");
        // wall_layer_mask = LayerMask.GetMask("Wall");
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if player is within attack range
        distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);
        if (timeUntilAttack > 0)
        {
            timeUntilAttack -= Time.deltaTime;
        }

        if (distanceToPlayer < attackRange)
        {
            inRange = true;

            if (!rangedEnemy.deathSeven)
            {

                //Turn towards target (object within enemy is turning towards player)
                targetDir = playerObject.transform.position - rotatingObject.transform.position;
                float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f; //-90f (for enemy direction)
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                rotatingObject.transform.rotation = Quaternion.RotateTowards(rotatingObject.transform.rotation, q, 90 * Time.deltaTime);


                //Check to see if it's time to attack
                if (timeUntilAttack <= 0 && rangedEnemy.following)
                {
                    //Raycast to see if there is line of sight to target
                    RaycastHit2D hit = Physics2D.Raycast(rotatingObject.transform.position, targetDir, distanceToPlayer, 1 << 8 | 1 << 9);
                    // if (hit.collider.tag != null)
                    // {
                    Debug.Log(hit.collider.tag);
                    if (hit.collider.tag == "Player")
                    {
                        lineOfSight = true;
                        GameObject newKnife = Instantiate(projectile, rotatingObject.transform.position, rotatingObject.transform.rotation);
                        newKnife.GetComponent<RangedDamage>().targetDir = targetDir;
                        newKnife.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, throwForce));
                        timeUntilAttack = 2;
                    }
                    // }
                    else
                    {
                        lineOfSight = false;
                        enemyPos = enemyObject.transform.position;
                        playerPos = playerObject.transform.position;
                        if (!enqueue)
                        {
                            enqueue = true;
                            PathRequestManager.RequestPath(enemyPos, playerPos, OnPathFound);
                            enqueue = false;
                        }
                        // }
                    }
                }
            }
        }
        else
        {
            inRange = false;
        }
    }


    public void OnPathFound(Vector2[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            // StopCoroutine("FollowPath");
            // StartCoroutine("FollowPath");
        }
    }
    // public void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         playerEngagement.doingDamage();
    //         on = true;
    //     }
    // }
    // public void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         playerEngagement.doingDamage();
    //         on = false;
    //     }
    // }
}