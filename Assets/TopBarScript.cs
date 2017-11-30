using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBarScript : MonoBehaviour {
	Button restartButton;
	// Use this for initialization
	void Start () {
		restartButton = transform.GetComponentInChildren <Button> ();
	
		restartButton.onClick.AddListener (GamePlayManagerScript.GetInstance ().Restart);
		Debug.Log ("Button is " + (restartButton != null) + ". GameplayManagerScript is " + GamePlayManagerScript.GetInstance ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
