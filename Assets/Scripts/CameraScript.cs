using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	GameObject player;

	void Start () {
		transform.position = new Vector3 (0, 0, -10);
		player = GameObject.Find ("SpaceShip");
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null)
			transform.position = player.transform.position + (Vector3.back * 10);
	}
}
