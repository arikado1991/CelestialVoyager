using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnerScript : MonoBehaviour {

	public GameObject[] fish_sprite;
	float spawning_cool_down_time = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		spawning_cool_down_time -= Time.deltaTime;
		if (spawning_cool_down_time <= 0) {
			SpawnFish ();
//			spawning_cool_down_time = GameOptionsScript.SPAWNING_COOLDOWN_TIME - Random.value * 3;
		}
	}

	void SpawnFish(){
		GameObject newFish = GameObject.Instantiate(fish_sprite[Random.Range (0, fish_sprite.Length)]);
		newFish.transform.SetParent (GameObject.Find ("Canvas").transform);
//		GameObject.Find ("Canvas").
	}
}
