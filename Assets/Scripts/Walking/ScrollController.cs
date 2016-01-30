using UnityEngine;
using System;
using System.Collections;

public class ScrollController : MonoBehaviour {

	public Camera camera;
	public float direction;
	public float speed;
	
	public Location currentLocation { get; private set; }
	public bool isMovingRight { get { return direction > 0; } }
	public bool isMovingLeft { get { return direction < 0; } }

	void Awake() {
		__instance = this;

		currentLocation = Location.Suburb;
	}

	public void NextLocation() {
		currentLocation = (Location) (((int) currentLocation + 1) % System.Enum.GetValues(typeof(Location)).Length);
	}

	public Vector3 rightBound {
		get { 
			Vector3 v = camera.ViewportToWorldPoint(Vector3.right);
			v.z = 0f;
			return v;
		}
	}

	public Vector3 leftBound {
		get {
			Vector3 v = camera.ViewportToWorldPoint(Vector3.zero);
			v.z = 0f;
			return v;
		}
	}

	static ScrollController __instance;
	public static ScrollController Instance {
		get { return __instance; }
	}

}
