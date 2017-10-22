using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedPlanetScript : MonoBehaviour {

	public static event EventManagerScript.GameDelegate OnReachingDestinationEvent;

	// Use this for initialization
	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.CompareTag ("Player"))
			OnReachingDestinationEvent ();
	}
}
