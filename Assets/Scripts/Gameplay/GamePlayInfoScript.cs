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
	//	Text[] retrivedTextComponents = GetComponentsInChildren<Text> ();
		//score = retrivedTextComponents [1];
		//time = retrivedTextComponents [0];
		fuelBar = GameObject.Find ("FuelPanel").GetComponentInChildren<Slider>();
		if (fuelBar == null) {
			Debug.Log ("FuelBar not found!");
		}
	}




	public void SetFuelBar (float newFuelAmount){
		fuelBar.value = (newFuelAmount > 0) ? (int)newFuelAmount : 0;
		Color barColor = Color.green;
		if (newFuelAmount == 0)
			barColor = Color.clear;
		else if (newFuelAmount < 10)
			barColor = Color.red;
		else if (newFuelAmount < 25)
			ColorUtility.TryParseHtmlString("#FF9400FF", out barColor);
		else if (newFuelAmount < 50)
			barColor = Color.yellow;
		
		
		GameObject.Find ("FuelPanel").GetComponent <FadeScript> ().ActivateFading (0.5f, 1);
		fuelBar.fillRect.transform.GetComponent<Image> ().color = barColor;
	
	}
	// Update is called once per frame
	void Update () {
		
	}
}
