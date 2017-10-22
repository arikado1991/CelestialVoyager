using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GamePlayInfoScript: MonoBehaviour {

	public Text score, time;

	Slider fuelBar;

	void OnEnable() {
		SpaceshipInfoScript.OnFuelAmountUpdateEvent += SetFuelBar;
	}
	void OnDisable() {
		SpaceshipInfoScript.OnFuelAmountUpdateEvent -= SetFuelBar;
	}

	// Use this for initialization
	void Awake () {
		Text[] retrivedTextComponents = GetComponentsInChildren<Text> ();
		//score = retrivedTextComponents [1];
		//time = retrivedTextComponents [0];
		fuelBar = transform.Find ("FuelBar").GetComponent<Slider>();
		if (fuelBar == null) {
			Debug.Log ("FuelBar not found!");
		}
	}




	public void SetFuelBar (float newFuelAmount){
		fuelBar.value = (newFuelAmount > 0) ? (int)newFuelAmount : 0;
	
	}
	// Update is called once per frame
	void Update () {
		
	}
}
