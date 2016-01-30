using UnityEngine;
using System.Collections;

public class EmitterSet : MonoBehaviour {
	public Emitter[] emitters;
	// Use this for initialization
	void OnEnable () {
		foreach (Emitter e in emitters) {
			e.Init();
		}
		Debug.Log("done");
		Destroy(gameObject);
	}
}
