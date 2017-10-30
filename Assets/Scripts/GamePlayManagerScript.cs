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
		player = GameObject.FindGameObjectWithTag ("Player");

		popUpManager.SetPopUp ("Welcome to Space Voyager!", "Are you ready to explore the cosmos and beyond?");
		popUpManager.SetButtonFunction (0, "Um .. no?", Restart);
		popUpManager.ShowPopUp (true);
		Debug.Log ("Show pop up at the beginning");
		Debug.Log (player != null);
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
		Debug.Log ("Show pop up:" + is_activated);

	}

	public void Restart(){
		OnGameRestartEvent ();
		popUpManager.ShowPopUp (false);
		ActivateGamePlay (true);

	}

	public void GameOver() {
		ActivateGamePlay (false);
		popUpManager.SetPopUp ("Game Over!", 
			"You are out of fuel!\n You're now a cold corpse that wanders the universe for all eternity!");
		popUpManager.ShowPopUp (true);
		popUpManager.SetButtonFunction (0, "Replay", Restart);

	}

	public void FinishLevel() {
		ActivateGamePlay (false);
		OnEndLevelEvent ();

		popUpManager.SetPopUp (
			"Level finished!", 

			"Fuel: " + (int)player.GetComponent<SpaceshipInfoScript> ().fuel + 
				" lt\nTime: " +  timer.timeStr + 
				"\nScore: " + scoreSystem.endLvScore);
		popUpManager.SetButtonFunction (1, "Continue", LoadNextLevel);
		popUpManager.ShowPopUp (true);
	}

	public void LoadNextLevel() {
		Debug.Log ("I should load the next level. If there was one!");
		Restart ();

	}
}
