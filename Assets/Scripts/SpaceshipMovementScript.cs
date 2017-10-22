using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovementScript : MonoBehaviour {

	public enum SpaceObjectType {PLANET, SPACEJUNK, METEOR, ALIEN};


	bool isActive;
	public SpaceshipInfoScript shipInfo;

	public static EventManagerScript.GetValue <Vector3> OnSpaceshipChangePosition;


	// Use this for initialization
	void Start () {
		shipInfo = GameObject.FindObjectOfType <SpaceshipInfoScript> ();

		Restart ();

	
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

			Vector2 thruster_force = new Vector2 (
				mousePositionInGame.x, 
				mousePositionInGame.y 
			);

			Debug.Log ("Force magnitude: " + thruster_force.magnitude);
			Debug.Log ("Ship info is " + rigidBody != null);
			rigidBody.AddForce ( thruster_force);
		//	ApplyForceToShip(thruster_force);

		 	shipInfo.fuel -= thruster_force.magnitude * Time.deltaTime * 10;



		} 

		transform.rotation = Quaternion.LookRotation (Vector3.forward, rigidBody.velocity);

		OnSpaceshipChangePosition (transform.position);

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

	// Update is called once per frame




	void Restart(){
		isActive = true;
		transform.position = Vector3.zero;
		GetComponent <Rigidbody2D> ().velocity = Vector3.zero;
	}

	void OnCollisionEnter2D (Collision2D col) {
		//death on collision with planet


	}

}
