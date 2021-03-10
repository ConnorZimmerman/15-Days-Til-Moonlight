using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;
    private Rigidbody2D myRigidbody;
    public bool isWalking;
    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;
    private int walkDirection;
    public Animator anim;
    public Vector2 lastMove;
    public Collider2D walkZone;
    private bool hasWalkZone;
    public bool canMove;
    private DialogueManager theDM;
    public int directionInt;
    public bool working;
    private PlayerController thePlayer;
    private bool dialogueDirection;
    private EngagedWithPlayer playerEngagement;
    public GameObject engageWithPlayerObject;
    private GameObject enemyGameObject;
    Transform enemyTransform;

    // Use this for initialization
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();

        theDM = FindObjectOfType<DialogueManager>();

        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        engageWithPlayerObject = this.gameObject.transform.GetChild(0).gameObject;
        playerEngagement = engageWithPlayerObject.GetComponent<EngagedWithPlayer>();

        enemyGameObject = this.gameObject;
        enemyTransform = enemyGameObject.transform;

        walkCounter = Random.Range(walkTime * 0.75f, walkTime * 1.25f);
        waitCounter = Random.Range(waitTime * 0.75f, walkTime * 1.25f);

        ChooseDirection();

        if (walkZone != null)
        {
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
            hasWalkZone = true;
        }

        canMove = true;
        isWalking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerEngagement.wallBlock)
        {

            switch (walkDirection)
            {
                case 0:
                    enemyTransform.position = new Vector2(enemyTransform.position.x,
                        enemyTransform.position.y + 0.2f);
                    playerEngagement.wallBlock = false;

                    break;

                case 1:
                    enemyTransform.position = new Vector2(enemyTransform.position.x + 0.2f,
                        enemyTransform.position.y);
                    playerEngagement.wallBlock = false;

                    break;

                case 2:
                    enemyTransform.position = new Vector2(enemyTransform.position.x,
                        enemyTransform.position.y - 0.2f);
                    playerEngagement.wallBlock = false;

                    break;

                case 3:
                    enemyTransform.position = new Vector2(enemyTransform.position.x - 0.2f,
                        enemyTransform.position.y);
                    playerEngagement.wallBlock = false;

                    break;
            }
        }
    }

    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = Random.Range(walkTime * 0.25f, waitTime * 1.75f);
    }
}