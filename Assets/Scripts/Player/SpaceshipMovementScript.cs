using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovementScript : MonoBehaviour {

	public enum SpaceObjectType {PLANET, SPACEJUNK, METEOR, ALIEN};
	public enum ExhaustionLevel {NONE, SMALL, LARGE};

	public enum ShipState
	{
		NORMAL, WARPING
	};

	public SpaceshipInfoScript shipInfo;

	public static event EventManagerScript.GetValueDelegate <Rigidbody2D> OnSpaceshipUpdate;

	public static event EventManagerScript.GetValueDelegate <Vector2> OnForceAppliedEvent;


	public ShipState shipState;

	// Use this for initialization
	void Start () {
		shipInfo = GameObject.FindObjectOfType <SpaceshipInfoScript> ();
	}

	void OnEnable (){
		GamePlayManagerScript.OnGameRestartEvent += Restart;


	}

	void OnDisable (){
		

	}

	void Update () {


		Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();
		ShipAnimationController shipAnimationController = GetComponent <ShipAnimationController> ();

			
		if (Input.GetMouseButton (0)) {

			Vector2 mousePositionInGame = new Vector2 (
				                              (Input.mousePosition.x - GameOptionsScript.SCREEN_CENTER_POINT.x) / GameOptionsScript.UNIT_TO_PIXEL,
				                              (Input.mousePosition.y - GameOptionsScript.SCREEN_CENTER_POINT.y) / GameOptionsScript.UNIT_TO_PIXEL);

			Vector2 thruster_force = LimitMaxSpeed (
				                         mousePositionInGame
			) ;

			Vector2 currentVelocity = rigidBody.velocity;
			float normalSpeedValue = 5f;
			float forceMultiplier = .8f;

			thruster_force = new Vector2 (
				(
				    (currentVelocity.x > normalSpeedValue && currentVelocity.x * thruster_force.x < 0) ? 
						thruster_force.x * Mathf.Abs (currentVelocity.x) * forceMultiplier : 
						thruster_force.x
				), 

				(
				    (currentVelocity.y > normalSpeedValue && currentVelocity.y * thruster_force.y < 0) ? 
						thruster_force.y * Mathf.Abs (currentVelocity.y) * forceMultiplier :
						thruster_force.y
				)
			);

			rigidBody.AddForce (thruster_force);
		//	Debug.Log (thruster_force.ToString());
//			OnForceAppliedEvent (thruster_force);
			shipInfo.fuel -= thruster_force.magnitude * Time.deltaTime * GameOptionsScript.FUEL_USAGE_MULTIPLIER;

			shipAnimationController.ChangeExhaustionFireAnimation ( 
				(shipState == ShipState.WARPING) ? ExhaustionLevel.NONE :

					( (thruster_force.magnitude < GameOptionsScript.MAX_VELOCITY) 
					? ExhaustionLevel.SMALL : ExhaustionLevel.LARGE) 
			);

			SoundManagerScript.GetInstance ().PlaySound ("Rocket Flying", transform.position, 0.6f + thruster_force.magnitude / GameOptionsScript.MAX_VELOCITY, false);

		} else {
			shipAnimationController.ChangeExhaustionFireAnimation (ExhaustionLevel.NONE);
			SoundManagerScript.GetInstance ().PlaySound ("Rocket Flying", transform.position, 0, false);
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

		transform.position = Vector3.zero;
		GetComponent <Rigidbody2D> ().velocity = Vector3.zero;
	}



}
