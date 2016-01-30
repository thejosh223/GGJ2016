using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float libido { get; set; }

	void Awake() {
		__instance = this;
	}

	static Player __instance;
	public static Player Instance {
		get { return __instance; }
	}

}
