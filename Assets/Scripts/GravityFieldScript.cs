using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFieldScript : MonoBehaviour {

	// Use this for initialization
	PlanetGravityScript 			planet; 
	Rigidbody2D 					shipRigidBody2D;
	SpaceshipMovementScript 		ship;
	float							orbittingVelocityMagnitude;
	float 							forceMultiplier;

	void Start () {
		
		planet = GetComponentInParent <PlanetGravityScript> ();
		float gravityFieldRadius = GetComponent<CircleCollider2D> ().radius;
		forceMultiplier = planet.mass * GameOptionsScript.G_CONSTANT / Mathf.Pow(gravityFieldRadius, 2.0f);

		orbittingVelocityMagnitude = Mathf.Sqrt (GameOptionsScript.G_CONSTANT * planet.mass / gravityFieldRadius); 

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		shipRigidBody2D = col.GetComponent<Rigidbody2D> ();
		ship = col.GetComponent<SpaceshipMovementScript> ();

	}

	void OnTriggerStay2D (Collider2D col){
		Vector2 gravityForceVector = (Vector2)  (planet.transform.position - col.transform.position);
		shipRigidBody2D.AddForce (ship.mass * forceMultiplier * gravityForceVector.normalized);
	}
}
