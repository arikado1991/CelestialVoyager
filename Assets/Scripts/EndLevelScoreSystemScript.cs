using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelScoreSystemScript : MonoBehaviour {

	int p_endLevelScore = 0;
	GamePlayManagerScript gameManager;

	void Start(){
		gameManager = GameObject.FindObjectOfType<GamePlayManagerScript> ();
	}

	public int endLvScore {
		get { return p_endLevelScore; }
	}
	// Use this for initialization
	void OnEnable (){
		GamePlayManagerScript.OnEndLevelEvent  += GetEndLevelScore;
		GamePlayManagerScript.OnGameRestartEvent += ResetScore;
	}

	void GetEndLevelScore(){
		p_endLevelScore =
			(int) (gameManager.player.GetComponent <SpaceshipInfoScript>().fuel * 10
			-	gameManager.timer.time);
	}

	void ResetScore() {
		p_endLevelScore = 0;
	}
}
