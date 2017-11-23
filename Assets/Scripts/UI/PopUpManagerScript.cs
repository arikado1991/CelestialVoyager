using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PopUpManagerScript : MonoBehaviour {


	public enum PopUpType {MESSAGE, ENDGAMERANKING

	};


	static PopUpManagerScript s_popUpManager;
	public static PopUpManagerScript GetInstance() {
		return s_popUpManager;
	}
		

	Dictionary <string, PopUpScript > popUps;
	Dictionary <string, GameObject> popUpPrefabs;

	void Awake() {
		PopUpScript.basicFontSize  =  (int) (GameOptionsScript.UNIT_TO_PIXEL / 2 );
		if (s_popUpManager != null && s_popUpManager != this) {
			GameObject.Destroy (this.gameObject);
			return;
		}
		s_popUpManager = this;

		popUps = new Dictionary <string, PopUpScript> ();
		popUpPrefabs = new Dictionary <string, GameObject> ();

		Object[] tempArray = Resources.LoadAll ("Prefabs/UI/PopUp");

		GameObject temp;
		foreach (object o in tempArray) {
			temp = o as GameObject;
			popUpPrefabs.Add (temp.name, temp);

		//	Debug.Log (temp.name);
		}
		PopUpScript.basicFontSize = (int) ( Mathf.Max (GameOptionsScript.UNIT_TO_PIXEL / 28 , 1));
	
	}


	// Use this for initialization
	void OnEnable () {




		/*
		popUp = new PopUp ();
		popUp.gameObject = GameObject.Find ("PopUp");
		popUp.title = popUp.gameObject.transform.Find("Title").GetComponent<Text>();
		popUp.content = popUp.gameObject.transform.Find("Message").GetComponent<Text>();
		popUp.buttonPanel = popUp.gameObject.GetComponentInChildren <ButtonPanelScipt> ();
*/
		//ShowPopUp (false);
		GamePlayManagerScript.OnNewLevelLoaded += ClearPopUp;

	}

	public GameObject CreatePopUp (PopUpType popUpType, string popUpName){

		GameObject newPopup; 
		string prefabKey = "";

		switch (popUpType) {
		case PopUpType.MESSAGE:
			prefabKey = "1Title1Message3ButtonMaxPopUp";
			break;
		case PopUpType.ENDGAMERANKING:
			prefabKey = "EndLevelPopUp";
			break;
		default:
			prefabKey = "";
			break;
		}

		try {
			newPopup = GameObject.Instantiate (popUpPrefabs [prefabKey]);
			newPopup.transform.SetParent ( GameObject.Find("UICanvas").transform, false);
			newPopup.transform.localPosition = Vector3.zero;
			newPopup.name = popUpName;
			popUps.Add (popUpName, newPopup.GetComponent <PopUpScript> ());
			Debug.Log ("Initiation object successful newPopUp is" + (newPopup != null));
			return newPopup;

		} catch (KeyNotFoundException) {
			Debug.LogError ("PopUpManager: Failed to create Pop Up" + popUpName + " - PopUp Type " + popUpType + " not recognized!");
			return null; 
		}

	}

	public void HideAllPopup () {
		foreach (string popUpName in popUps.Keys) {
			ShowPopUp (popUpName, false);
		}
	}

	public void ShowPopUp( string popUpName, bool visible){
		GetPopUp (popUpName).SetActive (visible);
	}

	public void ClearPopUp () {
		popUps.Clear();
	}


	public GameObject GetPopUp(string popUpName){
		try {
			return popUps[popUpName].gameObject;
		} catch (KeyNotFoundException) {
			Debug.LogError ("PopUpManagerScript::GetPopUp: Key " + popUpName + " not found");
			return null;
		}

	}


}
