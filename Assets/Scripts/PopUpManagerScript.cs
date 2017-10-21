using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManagerScript : MonoBehaviour {

	public GameObject popUp;

	// Use this for initialization
	void Start () {
		HidePopUp ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowPopUp( string title, string message, string buttonTitle){
		popUp.SetActive (true);
		popUp.transform.Find ("Title").GetComponent<Text>().text = title;
		popUp.transform.Find("Message").GetComponent<Text>().text = message;
		popUp.transform.Find ("Button").GetComponentInChildren<Text> ().text = buttonTitle;
	}

	public void HidePopUp(){
		popUp.SetActive (false);
	}
}
