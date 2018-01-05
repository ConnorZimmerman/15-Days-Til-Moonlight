using UnityEngine;
using System.Collections;

public class SpriteSorter : MonoBehaviour {
	//This script automatically sorts sprites depending on Y-position. This is very useful for games that use a top-down perspective, like the old Final Fantasy games.
	//Note that sorting requires the object to be on a Sorting Layer separate from the background. Otherwise objects may render behind scenery.
	public float sortOrderOffset; //Value with which to offset the automatic order in layer. Positive number means object gets rendered in front earlier and vice versa.
	private float zSort; //This value will be used in Start to set the Z-position of the 2D object (for sprite sorting.)
	private Vector3 newPos = new Vector3(0.0f, 0.0f, 0.0f); //The new position vector which we pass to the object's actual position.

	void Start() {
		zSort = (transform.position.y / 10) - sortOrderOffset;
		newPos = new Vector3(transform.position.x, transform.position.y, zSort);
		transform.position = newPos;
		Destroy(this);
	}
}
