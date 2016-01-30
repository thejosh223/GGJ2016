using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class SuccessMessageUI : MonoBehaviour {

	public enum SuccessMessage {
		Success,
		GreatJob
	}

	[System.Serializable]
	public class SuccessImage {
		public SuccessMessage message;
		public Image image;
	}

	public SuccessImage[] successImages;

	void Awake() {
		__instance = this;
	}

//	void Update() {
//		if (Input.GetKeyDown(KeyCode.Alpha1)) {
//			ShowMessage(SuccessMessage.GreatJob);
//		} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
//			ShowMessage(SuccessMessage.Success);
//		}
//	}

	public void ShowMessage(SuccessMessage message, Action toCallOnFadeOut) {
		RectTransform rectTransform = null;
		for (int i = 0; i < successImages.Length; i++) {
			if (successImages[i].message == message) {
				rectTransform = successImages[i].image.rectTransform;
			}
		}

		rectTransform.localScale = Vector3.zero;
		rectTransform.gameObject.SetActive(true);
		LeanTween.scale(rectTransform, Vector3.one, 0.25f).setEase(LeanTweenType.easeOutQuad);
		LeanTween.scale(rectTransform, Vector3.zero, 0.25f).setEase(LeanTweenType.easeInQuad).setDelay(0.25f + 0.5f).setOnComplete(toCallOnFadeOut);
	}

	void HideMessage(GameObject g) {
		g.SetActive(false);
	}


	private static SuccessMessageUI __instance;
	public static SuccessMessageUI Instance { get { return __instance; } }

}
