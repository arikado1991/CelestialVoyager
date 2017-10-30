﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovementScript : MonoBehaviour {

	public enum SpaceObjectType {PLANET, SPACEJUNK, METEOR, ALIEN};
	public enum ExhaustionLevel {NONE, SMALL, LARGE};


	bool isActive;
	public SpaceshipInfoScript shipInfo;

	public static event EventManagerScript.GetValue <Rigidbody2D> OnSpaceshipUpdate;

	public static event  EventManagerScript.GetValue <ExhaustionLevel> OnChangeExhaustion; 

	public static event EventManagerScript.GetValue <Vector2> OnForceAppliedEvent;


	// Use this for initialization
	void Start () {
		shipInfo = GameObject.FindObjectOfType <SpaceshipInfoScript> ();
	}

	void OnEnable (){
		GamePlayManagerScript.OnGameRestartEvent += Restart;


	}

	void Update () {
		if (!isActive)
			return;

		Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();

		if (Input.GetMouseButton (0)) {

			Vector2 mousePositionInGame = new Vector2 (
				                              (Input.mousePosition.x - GameOptionsScript.SCREEN_CENTER_POINT.x) / GameOptionsScript.UNIT_TO_PIXEL,
				                              (Input.mousePosition.y - GameOptionsScript.SCREEN_CENTER_POINT.y) / GameOptionsScript.UNIT_TO_PIXEL);

			Vector2 thruster_force = LimitMaxSpeed (
				                         mousePositionInGame
			) ;

			rigidBody.AddForce (thruster_force);
			Debug.Log (thruster_force.ToString());
			OnForceAppliedEvent (thruster_force);
			shipInfo.fuel -= thruster_force.magnitude * Time.deltaTime * GameOptionsScript.FUEL_USAGE_MULTIPLIER;

			OnChangeExhaustion (
				(thruster_force.magnitude < GameOptionsScript.MAX_VELOCITY) ? ExhaustionLevel.SMALL : ExhaustionLevel.LARGE);
		} else {
			OnChangeExhaustion (ExhaustionLevel.NONE);
		}

		transform.rotation = Quaternion.LookRotation (Vector3.forward, rigidBody.velocity);

		OnSpaceshipUpdate (rigidBody);


	}


	void ApplyForceToShip (Vector2 forceVector) {
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();
		rigidBody.AddForce ( forceVector);
	}

	Vector2 LimitMaxSpeed (Vector2 speed) {
		if (speed.magnitude > GameOptionsScript.MAX_VELOCITY) {
			speed =  speed.normalized * GameOptionsScript.MAX_VELOCITY;
		}
		return speed;
	}


	void Restart(){
		isActive = true;
		transform.position = Vector3.zero;
		GetComponent <Rigidbody2D> ().velocity = Vector3.zero;
	}

	void OnCollisionEnter2D (Collision2D col) {
		//death on collision with planet


	}

}
