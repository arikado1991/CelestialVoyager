using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	public Image[] shownDigits;
	float time_left;
	// Use this for initialization

	void Awake (){
		

	}

	void Start () {
		shownDigits = GetComponentsInChildren<Image>();
		time_left = 120.0f;
		SetDisplayTime ();
	}
	
	// Update is called once per frame
	void Update () {
		time_left -= Time.deltaTime;
		SetDisplayTime ();
	}

	void SetDisplayTime()
	{
		short minutes = (short) Mathf.Floor(time_left / 60);
		short secondsdigit1 = (short) Mathf.Floor (time_left % 60);
		short secondsdigit2 =  (short) Mathf.Floor (secondsdigit1 % 10);
		secondsdigit1 = (short) Mathf.Floor(secondsdigit1 / 10);
		//shownDigits [0].sprite = null; //GameOptionsScript.NUMBER_SPRITE [minutes];
		//shownDigits [1].sprite = GameOptionsScript.NUMBER_SPRITE [secondsdigit1];
		//shownDigits [2].sprite = GameOptionsScript.NUMBER_SPRITE [secondsdigit2];
	}
}
