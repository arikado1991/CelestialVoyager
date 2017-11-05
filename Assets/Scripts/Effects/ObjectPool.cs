using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool {

	GameObject objectPrefab;

	public Dictionary <string, GameObject> deployedObjects;
	public Queue inactiveObjects;

	public ObjectPool() {
		deployedObjects = new Dictionary <string, GameObject>();
		inactiveObjects = new Queue();
	}

	public GameObject Find (string name) {
		if (deployedObjects.ContainsKey (name)) {
			return deployedObjects[name];
		}
		return null;
	}

	public GameObject GetAvailableObject (string name) {
		GameObject returnedObject;
		if (inactiveObjects.Count == 0) {

			returnedObject = GameObject.Instantiate (objectPrefab);

		} else {

			returnedObject = inactiveObjects.Dequeue () as GameObject;

		}
		string suffix;
		int i = 0;
		while (deployedObjects.ContainsKey (name) ){
			suffix = "_" + i;
			name = name  + suffix;
			i++;
		}
		returnedObject.name = name;
		deployedObjects.Add (name, returnedObject);
		return returnedObject;
	}

	public void DestroyObject (string name) {
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

	public void Clear() {
		
		foreach (string key in deployedObjects.Keys) {
			GameObject victim = deployedObjects [key];
		
			GameObject.Destroy (victim);
		}
		deployedObjects.Clear ();

		while (inactiveObjects.Count > 0) {
			
			GameObject.Destroy (inactiveObjects.Dequeue() as GameObject);
		}
	}

}
