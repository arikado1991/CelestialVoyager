using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothCollisionDetectionScript : MonoBehaviour {

	public GameObject bubbleGenerator;
	private float cooldown;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision collision) {
		//Debug.Log ("Brushing");

		foreach (ContactPoint contact in collision.contacts)
			if (collision.gameObject.CompareTag ("Brush")) {
				GameObject bubbleCloud = GameObject.Instantiate (bubbleGenerator);
				bubbleCloud.transform.position = contact.point;
			}
				//Debug.DrawRay(contact.point, contact.normal, Color.white);

		//if (collision.relativeVelocity.magnitude > 2)
		//	audioSource.Play();
	}
}
