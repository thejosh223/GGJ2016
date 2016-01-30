using UnityEngine;
using System.Collections;

public class EmitterSet : MonoBehaviour {
	const float DEGREES = 180f;

	public Emitter[] emitters;
	public int set = 1;

	public float degreeOffset = 30f;
	public float degreeStep = 0.00f;
	public float _delay = 0.0f;
	public bool pingPong = false;

	public int damage;
	public float speed;

	int _spawned = 0;
	// Use this for initialization
	void OnEnable () {
		StartCoroutine("InitCoroutine");
	}

	IEnumerator InitCoroutine() {
		float deg = 0.000f;
		float delay = 0.0f;
		_spawned = 0;
		for (int i=0; i<set; i++) {
			Debug.Log(i);
			foreach (Emitter e in emitters) {
				e.Init(damage, speed, deg);
			}
			deg += degreeStep;
			_spawned++;
			if (i <= set) {
				while (delay <= _delay) {
					Debug.Log("wait");
					delay += Time.deltaTime;
					yield return null;
				}
				delay = 0.0f;
			}
		}

		if (pingPong) {
			for (int i=0; i<(set*2); i++) {
				foreach (Emitter e in emitters) {
					e.Init(damage, speed, deg);
				}
				deg -= degreeStep;
				_spawned++;
				if (i <= set) {
					while (delay <= _delay) {
						Debug.Log("wait");
						delay += Time.deltaTime;
						yield return null;
					}
					delay = 0.0f;
				}
			}
		}
	}

	void Update() {
		int m = !pingPong ? set : (set*2);
		if (_spawned >= m) {
			StopCoroutine("InitCoroutine");
			gameObject.SetActive(false);
		}
	}
}
