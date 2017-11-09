using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMovementScript : MonoBehaviour {

	public GameObject orbittedPlanet;
	public float orbittingSpeed;

	public bool realTime = false;

	float orbitingRadius;
	float orbittingVelocity;
	// Use this for initialization
	void Start () {
		orbitingRadius = Vector3.Distance (orbittedPlanet.transform.position, transform.position);
		orbittingVelocity = Mathf.Sqrt (GameOptionsScript.G_CONSTANT * orbittedPlanet.GetComponent <Rigidbody2D> ().mass / orbitingRadius);
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 centrifugalForce = (orbittedPlanet.transform.position - transform.position);
		Vector3 instantVelocity = new Vector3 (centrifugalForce.y, -centrifugalForce.x);
		transform.position += instantVelocity.normalized * orbittingVelocity * Time.deltaTime * orbittingSpeed *((realTime) ? 1 : 3);
		transform.position = orbittedPlanet.transform.position + (transform.position - orbittedPlanet.transform.position).normalized * orbitingRadius ;

	}

	void OnTriggerStay2D (Collider2D col) {
		return;
		Vector3 centrifugalForce = (orbittedPlanet.transform.position - transform.position);
		Vector3 instantVelocity = new Vector3 (centrifugalForce.y, -centrifugalForce.x);

		Vector3 tagAlongVelocity = orbittingSpeed *
		                           instantVelocity.normalized /
		                           Mathf.Pow (Vector3.Distance (transform.position, col.gameObject.transform.position), 2);
//		Debug.Log ("Adding force to " + col.gameObject.name + " magnitude " + tagAlongVelocity);
		col.gameObject.GetComponent<Rigidbody2D> ().AddForce ( tagAlongVelocity);
	}


}
