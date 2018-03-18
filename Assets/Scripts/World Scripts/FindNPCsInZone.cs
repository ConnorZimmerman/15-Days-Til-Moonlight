// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FindNPCsInZone : MonoBehaviour {

// 	// Use this for initialization
// 	void Start () {
		
// 	}
	
// 	// Update is called once per frame
// 	void Update () {
		
// 	}
// 	public void OnTriggerStay2D(Collider2D other)
//     {
// 		if(other.gameObject.tag == "Enemy"){
// 			TrackingRaycast enemyRaycast = other.gameObject.GetComponent<TrackingRaycast>();
// 			// enemyRaycast.pathRequest = this.gameObject.GetComponent<PathRequestManager>();
// 			enemyRaycast.pathfinder = this.gameObject.GetComponent<Pathfinding>();
// 		}
// 	}
// }
