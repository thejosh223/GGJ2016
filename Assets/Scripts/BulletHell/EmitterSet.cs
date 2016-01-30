using UnityEngine;
using System.Collections;

public class EmitterSet : MonoBehaviour {
	const float DEGREES = 180f;

	public Emitter[] emitters;
	public int set = 1;
	public float degreeOffset = 30f;
	public float degreeStep = 0.00f;
	public float _delay = 0.0f;
	public int damage;
	public float speed;

	int _spawned = 0;
	// Use this for initialization
	void OnEnable () {
		StartCoroutine("InitCoroutine");
	}

	IEnumerator InitCoroutine() {
		float deg = 0.0f;
		float delay = 0.0f;
		_spawned = 0;
		for (int i=0; i<=set; i++) {
			foreach (Emitter e in emitters) {
				if (i == 0) {
					e.Init(damage, speed, degreeOffset);
				} else {
					deg += degreeStep;
					e.Init(damage, speed, deg+degreeOffset);
				}
			}
			_spawned++;
			if (i <= set) {
				while (delay <= _delay) {
					delay += Time.deltaTime;
					yield return null;
				}
			}
		}
	}

	void Update() {
		if (_spawned >= set) {
			StopCoroutine("InitCoroutine");
			gameObject.SetActive(false);
		}
	}
}
