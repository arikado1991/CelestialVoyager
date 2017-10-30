using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceVectorDisplayManager : MonoBehaviour {
	
	Dictionary <string, GameObject> forceVectors;
	// Use this for initialization
	void Start () {
		forceVectors = new Dictionary <string, GameObject> ();
		  
	}

	void OnEnable() {
		GamePlayManagerScript.OnGameRestartEvent += OnGamePlayRestart;
		PlanetGravityScript.OnUnderGravityFromPlanetEvent += SetVectorDisplay;	
		SpaceshipMovementScript.OnForceAppliedEvent += (Vector2 v) => SetVectorDisplay ("Spaceship", v);
	}	
		

	void OnGamePlayRestart() {
		if (!forceVectors.ContainsKey ("Spaceship")){
			AddVectorDisplay ("Spaceship");

		}

	}

	public void AddVectorDisplay (string name) {
		Debug.Log ("Key = " + name + " " + forceVectors.ContainsKey (name));
		if (!forceVectors.ContainsKey (name)) {
			GameObject newVectorDisplay = 
				GameObject.Instantiate (Resources.Load <GameObject> ("Prefabs/ForceVectorDisplayPrefab"));
			forceVectors.Add (name, newVectorDisplay);
			newVectorDisplay.transform.SetParent (transform);
			newVectorDisplay.name = name + "_ForceVectorDisplay";
			newVectorDisplay.GetComponent<ExpandableArrowScript> ().SetFollowShip ();

			SpriteRenderer[] spriteRenderers = forceVectors [name].GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer render in spriteRenderers) {
				render.color = (name == "Spaceship") ? Color.blue : Color.red;
			}
		}
	}

	public void SetVectorDisplay (string name, Vector2 vector) {
		if (!forceVectors.ContainsKey (name))
			AddVectorDisplay (name);
		forceVectors [name].GetComponent<ExpandableArrowScript> ().SetForce (vector);

	}

	// Update is called once per frame
	void Update () {
		
	}
}
