using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastForwardTimeOffScreenScript : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D (Collider2D col) {
		OrbitMovementScript orbittingPlanet = col.gameObject.GetComponent <OrbitMovementScript> ();
		Debug.Log ("TimeFastforward cancelled");
		if (orbittingPlanet != null) {
			Debug.Log ("TimeFastforward cancelled");
			orbittingPlanet.realTime = true;
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		OrbitMovementScript orbittingPlanet = col.gameObject.GetComponent <OrbitMovementScript> ();
		if (orbittingPlanet != null) {
			Debug.Log ("TimeFastforward enabled");
			orbittingPlanet.realTime = false;
		}
	}
}
