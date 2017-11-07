using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpScript: MonoBehaviour {
	public enum ShowContentMethod {SLIDE_FROM_TOP, FADE_IN};


	private float width, height;
	public void SetDimension (float width, float height) {

		float horizontalMargin = (1 - width) * 0.5f;
		float verticalMargin = (1 - height) * 0.5f;
		rectTransform.anchorMax = new Vector2 (1 - horizontalMargin, 1 - verticalMargin);
		rectTransform.anchorMin = new Vector2 (horizontalMargin, verticalMargin);
		rectTransform.anchoredPosition = new Vector2 (0, 0);

	}

	RectTransform rectTransform;

	public Dictionary <string, GameObject> contents;

	public ShowContentMethod showMethod;




	void ReadContent () {
		contents = new Dictionary<string, GameObject> ();
		foreach (Transform child in transform) {
			contents.Add (child.name, child.gameObject);
		}
	}

	public GameObject GetContent (string contentName) {
		try {
			return contents[contentName];
		} catch (KeyNotFoundException) {
			Debug.LogError ("PopUpScript: Key '" + contentName + "' Not Found!");
			return null;
		}
	}

	void Awake () {
		rectTransform = gameObject.GetComponent <RectTransform> (); 

		rectTransform.localScale = Vector3.one;
		rectTransform.position = Vector3.one;

		if (rectTransform == null) {
			Debug.LogError ("PopUpScript: PopUp " + gameObject.name + " has no RectTransform component");
			return;
		}
		ReadContent ();

	}

	void Update () {
	}

	void ShowContent () {
		
	}
};