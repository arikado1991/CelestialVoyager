using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollingScript : MonoBehaviour {

	SpaceshipMovementScript spaceship;

	void Start() {
		spaceship = GameObject.FindObjectOfType <SpaceshipMovementScript> ();

	}
	// Update is called once per frame
	void Update () {
		MeshRenderer meshRenderer = GetComponent<MeshRenderer> ();
		Material material = meshRenderer.material;
		transform.position = spaceship.transform.position + Vector3.forward * 10;
		material.mainTextureOffset = (Vector2) spaceship.transform.position * .1f;
	}
}
