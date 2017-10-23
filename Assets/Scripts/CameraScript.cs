using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	// Use this for initialization

	void Start () {
		transform.position = new Vector3 (0, 0, -10);

	}

	void OnEnable() {
		SpaceshipMovementScript.OnSpaceshipChangePosition += Follow;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Follow (Vector3 position){
			transform.position = position + (Vector3.back * 10);
	}
}
