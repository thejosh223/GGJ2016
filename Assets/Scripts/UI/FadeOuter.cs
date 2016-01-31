using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOuter : MonoBehaviour {

	public float alphaDuration = 1f;

	bool isAlphaUp = true;
	float alpha = 0;
	Graphic graphic;

	void Start() {
		graphic = GetComponent<Graphic>();
	}

	void Update () {
		if (isAlphaUp) {
			alpha += Time.deltaTime / alphaDuration;
			if (alpha > 1f) {
				isAlphaUp = !isAlphaUp;
				alpha = 1f;
			}
		} else {
			alpha -= Time.deltaTime / alphaDuration;
			if (alpha < 0f) {
				isAlphaUp = !isAlphaUp;
				alpha = 0f;
			}
		}

		Color c = graphic.color;
		c.a = alpha;
		graphic.color = c;
	}
}
