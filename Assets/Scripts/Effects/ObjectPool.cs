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
		string suffix = "";
		int i = 0;

		bool newEntryRequired = true;
		while (deployedObjects.ContainsKey (name + suffix)){
			
			suffix = "_" + i;

			i++;

			newEntryRequired = false;
		}
		returnedObject.name = name + suffix;
		if (newEntryRequired) {
			
			deployedObjects.Add (returnedObject.name, returnedObject);
		} else {
			deployedObjects [name] = returnedObject;
		}
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
		Dictionary<string, GameObject>.KeyCollection  keys = deployedObjects.Keys ;
		foreach (string key in keys) {
			inactiveObjects.Enqueue (deployedObjects [key]);
		
		}
		deployedObjects.Clear ();
		while (inactiveObjects.Count > 0) {
			
			GameObject.Destroy (inactiveObjects.Dequeue() as GameObject);
		}
	}

}
