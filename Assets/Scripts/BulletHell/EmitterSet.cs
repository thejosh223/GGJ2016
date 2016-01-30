using UnityEngine;
using System.Collections;

public class EmitterSet : MonoBehaviour {
	public Emitter[] emitters;
	public int damage;
	public float speed;
	// Use this for initialization
	void OnEnable () {
		foreach (Emitter e in emitters) {
			e.Init(damage, speed);
		}
		gameObject.SetActive(false);
	}
}
