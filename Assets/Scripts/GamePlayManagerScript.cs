using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManagerScript : MonoBehaviour {

	int score;
	float remainingTime;

	GamePlayInfoScript infoBoard;
	PopUpManagerScript popUpManager;
	GameObject player;
	GameObject fishSpawner;


	bool isPlayable;






	// Use this for initialization
	void Start () {
		infoBoard = GameObject.FindObjectOfType<GamePlayInfoScript> ();
		popUpManager = GameObject.FindObjectOfType<PopUpManagerScript> ();
		popUpManager.ShowPopUp ("Space Voyager hooray!", "Are you ready to explore the mysterious cosmos?", "Where's my insurrance?");

		player = GameObject.FindGameObjectWithTag ("Player");
		//player.SetActive (false);
		fishSpawner = GameObject.Find ("FishSpawner");

		ActivateGamePlay (false);
	}
	

	public void UpdateInfoBoard (bool is_score_changed = false) {
		infoBoard.SetTime (Mathf.RoundToInt(remainingTime));
		if (is_score_changed)
			infoBoard.SetScore (score);

	}

	public void UpdateScore (int newScore){
		score += newScore;
		UpdateInfoBoard (true);
	
	}

	// Update is called once per frame	
	void Update(){
		if (!isPlayable)
			return;
		
		remainingTime -= Time.deltaTime;
		UpdateInfoBoard ();


	}

	void ActivateGamePlay(bool is_activated) {
		UpdateInfoBoard (true);
		isPlayable = is_activated;
	//	player.SetActive (is_activated);
		fishSpawner.SetActive (is_activated);
	}

	public void Restart(){
		popUpManager.HidePopUp ();
		score = 0;
		player.GetComponent <SpaceshipMovementScript> ().Restart();
		remainingTime = GameOptionsScript.GAME_DURATION;
		infoBoard = GameObject.FindObjectOfType<GamePlayInfoScript>();
		ActivateGamePlay (true);

	}

	public void GameOver() {
		popUpManager.ShowPopUp ("Game Over!", "You are out of fuel!\n You're now a cold corpse that wanders the universe for all eternity!", "Restart");
	}
}
