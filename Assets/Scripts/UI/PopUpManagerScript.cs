using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PopUpManagerScript : MonoBehaviour {
	static PopUpManagerScript s_popUpManager;
	public static PopUpManagerScript GetInstance() {
		return s_popUpManager;
	}

	 struct PopUp {
		public GameObject gameObject;
		public Text title;
		public Text content; 
		public ButtonPanelScipt buttonPanel;
	};

	PopUp popUp;
	void Awake() {
		if (s_popUpManager != null && s_popUpManager != this) {
			GameObject.Destroy (this.gameObject);
			return;
		}
		s_popUpManager = this;

	
		popUp = new PopUp ();

	
	}
	// Use this for initialization
	void OnEnable () {
		
		popUp = new PopUp ();
		popUp.gameObject = GameObject.Find ("PopUp");
		popUp.title = popUp.gameObject.transform.Find("Title").GetComponent<Text>();
		popUp.content = popUp.gameObject.transform.Find("Message").GetComponent<Text>();
		popUp.buttonPanel = popUp.gameObject.GetComponentInChildren <ButtonPanelScipt> ();

		//ShowPopUp (false);
	}

	public void ClearPopup () {
		popUp.buttonPanel.ResetButtons ();
	}

	public void ShowPopUp( bool visible){
		popUp.content.fontSize =  (int) (GameOptionsScript.UNIT_TO_PIXEL / 2 );
		popUp.title.fontSize = (int) (popUp.content.fontSize * 1.5f);
		popUp.buttonPanel.EvenlyPlaceButton ();
		popUp.gameObject.SetActive (visible);

	}



	public void SetPopUp(string title, string content){
	//	popUp.transform.name = popUpContent.name;
		Debug.Log(popUp.gameObject);
		popUp.title.text = title;
		popUp.content.text = content;

	}

	public void SetButtonFunction (int index, string label, UnityEngine.Events.UnityAction func) {
		popUp.buttonPanel.SetButton (index, label, func);
	}
}
