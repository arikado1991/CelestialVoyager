﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FuelGaugeScript: MonoBehaviour {

	static private FuelGaugeScript s_fuelGauge;
	static public FuelGaugeScript GetInstance() {
		return s_fuelGauge;
	}

	LevelScript levelInfo;

	Slider fuelBar;

	void OnEnable() {
		SpaceshipInfoScript.OnMaxFuelAmountUpdateEvent += SetMaxFuelBar;
		SpaceshipInfoScript.OnFuelAmountUpdateEvent += SetFuelBar;
	}
	void OnDisable() {
		SpaceshipInfoScript.OnMaxFuelAmountUpdateEvent -= SetMaxFuelBar;
		SpaceshipInfoScript.OnFuelAmountUpdateEvent -= SetFuelBar;
	}

	// Use this for initialization
	void Awake () {

		if (s_fuelGauge != null && s_fuelGauge != this) {
			GameObject.Destroy (this.gameObject);
			return;
		}

		s_fuelGauge = this;


		fuelBar = GameObject.Find ("FuelPanel").GetComponentInChildren<Slider>();
		if (fuelBar == null) {
			Debug.Log ("FuelBar not found!");
		}
	}

	void Start() {
		levelInfo = GamePlayManagerScript.GetInstance().levelInfo;
	}

	public void SetMaxFuelBar (float newMaxFuel) {
		fuelBar.maxValue = newMaxFuel;
	}


	public void SetFuelBar (float newFuelAmount){
		
		fuelBar.value = (newFuelAmount > 0) ? (int)newFuelAmount : 0;
		Color barColor = Color.green;
		if (newFuelAmount == 0)
			barColor = Color.clear;
		else if (newFuelAmount < 120)
			barColor = Color.red;
		else if (newFuelAmount < levelInfo.fuel2star)
			ColorUtility.TryParseHtmlString("#FF9400FF", out barColor);
		else if (newFuelAmount < levelInfo.fuel3star)
			barColor = Color.yellow;
		
		
		GameObject.Find ("FuelPanel").GetComponent <FadeScript> ().ActivateFading (0.5f, 1);
		fuelBar.fillRect.transform.GetComponent<Image> ().color = barColor;
	
	}
	// Update is called once per frame
	void Update () {
		
	}
}
