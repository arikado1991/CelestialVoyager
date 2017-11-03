using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanelScipt : MonoBehaviour {

	GameObject[] buttons;
	public int[] buttonStatuses;

	public event EventManagerScript.GetValueDelegate <int> ButtonIsClicked;

	// Use this for initialization
	void Awake () {
		int buttonsCount = transform.childCount;
		buttonStatuses = new int[transform.childCount];
		buttons = new GameObject[buttonsCount];

		for (int i = 0; i < buttonsCount; i++) {
			buttons [i] = transform.GetChild (i).gameObject;
			SetButton (i, "", null);
			buttonStatuses[i] = 0;

		}
	}

	public void EvenlyPlaceButton (){
		int buttonsActiveCount = 0;
		foreach (var stat in buttonStatuses) {
			buttonsActiveCount += stat;
		}

		float buttonWidth = 1f / transform.childCount;
		float margin = (1 - (buttonsActiveCount * buttonWidth)) / (buttonsActiveCount + 1);

		RectTransform buttonRectTransform;
		for (int i = 0; i < transform.childCount; i++) {
			if (buttonStatuses [i] > 0) {
				//Debug.Log ("Index " + i + " is " + (buttons [i] != null));
				buttonRectTransform = buttons [i].GetComponent <RectTransform> ();

				buttonRectTransform.anchorMin = new Vector2 ( i 	* (margin + buttonWidth) + margin, 0);
				buttonRectTransform.anchorMax = new Vector2 ((i+1) 	* (margin + buttonWidth), 1);
			}
		}
	}

	public bool SetButton (int index, string label, UnityEngine.Events.UnityAction func){
		
		if (index < buttons.Length) {
			GameObject chosenButton = buttons [index];
			if (label.Length == 0) {
				chosenButton.SetActive (false);
				buttonStatuses [index] = 0;
			} else {
				chosenButton.SetActive (true);
				chosenButton.GetComponentInChildren<Text> ().text = label;
				chosenButton.GetComponent<Button> ().onClick.AddListener (func);
				buttonStatuses [index] = 1;
			}

			return true;
		}
	
		return false;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
