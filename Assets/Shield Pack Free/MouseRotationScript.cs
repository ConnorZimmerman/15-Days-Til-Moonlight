using UnityEngine;
using System.Collections;

public class MouseRotationScript : MonoBehaviour {
	
	void Update () {
		transform.localRotation = Quaternion.Euler (transform.localRotation.eulerAngles.x, (Input.mousePosition.x / Screen.width) * 360 +180 , transform.localRotation.eulerAngles.z);

	}
}
