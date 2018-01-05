using UnityEngine;
using System.Collections;

public class DemoStairs : MonoBehaviour {
	public GameObject teleportPoint; //The point which we'll teleport to (GameObject).

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") //If an object with the Player tag enters the trigger, move to the teleport point.
		{
			other.transform.position = teleportPoint.transform.position;
		}
	}
}
