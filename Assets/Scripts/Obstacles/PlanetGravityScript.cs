﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code 


public class PlanetGravityScript : MonoBehaviour {

	public float mass = 100;
	public static event  EventManagerScript.GameDelegate OnSpaceshipCollisionWithPlanetEvent;
	public static event EventManagerScript.GetValueDelegate <Vector3> OnExplosiveContactEvent;

	public delegate void OnGravityFromPlanetDelegate (string planetName, Vector2 gravityForceVector);

	static public OnGravityFromPlanetDelegate OnUnderGravityFromPlanetEvent;

	void OnTriggerStay2D(Collider2D c){

		Rigidbody2D spaceShipRigidBody = c.GetComponent <Rigidbody2D> ();
		float ship_mass = c.GetComponent <SpaceshipInfoScript> ().mass;

		Vector3 distantVector =  transform.position - c.GetComponent<Transform>().position;

		float gravity_force_magnitude = GameOptionsScript.G_CONSTANT * ship_mass * mass / distantVector.sqrMagnitude;
		Vector2 gravity_force = new Vector2 (distantVector.x, distantVector.y).normalized * gravity_force_magnitude ;
		spaceShipRigidBody.AddForce (gravity_force);
		OnUnderGravityFromPlanetEvent (name, gravity_force);
	}

	void OnCollisionEnter2D (Collision2D col){
		if (GetComponent<FinishedPlanetScript> () == null) {

			//OnExplosiveContactEvent (col.gameObject.transform.position);

			object[] parameters = { "Explosion", col.contacts [0].point };
			GamePlayManagerScript.gamePlayManager.effectManager.CreateEffect (2, parameters);
				//if (pt != null)

			OnSpaceshipCollisionWithPlanetEvent ();
		}
	}
}
