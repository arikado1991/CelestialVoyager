using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaManagerScript : MonoBehaviour {
	// Use this for initialization
	int bacteriaCount;

	int teethCount = 2;

	float spawningCooldown;


	public GameObject bacteriaPrefab;

	static Dictionary <int, GameObject> occupyingBacteria;
	static Dictionary <int, GameObject> teeth;

	void Awake () {
		spawningCooldown = 0;
		bacteriaCount = 0;

		teeth = new Dictionary <int, GameObject> ();
		foreach (GameObject tooth in GameObject.FindGameObjectsWithTag("Tooth"))
		{
			teeth.Add (teeth.Count, tooth);		
			Debug.Log ("Found one!" + teethCount.ToString());
		}

		occupyingBacteria = new Dictionary<int, GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		spawningCooldown -= Time.deltaTime;
		if (spawningCooldown <= 0 && bacteriaCount < GameOptionsScript.MAX_BACTERIA_COUNT)
			SpawnBacteria ();
	}

	public void KillBacteria(int id){

		if (occupyingBacteria.ContainsKey(id)) {
			GameObject EddStark = occupyingBacteria [id];
			GameObject.Destroy (EddStark);
			occupyingBacteria.Remove (id);

			bacteriaCount--;
		}
	}

	public void SpawnBacteria (){
			teeth [Random.Range(0, teeth.Count)].BroadcastMessage ("GetSpawnPosition");
		//Debug.Log ("Trying to spawn Bacteria");
	}

	public void AddBacteria ( Vector3 spawnPosition){
		Debug.Log ("Trying to add bacteria");

		for (int i = 0; i <= bacteriaCount; i++)
			if (!occupyingBacteria.ContainsKey (i)) {

				GameObject newBacteria = GameObject.Instantiate (bacteriaPrefab);
				newBacteria.transform.position = spawnPosition;
				newBacteria.BroadcastMessage ("SetId", i);
				occupyingBacteria.Add (i, newBacteria);
				bacteriaCount++;
				spawningCooldown = GameOptionsScript.SPAWNING_COOLDOWN_TIME - Mathf.Min(bacteriaCount,3);
				break;
			}
	}
}

