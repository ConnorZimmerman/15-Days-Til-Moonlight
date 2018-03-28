using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {
    private PlayerController thePlayer;
    private CameraController theCamera;
    public Vector2 startDirection;
    public string startPoint;
    private GlobalDataScript globalData;

    // Use this for initialization
    void Start() {
        thePlayer = FindObjectOfType<PlayerController>();

        // if(thePlayer.startPoint == pointName)
        // {
            globalData = FindObjectOfType<GlobalDataScript>();
            startPoint = globalData.globalPlayerStartPoint;
            // thePlayer.transform.position = GameObject.Find(startPoint).transform.position;
            // thePlayer.transform.position = transform.position;

            theCamera = FindObjectOfType<CameraController>();
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y,
                theCamera.transform.position.z);
        // }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
