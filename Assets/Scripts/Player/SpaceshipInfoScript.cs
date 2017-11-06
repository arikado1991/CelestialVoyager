using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipInfoScript : MonoBehaviour {
	static SpaceshipInfoScript s_spaceshipInfo;
	public static SpaceshipInfoScript GetPlayer() {
		return s_spaceshipInfo;
	}

	float p_mass = GameOptionsScript.SHIP_MASS;
	float p_remainingFuel;


	public static event EventManagerScript.GetValueDelegate<float> OnFuelAmountUpdateEvent;
	public static event EventManagerScript.GameDelegate OnFuelEmptyEvent;

	public void Awake() {
		if (s_spaceshipInfo != null && s_spaceshipInfo != this) {
			GameObject.DestroyObject (this.gameObject);
			return;
		}
		s_spaceshipInfo = this;
		DontDestroyOnLoad (this.gameObject);

		GamePlayManagerScript.OnGameRestartEvent += Restart ;
		PlanetGravityScript.OnSpaceshipCollisionWithPlanetEvent += KillShip;

	}


	public void KillShip() {
		SoundManagerScript.GetInstance().PlaySound ("Wilhelm Scream", transform.position);
		SoundManagerScript.GetInstance().PlaySound ("Explosion", transform.position);
		SoundManagerScript.GetInstance().PlaySound ("Rocket Flying", transform.position, 0, false);

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

	void Restart () {
		fuel = GameOptionsScript.MAX_FUEL_AMOUNT;
		Debug.Log ("ResetFuelTank");

	}

};
