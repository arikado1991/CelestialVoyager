using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManagerScript : MonoBehaviour {

	GamePlayInfoScript infoBoard;
	PopUpManagerScript popUpManager;

	public TimerScript timer;
	public EndLevelScoreSystemScript scoreSystem;

	public GameObject player;


	public static event EventManagerScript.GameDelegate OnGameOverEvent;
	public static event EventManagerScript.GameDelegate OnGameRestartEvent;
	public static event EventManagerScript.GameDelegate OnEndLevelEvent;
	public static event EventManagerScript.SetValue <bool> OnSetGameActiveEvent;



	// Use this for initialization
	void Start () {
		infoBoard = GameObject.FindObjectOfType<GamePlayInfoScript> ();
		popUpManager = GameObject.FindObjectOfType<PopUpManagerScript> ();
		popUpManager.ShowPopUp ("Welcome to Space Voyager!", "Are you ready to explore the cosmos and beyond?", "Does my insurance cover this?");

		player = GameObject.FindGameObjectWithTag ("Player");
		timer = GameObject.FindObjectOfType <TimerScript> ();
		scoreSystem = GameObject.FindObjectOfType <EndLevelScoreSystemScript> ();

		ActivateGamePlay (false);

	}

	void OnEnable(){
		SpaceshipInfoScript.OnFuelEmptyEvent += GameOver;
		FinishedPlanetScript.OnReachingDestinationEvent += FinishLevel;

	}

	void OnDisable() {
		SpaceshipInfoScript.OnFuelEmptyEvent -= GameOver;
		FinishedPlanetScript.OnReachingDestinationEvent -= FinishLevel;
	}



	void ActivateGamePlay(bool is_activated) {
		
		player.SetActive (is_activated);
		OnSetGameActiveEvent (is_activated);

	}

	public void Restart(){
		OnGameRestartEvent ();
		popUpManager.HidePopUp ();
		ActivateGamePlay (true);

	}

	public void GameOver() {
		ActivateGamePlay (false);
		popUpManager.ShowPopUp ("Game Over!", 
			"You are out of fuel!\n You're now a cold corpse that wanders the universe for all eternity!", 
			"Restart");
	}

	public void FinishLevel() {
		ActivateGamePlay (false);
		OnEndLevelEvent ();

		popUpManager.ShowPopUp (
			"Level finished!", 

			"Fuel: " + (int)player.GetComponent<SpaceshipInfoScript> ().fuel + 
				" lt\nTime: " +  timer.timeStr + 
				"\nScore: " + scoreSystem.endLvScore, 

			"Next Level");
	}
}
