using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundScript : MonoBehaviour {

	const float WARNING_TIME = 3f;
	const float DRAIN_RATE = 20;
	SpaceshipInfoScript shipInfo;
	float warningTime;
	float outOfBoundDuration;
	public static event EventManagerScript.GetValueDelegate <bool> OutOfBoundEvent;
	// Use this for initialization
	void Start () {
		shipInfo = GamePlayManagerScript.GetInstance ().player.GetComponent <SpaceshipInfoScript> ();
		warningTime = -10;
		outOfBoundDuration = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (warningTime > 0)
			warningTime -= Time.deltaTime;
		else if (warningTime != -10) {
			shipInfo.fuel -= DRAIN_RATE * Time.deltaTime * outOfBoundDuration * outOfBoundDuration;
			outOfBoundDuration += Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag ("Player")) {
			warningTime = WARNING_TIME;
			Debug.Log ("Out of bound triggered");

			OutOfBoundEvent (true);
			outOfBoundDuration = 0;
		}
			
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.CompareTag ("Player")) {
			warningTime = -10;
			OutOfBoundEvent (false);

		}
	}
}
