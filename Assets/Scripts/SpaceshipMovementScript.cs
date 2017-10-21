using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovementScript : MonoBehaviour {



	bool isActive;
	public float mass = Mathf.Pow(1, -20);

	public float remainingFuel;


	// Use this for initialization
	void Start () {
		Restart ();
	
	}



	// Update is called once per frame
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


			rigidBody.AddForce ( thruster_force);

			remainingFuel -= thruster_force.magnitude * Time.deltaTime * 10;
			GameObject.FindObjectOfType <GamePlayInfoScript> ().BroadcastMessage ("SetFuel", (int)remainingFuel);


		} 
			
		transform.rotation = Quaternion.LookRotation (Vector3.forward, rigidBody.velocity);

		if (remainingFuel <= 0)
			GameObject.FindObjectOfType <GamePlayManagerScript> ().GameOver ();

	}

	void OnCollisionEnter2D (Collision2D col) {
		//death on collision with planet
		remainingFuel = 0;
	}

	public void Restart(){
		isActive = true;
		transform.position = Vector3.zero;
		remainingFuel = GameOptionsScript.MAX_FUEL_AMOUNT;
		GetComponent <Rigidbody2D> ().velocity = Vector3.zero;
	}



}
