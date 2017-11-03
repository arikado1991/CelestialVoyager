using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollingScript : MonoBehaviour {



	void Start() {


	}

	void OnEnable(){
		SpaceshipMovementScript.OnSpaceshipUpdate += FollowSpaceshipAndScrollTextures;
	}

	void OnDisable(){
		SpaceshipMovementScript.OnSpaceshipUpdate -= FollowSpaceshipAndScrollTextures;
	}
	// Update is called once per frame
	void Update () {

	}

	void FollowSpaceshipAndScrollTextures(Rigidbody2D ship){
		MeshRenderer meshRenderer = GetComponent<MeshRenderer> ();
		Material material = meshRenderer.material;
		transform.position = (Vector3) ship.position + Vector3.forward * 10;
		material.mainTextureOffset = (Vector2) ship.position * .05f;
	}
}
