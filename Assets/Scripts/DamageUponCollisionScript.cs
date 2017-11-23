﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUponCollisionScript : MonoBehaviour {
	const float REPELLING_FORCE_MAGNITUDE = 1f;
	// Use this for initialization
	void OnCollisionEnter2D (Collision2D col) {
		Rigidbody2D collidedObjectRigidbody = col.gameObject.GetComponent<Rigidbody2D> ();
		collidedObjectRigidbody.AddForce ( (col.gameObject.transform.position - transform.position).normalized * REPELLING_FORCE_MAGNITUDE, ForceMode2D.Impulse);
			
		SpaceshipInfoScript spaceShip = col.gameObject.GetComponent <SpaceshipInfoScript> ();
		object[] parameters = { "Explosion", col.contacts [0].point };
		GameEffectManagerScript.GetInstance().CreateEffect (2, parameters);
		SoundManagerScript.GetInstance ().PlaySound ("Explosion", col.contacts[0].point);
		if (spaceShip != null) {
			spaceShip.fuel -= 10;
		}
	}
}