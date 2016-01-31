using UnityEngine;
using System.Collections;

public class AnyKeySceneChanger : MonoBehaviour {

	public string sceneToChangeTo;

	void Update() {
		if (Input.anyKeyDown) {
			FadeOutOverlay.Instance.FadeOut(1f, () => Application.LoadLevel(sceneToChangeTo));
		}
	}

}
