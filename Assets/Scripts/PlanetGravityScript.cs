using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravityScript : MonoBehaviour {

	public float mass = 100;
	public float orbiting_velocity;
	public bool is_being_orbited = false;
	float updateOrbitStatus;
	// Use this for initialization
	void Start () {
	//	orbiting_velocity = sqrt
		updateOrbitStatus = 0;
	}
	
	// Update is called once per frame
	void Update () {
		updateOrbitStatus -= Time.deltaTime;
	}



	void OnTriggerStay2D(Collider2D c){
		Debug.Log ("Gravity is on!");


			

		Rigidbody2D spaceShipRigidBody = c.GetComponent <Rigidbody2D> ();
		float ship_mass = c.GetComponent <SpaceshipMovementScript> ().mass;
		Vector3 distantVector =  transform.position - c.GetComponent<Transform>().position;


			
		float gravity_force_magnitude = GameOptionsScript.G_CONSTANT * ship_mass * mass / distantVector.sqrMagnitude;

		Vector2 gravity_force = new Vector2 (distantVector.x, distantVector.y).normalized * gravity_force_magnitude ;

		orbiting_velocity = Mathf.Sqrt (GameOptionsScript.G_CONSTANT * mass / distantVector.magnitude); 

		//is_being_orbited = c.GetComponent <SpaceshipMovementScript> ().orbitting;
		/*spaceShipRigidBody.velocity = closeEnough (spaceShipRigidBody.velocity, new Vector2 (distantVector.x, distantVector.y) );
		c.GetComponent <SpaceshipMovementScript> ().orbitting = is_being_orbited;
		*/
			spaceShipRigidBody.AddForce (gravity_force);
		




	//	Debug.Log (spaceShipRigidBody.velocity);
	}
	/*
	Vector2 closeEnough(Vector2 speed, Vector2 p){


		return speed;
		
		Vector2 orbiting_velocity_vector = new Vector3 (-p.y, p.x);

			

		orbiting_velocity_vector = orbiting_velocity_vector.normalized * orbiting_velocity;

				if (
			( Mathf.Abs (1 - speed.x/orbiting_velocity_vector.x ) < .2f
				&& Mathf.Abs (1 - speed.y/orbiting_velocity_vector.y ) < .2f
			|| 	( Mathf.Abs (1 + speed.x/orbiting_velocity_vector.x) < 0.2f
					&& Mathf.Abs (1 + speed.y + orbiting_velocity_vector.y) < 0.2f)) ) {
			Debug.Log ("Orbitting");
			if (updateOrbitStatus < 0) {
				is_being_orbited = true;
				updateOrbitStatus = GameOptionsScript.UPDATE_RATE;
			}
			Debug.Log (orbiting_velocity_vector);
			return orbiting_velocity_vector * orbiting_velocity;
		}
		if (is_being_orbited)
			Debug.Log ("Still orbiting");
		return orbiting_velocity_vector * orbiting_velocity ;
		return speed;

	}*/
}
