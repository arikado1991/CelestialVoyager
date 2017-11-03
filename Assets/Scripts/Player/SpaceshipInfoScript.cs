using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipInfoScript : MonoBehaviour {
	float p_mass = GameOptionsScript.SHIP_MASS;
	float p_remainingFuel;


	public static event EventManagerScript.GetValueDelegate<float> OnFuelAmountUpdateEvent;
	public static event EventManagerScript.GameDelegate OnFuelEmptyEvent;

	public void OnEnable(){
		//EventManagerScript.ChangeFuelAmountEvent += AddFuel;
		PlanetGravityScript.OnSpaceshipCollisionWithPlanetEvent += () => {fuel = 0;};
		GamePlayManagerScript.OnGameRestartEvent += Reset ;
	}

	public float mass { get { return p_mass; } }

	public float fuel { 
		get { return p_remainingFuel; }

		set { 
			p_remainingFuel = Mathf.Max (0, value); 
			OnFuelAmountUpdateEvent(p_remainingFuel);
			if (p_remainingFuel <= 0) {
				OnFuelEmptyEvent ();
			}
		}

	}

	void Reset () {
		fuel = GameOptionsScript.MAX_FUEL_AMOUNT;
	}

};
