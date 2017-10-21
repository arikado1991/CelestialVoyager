using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaSpawner : MonoBehaviour {

	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

			
	}

	public void  GetSpawnPosition ()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, 
			new Vector3( 	Random.Range(-50, 50) * 0.01f, 
							Random.Range(-50, 50) * 0.01f,
							Random.Range(-50, 0)  * 0.01f),
			out hit, 10.0f) 
		)
		
		{

			GameObject.FindObjectOfType<BacteriaManagerScript> ().BroadcastMessage("AddBacteria", hit.point);
		}
	}




}
