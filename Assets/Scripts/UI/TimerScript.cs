using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	float p_timeLeft;
	bool p_isActive = false;
	Text timerDisplayText;
	// Use this for initialization
	LevelScript levelInfo;
	public static event EventManagerScript.GameDelegate OutOfTimeEvent ;
	void Start () {
		p_timeLeft = 10;
		timerDisplayText = transform.Find("TimerDisplayText").GetComponent<Text> ();
		levelInfo = GamePlayManagerScript.GetInstance ().levelInfo;
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
		get {return p_timeLeft;}
	}
	public string timeStr { get {return timerDisplayText.text;}}

	void Restart() {
		p_timeLeft = levelInfo.maxTime;

		Debug.Log ("Time: " + levelInfo.maxTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (p_isActive) {
			
			p_timeLeft -= Time.deltaTime;
			SetDisplayTime ();
			//OnUpdateTimerEvent (time);
		}
		if (p_timeLeft <= 0)
			OutOfTimeEvent ();

	}

	void SetDisplayTime()
	{
		short minutes = (short) Mathf.Floor(p_timeLeft / 60);
		short seconds= (short) Mathf.Floor (p_timeLeft % 60);

		timerDisplayText.text = "" + minutes + ":" + 
			((seconds < 10) ? "0" : "") + seconds;
		Color textColor = Color.green;
		if (time < levelInfo.time2star)
			ColorUtility.TryParseHtmlString("#FF9400FF", out textColor);
		else if (time < levelInfo.time3star)
			textColor = Color.yellow;

		timerDisplayText.color = textColor;
	}
}
