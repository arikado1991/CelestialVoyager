using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteholeScript : MonoBehaviour {

	public GameObject blackhole;
	const float WHITEHOLE_LIFETIME = 1.5f;
	const float FADING_SPEED = 1;
	const float APPEARING_SPEED = 10f;

	SpriteRenderer whiteholeRenderer;

	bool appearing;

	GameObject spaceShip;
	float lifeTime;

	// Use this for initialization
	void Start () {
		whiteholeRenderer = GetComponent <SpriteRenderer> ();
		spaceShip = GamePlayManagerScript.GetInstance ().player;
		appearing = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (appearing) {
			whiteholeRenderer.color = new Color (
				whiteholeRenderer.color.r,
				whiteholeRenderer.color.g,
				whiteholeRenderer.color.b,
				whiteholeRenderer.color.a  + Time.deltaTime * APPEARING_SPEED);
			
			if (whiteholeRenderer.color.a >= 1) {
				appearing = false;
				spaceShip.GetComponent<Renderer> ().enabled = true;
				//spaceShip.GetComponent<Collider2D> ().enabled = true;
				spaceShip.GetComponent<Rigidbody2D> ().WakeUp();
				spaceShip.GetComponent<Rigidbody2D> ().isKinematic = false;
			}

		
		} else {
			whiteholeRenderer.color = new Color (
				whiteholeRenderer.color.r,
				whiteholeRenderer.color.g,
				whiteholeRenderer.color.b,
				whiteholeRenderer.color.a  - Time.deltaTime * FADING_SPEED);
			if (whiteholeRenderer.color.a < 0)
				gameObject.SetActive (false);
		}
	}

	void OnEnable() {
		
		appearing = true;
		if (whiteholeRenderer != null)
			whiteholeRenderer.color = new Color (
				whiteholeRenderer.color.r,
				whiteholeRenderer.color.g,
				whiteholeRenderer.color.b,
				0);
		spaceShip = GamePlayManagerScript.GetInstance ().player;
	}
}
