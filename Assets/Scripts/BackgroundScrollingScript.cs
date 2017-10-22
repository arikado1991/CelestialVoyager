using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollingScript : MonoBehaviour {



	void Start() {


	}

	void OnEnable(){
		SpaceshipMovementScript.OnSpaceshipChangePosition += FollowSpaceship;
	}
	// Update is called once per frame
	void Update () {

	}

	void FollowSpaceship(Vector3 shipPosition){
		MeshRenderer meshRenderer = GetComponent<MeshRenderer> ();
		Material material = meshRenderer.material;
		transform.position = shipPosition + Vector3.forward * 10;
		material.mainTextureOffset = (Vector2) shipPosition * .1f;
	}
}
