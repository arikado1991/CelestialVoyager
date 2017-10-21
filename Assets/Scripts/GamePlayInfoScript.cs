using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GamePlayInfoScript: MonoBehaviour {

	public Text score, time;

	public Slider fuelBar;

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

	public void  SetScore (int newScore){
		score.text = "Score\n" + newScore.ToString ();
	}

	public void  SetTime (int newTime){
		time.text = "Time\n" + newTime.ToString ();
	}

	public void SetFuel (int newFuelAmount){
		fuelBar.value = newFuelAmount;
	
	}
	// Update is called once per frame
	void Update () {
		
	}
}
