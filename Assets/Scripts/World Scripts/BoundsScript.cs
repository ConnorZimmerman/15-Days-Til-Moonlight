using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsScript : MonoBehaviour {

    private BoxCollider2D bounds;

    private CameraController theCamera;

    private PlayerController thePlayer;

	// Use this for initialization
	void Start () {

        bounds = GetComponent<BoxCollider2D>();
        theCamera = FindObjectOfType<CameraController>();
        theCamera.SetBounds(bounds);
        thePlayer = FindObjectOfType<PlayerController>();
        thePlayer.SetBounds(bounds);

	}
}
