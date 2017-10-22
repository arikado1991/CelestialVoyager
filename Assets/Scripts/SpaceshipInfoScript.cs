using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipInfoScript : MonoBehaviour {
	float p_mass = 10;//Mathf.Pow (,20);
	float p_remainingFuel;


	public static event EventManagerScript.GetValue<float> OnFuelAmountUpdateEvent;
	public static event EventManagerScript.GameDelegate OnFuelEmptyEvent;

	public void OnEnable(){
		//EventManagerScript.ChangeFuelAmountEvent += AddFuel;
		PlanetGravityScript.OnSpaceshipCollisionWithPlanetEvent += EmptyFuel;
		GamePlayManagerScript.OnGameRestartEvent += Reset ;
	}

	public float mass { get { return p_mass; } }

	public float fuel { 
		get { return p_remainingFuel; }

		set { 
			p_remainingFuel = Mathf.Max (0, value); 
			OnFuelAmountUpdateEvent(p_remainingFuel);
			if (p_remainingFuel <= 0)
				OnFuelEmptyEvent ();
		}

	}

	void Reset () {
		fuel = GameOptionsScript.MAX_FUEL_AMOUNT;
	}

	void EmptyFuel () {
		fuel = 0;
	}


};
