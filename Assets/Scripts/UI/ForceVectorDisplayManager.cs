using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ForceVectorDisplayManager : MonoBehaviour {

	static ForceVectorDisplayManager s_forceDisplayManager;
	public static ForceVectorDisplayManager GetInstance(){
		return s_forceDisplayManager;
	}
	
	ObjectPool forceVectors;
	// Use this for initialization

	void Awake () {
		
		if (s_forceDisplayManager != null && s_forceDisplayManager != this) {
			GameObject.Destroy (this.gameObject);
			return;
		}
		s_forceDisplayManager = this;


		forceVectors = new ObjectPool ();
		forceVectors.SetPrefab (Resources.Load ("Prefabs/UI/ForceVectorDisplayPrefab") as GameObject);
		//DontDestroyOnLoad (this.gameObject);
	}

	void OnEnable() {
		GamePlayManagerScript.OnGameRestartEvent += OnGamePlayRestart;
		PlanetGravityScript.OnUnderGravityFromPlanetEvent += SetVectorDisplay;	
		SpaceshipMovementScript.OnForceAppliedEvent += (Vector2 v) => SetVectorDisplay ("Spaceship", v);
	}

	void OnDisable(){
		GamePlayManagerScript.OnGameRestartEvent -= OnGamePlayRestart;
		PlanetGravityScript.OnUnderGravityFromPlanetEvent -= SetVectorDisplay;	
		SpaceshipMovementScript.OnForceAppliedEvent -= (Vector2 v) => SetVectorDisplay ("Spaceship", v);
	}
		

	void OnGamePlayRestart() {
		if (forceVectors.Find ("Spaceship")!= null){
			AddVectorDisplay ("Spaceship");

		}

	}

	GameObject AddVectorDisplay (string name) {
//		Debug.Log ("Key = " + name + " " + forceVectors.ContainsKey (name));
		GameObject newVectorDisplay = forceVectors.Find (name + "_ForceVectorDisplay");
		if (newVectorDisplay == null) {
			newVectorDisplay = 
				forceVectors.GetAvailableObject (name + "_ForceVectorDisplay");

			newVectorDisplay.transform.SetParent (transform);
			newVectorDisplay.GetComponent<ExpandableArrowScript> ().SetFollowShip ();

			SpriteRenderer[] spriteRenderers = newVectorDisplay.GetComponentsInChildren<SpriteRenderer> ();

			foreach (SpriteRenderer render in spriteRenderers) {
				render.color = (name == "Spaceship") ? Color.cyan : Color.red;
			}
		}
		newVectorDisplay.SetActive (true);
		return newVectorDisplay;
	}

	public void SetVectorDisplay (string name, Vector2 vector) {
		GameObject targetForceVector = forceVectors.Find (name);
		if (targetForceVector == null)
			targetForceVector = AddVectorDisplay (name);
		targetForceVector.GetComponent<ExpandableArrowScript> ().SetForce (vector);

	}
		

	public void Clear() {
		forceVectors.Clear ();
	}
}
