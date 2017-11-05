using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipInfoScript : MonoBehaviour {
	float p_mass = GameOptionsScript.SHIP_MASS;
	float p_remainingFuel;


	public static event EventManagerScript.GetValueDelegate<float> OnFuelAmountUpdateEvent;
	public static event EventManagerScript.GameDelegate OnFuelEmptyEvent;

	public void Awake() {
		GamePlayManagerScript.OnGameRestartEvent += Reset ;
		PlanetGravityScript.OnSpaceshipCollisionWithPlanetEvent += KillShip;
	}


	public void KillShip() {
		SoundManagerScript.GetInstance().PlaySound ("Wilhelm Scream", transform.position);
		SoundManagerScript.GetInstance().PlaySound ("Explosion", transform.position);
	
		fuel = 0;
	}

	public float mass { get { return p_mass; } }

	public float fuel { 
		get { return p_remainingFuel; }

		set { 
			p_remainingFuel = Mathf.Max (0, value); 
		//	Debug.Log (p_remainingFuel);
			OnFuelAmountUpdateEvent(p_remainingFuel);
			if (p_remainingFuel <= 0) {
				OnFuelEmptyEvent ();
			}
		}

	}

	void Reset () {
		fuel = GameOptionsScript.MAX_FUEL_AMOUNT;
		Debug.Log ("ResetFuelTank");

	}

};
