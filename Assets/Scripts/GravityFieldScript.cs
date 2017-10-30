using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFieldScript : MonoBehaviour {

	// Use this for initialization
	PlanetGravityScript 			planet; 
	Rigidbody2D 					shipRigidBody2D;
	SpaceshipInfoScript 		ship;
	float							orbittingVelocityMagnitude;
	float 							forceMultiplier;
	ForceVectorDisplayManager forceVectorDisplayManager;



	void Start () {
		
		planet = GetComponentInParent <PlanetGravityScript> ();
		float gravityFieldRadius = GetComponent<CircleCollider2D> ().radius;
		forceMultiplier = planet.mass * GameOptionsScript.G_CONSTANT / Mathf.Pow(gravityFieldRadius, 2.0f);

		orbittingVelocityMagnitude = Mathf.Sqrt (GameOptionsScript.G_CONSTANT * planet.mass / gravityFieldRadius);
		forceVectorDisplayManager = GameObject.FindObjectOfType<ForceVectorDisplayManager>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		shipRigidBody2D = col.GetComponent<Rigidbody2D> ();
		ship = col.GetComponent<SpaceshipInfoScript> ();

	

	}

	void OnTriggerStay2D (Collider2D col){
		Vector2 gravityForceVector = (Vector2)  (planet.transform.position - col.transform.position);
		gravityForceVector = ship.mass * forceMultiplier * gravityForceVector.normalized;
		shipRigidBody2D.AddForce (gravityForceVector);
		//OnUnderGravityFromPlanetEvent (name, gravityForceVector);
	}

}
