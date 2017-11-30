using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUponCollisionScript : MonoBehaviour {
	const float REPELLING_FORCE_MAGNITUDE = 1f;
	public int damage;
	public bool destroyUponImpact = false;
	// Use this for initialization

	void OnCollisionEnter2D (Collision2D col) {
		Rigidbody2D collidedObjectRigidbody = col.gameObject.GetComponent<Rigidbody2D> ();
		collidedObjectRigidbody.AddForce ( 
			(col.gameObject.transform.position - transform.position).normalized 
			* REPELLING_FORCE_MAGNITUDE, ForceMode2D.Impulse);
			
		if (col.contacts.Length > 0) {
			object[] parameters = { "Explosion", col.contacts [0].point };
			GameEffectManagerScript.GetInstance ().CreateEffect (2, parameters);
			SoundManagerScript.GetInstance ().PlaySound ("Explosion", col.contacts[0].point);
		}

		if (col.gameObject.CompareTag("Player")) {
			SpaceshipInfoScript spaceShip = col.gameObject.GetComponent <SpaceshipInfoScript> ();
			spaceShip.fuel -= damage;
		}

		if (destroyUponImpact)
			GameObject.Destroy (gameObject);


	}
}
