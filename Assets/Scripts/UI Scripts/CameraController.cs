using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject followTarget;

    private Vector3 targetPos;

    public float moveSpeed;

    private static bool cameraExists;

    public BoxCollider2D boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;

    public GameObject cameraBoundObject;

    // Use this for initialization
    void Start () {

        followTarget = GameObject.Find("Player");

        cameraBoundObject = GameObject.Find("Bounds");

        if(boundBox == null)
        {
            // boundBox = FindObjectOfType<BoundsScript>().GetComponent<BoxCollider2D>();
            boundBox = cameraBoundObject.GetComponent<BoxCollider2D>();
            minBounds = boundBox.bounds.min;
            maxBounds = boundBox.bounds.max;
        }

        if (!cameraExists)
        {
            cameraExists = true;
            //DontDestroyOnLoad(transform.gameObject);
        }

        else
        {
            //Destroy(gameObject);
        }

        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;

        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update () {
        targetPos = new Vector3(followTarget.transform.position.x, 
            followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, 
            targetPos, moveSpeed * Time.deltaTime);

        float clampX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth,
            maxBounds.x - halfWidth);
        float clampY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight,
            maxBounds.y - halfHeight);
        transform.position = new Vector3(clampX, clampY, transform.position.z);
    }

    public void SetBounds(BoxCollider2D newBounds)
    {
        boundBox = newBounds;

        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
    }
       
}
