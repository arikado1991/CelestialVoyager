using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundScript : MonoBehaviour {

	const float WARNING_TIME = 1.5f;
	const float DRAIN_RATE = 150;
	SpaceshipInfoScript shipInfo;
	float warningTime;
	// Use this for initialization
	void Start () {
		shipInfo = GamePlayManagerScript.GetInstance ().player.GetComponent <SpaceshipInfoScript> ();
		warningTime = -10;
	}
	
	// Update is called once per frame
	void Update () {
		if (warningTime > 0)
			warningTime -= Time.deltaTime;
		else if (warningTime != -10)
			shipInfo.fuel -= DRAIN_RATE * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag ("Player")) {
			warningTime = WARNING_TIME;
			Debug.Log ("Out of bound triggered");
		}
			
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.CompareTag ("Player"))
			warningTime = -10;
	}
}
