using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool {

	GameObject objectPrefab;

	public Dictionary <string, GameObject> deployedObjects;
	public Queue inactiveObjects;

	ObjectPool() {
		deployedObjects = new Dictionary <string, GameObject>();
		inactiveObjects = new Queue();
	}

	GameObject GetAvailableObject () {
		GameObject returnedObject;
		if (inactiveObjects.Count == 0) {

			returnedObject = GameObject.Instantiate (objectPrefab);

		} else {

			returnedObject = inactiveObjects.Dequeue () as GameObject;

		}
		return returnedObject;
	}

	void DestroyObject (string name) {
		if (deployedObjects.ContainsKey (name)) {

			inactiveObjects.Enqueue (deployedObjects [name]);
			deployedObjects[name].SetActive (false);
			deployedObjects.Remove (name);


		} else {
			Debug.Log ("ObjectPool: Can't find speaker of that name");
		}
	
	}

	public void SetPrefab (GameObject prefab){
		objectPrefab = prefab;
	}

}
