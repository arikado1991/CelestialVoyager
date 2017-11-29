using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour {
	

	const float MEOTEOR_LIFE_TIME = 3f;

	public Vector2 direction;
	public float speed;
	float lifeTime;

	Transform target;


	// Use this for initialization
	void Start () {
		//transform.LookAt (velocity);
		target = transform.Find ("Target");
		lifeTime = MEOTEOR_LIFE_TIME;
		transform.rotation = Quaternion.LookRotation (Vector3.forward, direction);
		DamageUponCollisionScript damageBehavior = GetComponent<DamageUponCollisionScript> ();
		if (damageBehavior != null){
			damageBehavior.damage = 50;
			damageBehavior.destroyUponImpact = true;
		}
		else
			Debug.LogError ("Meteor script: - No DamageUpconCollisionScript found");
	}


	
	// Update is called once per frame
	void Update () {
		lifeTime -= Time.deltaTime;
		if (lifeTime < 0)
			GameObject.Destroy (gameObject);
		transform.position += transform.up * speed * Time.deltaTime;

	}



}
		