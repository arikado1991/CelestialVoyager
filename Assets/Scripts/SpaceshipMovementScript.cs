using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovementScript : MonoBehaviour {



	bool isActive;
	public float mass = Mathf.Pow(1, -20);
	Vector2 speed;
	public bool orbitting = false;

	public float remainingFuel;


	// Use this for initialization
	void Start () {
		Restart ();
	
	}



	// Update is called once per frame
	void Update () {
		if (!isActive)
			return;

		if (Input.GetMouseButton (0)) {

			orbitting = false;

			Vector2 mouse_position_in_game = new Vector2 (
				(Input.mousePosition.x - GameOptionsScript.SCREEN_CENTER_POINT.x) / GameOptionsScript.UNIT_TO_PIXEL,
				(Input.mousePosition.y - GameOptionsScript.SCREEN_CENTER_POINT.y) / GameOptionsScript.UNIT_TO_PIXEL);

			speed = new Vector2 (
				mouse_position_in_game.x, //- transform.position.x,
			
				mouse_position_in_game.y //- transform.position.y
			);
			

			if (speed.x < 0)
				this.GetComponentInParent<SpriteRenderer> ().flipX = true;
			else if (speed.x > 0)
				this.GetComponentInParent<SpriteRenderer> ().flipX = false;



			//Debug.Log ("Speed: " + speed.ToString());
			//Debug.Log (Input.mousePosition);
		} else {
			speed = Vector2.zero;
		}

		Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();


		rigidBody.AddForce (speed );
		transform.rotation = Quaternion.LookRotation (Vector3.forward, rigidBody.velocity);


		remainingFuel -= speed.magnitude * Time.deltaTime * 10;
		GameObject.FindObjectOfType <GamePlayInfoScript> ().BroadcastMessage ("SetFuel", (int)remainingFuel);




		if (remainingFuel <= 0)
			GameObject.FindObjectOfType <GamePlayManagerScript> ().GameOver ();
		//GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, -2.0f);
	}

	void OnCollisionEnter2D (Collision2D col) {
		remainingFuel = 0;
	}

	public void Restart(){
		isActive = true;
		speed = Vector2.zero;
		transform.position = Vector3.zero;
		remainingFuel = GameOptionsScript.MAX_FUEL_AMOUNT;
		GetComponent <Rigidbody2D> ().velocity = Vector3.zero;
	}



}
