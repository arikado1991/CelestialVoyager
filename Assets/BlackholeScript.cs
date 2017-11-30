using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeScript : MonoBehaviour {

	public GameObject whiteHole;
	GameObject spaceShip;

	const float WARP_TIME = 3f;
	const float WHITEHOLE_APPEAR_CUE = .5f;

	float warptime;

	Vector3 warpingVelocity;

	// Use this for initialization
	void Start () {
		warpingVelocity = (whiteHole.transform.position - transform.position) / WARP_TIME;
		warptime = 0;
		whiteHole = transform.Find ("Whitehole").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (spaceShip != null) {
			if (warptime > 0) {			

				GamePlayManagerScript.GetInstance ().player.transform.position += warpingVelocity * Time.deltaTime;
				warptime -= Time.deltaTime;
			} else if (warptime != -100) {
				Debug.Log("Should have active whitehole");

					whiteHole.SetActive (true);
					spaceShip = null;
				warptime = -100;
			} 
		}

	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.CompareTag ("Player")){
			Debug.Log ("Collided with blackhole");
			spaceShip = col.gameObject;
			spaceShip.transform.position = transform.position;
			spaceShip.GetComponent<Renderer> ().enabled = false;
			//spaceShip.GetComponent<Collider2D> ().enabled = true;
			spaceShip.GetComponent<Rigidbody2D> ().isKinematic = true;
			spaceShip.GetComponent<Rigidbody2D> ().Sleep();
			spaceShip.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			warptime = WARP_TIME;
		

		}
	}


}
