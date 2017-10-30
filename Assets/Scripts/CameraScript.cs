using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	// Use this for initialization


	void Start () {
		transform.position = new Vector3 (0, 0, -10);

	}

	void OnEnable() {
		SpaceshipMovementScript.OnSpaceshipUpdate += Follow;

	}

	void OnDisable() {
		SpaceshipMovementScript.OnSpaceshipUpdate -= Follow;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Follow (Rigidbody2D ship){
		transform.position = (Vector3) ship.position + (Vector3.back * 
			Mathf.Min (
				GameOptionsScript.MIN_CAMERA_DISTANCE + ship.velocity.magnitude * 0.5f, 
				GameOptionsScript.MAX_CAMERA_DISTANCE));
	}


}
