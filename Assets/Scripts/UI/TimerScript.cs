using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	float p_timePassed;
	bool p_isActive = false;
	Text timerDisplayText;
	// Use this for initialization

	void Start () {
		timerDisplayText = transform.Find("TimerDisplayText").GetComponent<Text> ();
	
		GamePlayManagerScript.OnSetGameActiveEvent += SetActive;
		GamePlayManagerScript.OnGameRestartEvent += Restart;
	}

	bool active {
		get { return p_isActive; }
		set { p_isActive = value; }
	}

	void SetActive (bool isActive){
		active = isActive;
	}

	public float time { 
		get {return p_timePassed;}
	}
	public string timeStr { get {return timerDisplayText.text;}}

	void Restart() {
		p_timePassed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (p_isActive) {
			
			p_timePassed += Time.deltaTime;
			SetDisplayTime ();
			//OnUpdateTimerEvent (time);
		}

	}

	void SetDisplayTime()
	{
		short minutes = (short) Mathf.Floor(p_timePassed / 60);
		short seconds= (short) Mathf.Floor (p_timePassed % 60);

		timerDisplayText.text = "" + minutes + ":" + 
			((seconds < 10) ? "0" : "") + seconds;
	}
}
