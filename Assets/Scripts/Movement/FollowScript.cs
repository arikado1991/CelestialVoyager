using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {

	public GameObject target;
	Vector3 distanceVector;

	// Use this for initialization
	void Start () {
		


	}

	public void SetTarget(GameObject newTarget) {
		target = newTarget;
		//Debug.Log ("Followee is " + newTarget.name);
	}

	public void SetDistance (Vector3  newDistanceVector){
		distanceVector = newDistanceVector;

	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {

			return;
		}			
		transform.position = target.transform.position + distanceVector;
	
	}
}
