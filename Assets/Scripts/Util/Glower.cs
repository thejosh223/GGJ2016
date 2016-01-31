using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Glower : MonoBehaviour {

	public float alphaDuration = 1f;

	bool isAlphaUp = true;
	float alpha = 0;
	SpriteRenderer spriteRenderer;

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
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

		Color c = spriteRenderer.color;
		c.a = alpha;
		spriteRenderer.color = c;
	}
}
