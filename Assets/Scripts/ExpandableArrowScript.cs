using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandableArrowScript : MonoBehaviour {

	float ARROW_SCALE = 0.5f;
	public enum ForceType {GRAVITY, THRUSTER, EXTERNAL};
	const float MIN_ARROW_LENGTH = 0.5f;
	const float MAX_ARROW_LENGTH = 4f;

	GameObject head;
	GameObject body;
	Vector2 forceVector;

	FollowScript followShipScript;
	FadeScript fadeScript;


	public void SetForce (Vector2 forceVector) {
		this.forceVector = forceVector;
		Display ();
		StartFading (0.5f, 1 + forceVector.magnitude/GameOptionsScript.MAX_VELOCITY);
	}

	// Use this for initialization
	void Awake () {
		
		body = transform.Find ("ForceArrowBody").gameObject;
		head = transform.Find ("ForceArrowHead").gameObject;

		fadeScript = GetComponent <FadeScript> ();

		StartFading (0,0);

	}

	public void SetFollowShip(){
		followShipScript = GetComponent<FollowScript> ();
		Debug.Log("PLayer = " + GameObject.FindObjectOfType<GamePlayManagerScript>().player.name);
		followShipScript.SetTarget (GameObject.FindObjectOfType<GamePlayManagerScript>().player);
		followShipScript.SetDistance (Vector3.back * -0.2f);
	}

	void OnEnable (){
		GamePlayManagerScript.OnGameRestartEvent += SetFollowShip;

	}

	void OnDisable (){
		GamePlayManagerScript.OnGameRestartEvent -= SetFollowShip;

	}
	// Update is called once per frame
	void Update () {

	}

	void StartFading(float duration, float alpha){
		fadeScript.ActivateFading (duration, alpha);
	}

	void Display(){
		transform.rotation = Quaternion.LookRotation (Vector3.forward, forceVector);
		float scale = Mathf.Min (MAX_ARROW_LENGTH, Mathf.Max (forceVector.magnitude * ARROW_SCALE, MIN_ARROW_LENGTH));
		transform.localScale = Vector3.one * scale;


	}
}
