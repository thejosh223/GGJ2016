using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class FadeOutOverlay : MonoBehaviour {

	public bool startFadeIn = false;
	public float startFadeInTime = 1f;
	public bool startFadeOut = false;
	public float startFadeOutTime = 1f;

	Image overlayImage;

	void Awake() {
		__instance = this;
	}

	void Start() {
		overlayImage = GetComponent<Image>();
		gameObject.SetActive(false);

		if (startFadeIn) {
			FadeIn(startFadeInTime, null);
		}
		if (startFadeOut) {
			FadeOut(startFadeOutTime, null);
		}
	}

	public void FadeOutIn(float duration, float delay, Action toCall) {
		gameObject.SetActive(true);
		LTDescr lt = LeanTween.value(gameObject, 0f, 1f, duration).setOnUpdate(UpdateAlpha);
		if (toCall != null) {
			lt.setOnComplete(toCall);
		}

		LeanTween.value(gameObject, 1f, 0f, duration).setDelay(duration + delay).setOnUpdate(UpdateAlpha).setOnComplete(() => gameObject.SetActive(false));
	}

	public void FadeOut(float duration, Action toCall) {
		gameObject.SetActive(true);
		LTDescr lt = LeanTween.value(gameObject, 0f, 1f, duration).setOnUpdate(UpdateAlpha);
		if (toCall != null) {
			lt.setOnComplete(toCall);
		}
	}

	public void FadeIn(float duration, Action toCall) {
		gameObject.SetActive(true);
		UpdateAlpha(1f);

		LTDescr lt = LeanTween.value(gameObject, 1f, 0f, duration).setOnUpdate(UpdateAlpha);
		if (toCall != null) {
			lt.setOnComplete(() => gameObject.SetActive(false));
		}
	}

	void UpdateAlpha(float f) {
		Color c = overlayImage.color;
		c.a = f;
		overlayImage.color = c;
	}

	private static FadeOutOverlay __instance;
	public static FadeOutOverlay Instance { get { return __instance; } }

}
