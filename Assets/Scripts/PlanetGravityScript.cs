using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code 


public class PlanetGravityScript : MonoBehaviour {

	public float mass = 100;
	public static EventManagerScript.CollideWithObject OnSpaceshipCollisionWithPlanetEvent;
	public bool isDestinationPlanet = false;

	void OnTriggerStay2D(Collider2D c){

		Rigidbody2D spaceShipRigidBody = c.GetComponent <Rigidbody2D> ();
		float ship_mass = c.GetComponent <SpaceshipInfoScript> ().mass;
		Vector3 distantVector =  transform.position - c.GetComponent<Transform>().position;

		float gravity_force_magnitude = GameOptionsScript.G_CONSTANT * ship_mass * mass / distantVector.sqrMagnitude;
		Vector2 gravity_force = new Vector2 (distantVector.x, distantVector.y).normalized * gravity_force_magnitude ;
		spaceShipRigidBody.AddForce (gravity_force);
	}

	void OnCollisionEnter2D (Collision2D col){
		if (GetComponent<FinishedPlanetScript>() == null)	
			OnSpaceshipCollisionWithPlanetEvent ();
	}
}
