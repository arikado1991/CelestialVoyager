using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawnerScript : MonoBehaviour {

	const float METEOR_SHOWER_LIFE_TIME = 3f;
	const float METEOR_SPAWN_RATE = .5f;
	const float SPAWN_LOCATION_HORIZONTAL_MARGIN = 12f;
	const float SPAWN_LOCATION_VERTICAL_DISTANCE = 10f;
	const float METEOR_SPEED = 20f;
	const float WARNING_TIME = 2.5f;
	const float NO_METEOR_TIME = 10f;

	public enum MeteorShowerState {INACTIVE, PEACEFUL, WARNING, SHOWER};

	public MeteorShowerState state;


	public GameObject meteorPrefab;
	public float spawnCooldown;
	public float stateDuration;



	Transform target;

	float lifeTime;
	// Use this for initialization
	void Start () {


		state = MeteorShowerState.INACTIVE;
		target = transform.Find ("Target");
		if (target == null)
			Debug.LogError ("[MeteorSpawnerScript] - Cannot find object 'Target'");
	}
/*
	void OnEnable() {

		GamePlayManagerScript.OnGameRestartEvent += Restart();
		lifeTime = 0;
	}

	void OnDisable() 
	{

		GamePlayManagerScript.OnGameRestartEvent -= Restart();
	}

*/

	// Update is called once per frame
	void FixedUpdate () {

		switch (state) {

		case MeteorShowerState.INACTIVE:
			
			break;
		
		case MeteorShowerState.PEACEFUL:
			if (stateDuration <= 0) {
				state = MeteorShowerState.WARNING;
				stateDuration = WARNING_TIME;
			}

			break;
		case MeteorShowerState.WARNING:
			if (spawnCooldown <= 0) {

				SpawnMeteor (true);
				spawnCooldown = METEOR_SPAWN_RATE * 3 + Random.value * (Random.Range (0, 1) * 2 - 1);
			} else {
				spawnCooldown -= Time.fixedDeltaTime;
			}
		
			if (stateDuration <= 0) { 
				state = MeteorShowerState.SHOWER;
				stateDuration = METEOR_SHOWER_LIFE_TIME;
				spawnCooldown = 0;
			}
			break;
		case MeteorShowerState.SHOWER:
			
			if (spawnCooldown <= 0) {

				SpawnMeteor ();
				spawnCooldown = METEOR_SPAWN_RATE + Random.value * (Random.Range (0, 1) * 2 - 1);
			} else {
				spawnCooldown -= Time.fixedDeltaTime;
			}
			if (stateDuration <= 0) {
				state = MeteorShowerState.PEACEFUL;
				stateDuration = NO_METEOR_TIME;
			}
			break;
		}

		if (stateDuration > 0 && state != MeteorShowerState.INACTIVE)
			stateDuration -= Time.fixedDeltaTime;

	}

	void SpawnMeteor(bool isWarningShot = false) {
		GameObject newMeteor = GameObject.Instantiate (meteorPrefab);
		newMeteor.transform.parent = transform;


		Vector3 playerPosition = GamePlayManagerScript.GetInstance ().player.transform.position;
		newMeteor.transform.localPosition = playerPosition
			+ new Vector3 (
				(Random.Range (0, 1) * 2 - 1) * Random.value * SPAWN_LOCATION_HORIZONTAL_MARGIN, 
				SPAWN_LOCATION_VERTICAL_DISTANCE,
				( (isWarningShot) ? 9.5f : 0) 
			);

		if (isWarningShot) {
			newMeteor.transform.localScale *= .5f;
			newMeteor.GetComponent<Collider2D> ().enabled = false;
		}
	
		MeteorScript meteorScript = newMeteor.GetComponent <MeteorScript> ();
		meteorScript.direction = (target.position - transform.position);


		//Debug.Log ("Meteor direction " + meteorScript.direction.ToString ());
		meteorScript.speed = (isWarningShot) ? METEOR_SPEED * .5f : METEOR_SPEED ;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag ("Player") ) {
			state = MeteorShowerState.WARNING;
			stateDuration = WARNING_TIME;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		Debug.Log ("Trigger inactive");
		if (col.gameObject.CompareTag ("Player"))
			state = MeteorShowerState.INACTIVE;
	}
}
