﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidingArrowScript : MonoBehaviour {

	const float MAX_DISTANCE_FROM_SPACESHIP = 5f;
	const float SHRINKING_DISTANCE = 25f;

	FinishedPlanetScript destinationPlanet;
	GameObject player;
	// Use this for initialization
	void Start () {
	}

	void OnEnable() {
		GamePlayManagerScript.OnGameRestartEvent += Restart;

	}

	void OnDisable(){
		GamePlayManagerScript.OnGameRestartEvent -= Restart;
	}


	void Restart() {
		if (player == null) {
			player = GamePlayManagerScript.gamePlayManager.player;
		}
		destinationPlanet = GameObject.FindObjectOfType <FinishedPlanetScript> ();
	}
	// Update is called once per frame
	void Update () {
		if (destinationPlanet != null && player != null) {
			Vector3 arrowRelativePositionToShip = (destinationPlanet.transform.position - player.transform.position);
		
		 
			transform.localScale = Vector3.one * (
				(arrowRelativePositionToShip.magnitude < SHRINKING_DISTANCE) ? 
				(arrowRelativePositionToShip.magnitude / SHRINKING_DISTANCE) : 1);

			arrowRelativePositionToShip = (arrowRelativePositionToShip.magnitude > MAX_DISTANCE_FROM_SPACESHIP) ?
				arrowRelativePositionToShip.normalized * MAX_DISTANCE_FROM_SPACESHIP :
				arrowRelativePositionToShip;

			transform.position = player.transform.position + arrowRelativePositionToShip;
			transform.position += Vector3.back * 0.2f;

			transform.rotation = Quaternion.LookRotation (Vector3.forward, arrowRelativePositionToShip);
		}

	}
}
