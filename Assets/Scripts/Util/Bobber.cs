using UnityEngine;
using System.Collections;

public class Bobber : MonoBehaviour {

	public enum Axis {
		x, y
	}

	public float timeMultiplier = 1;
	public float localDeltaMultiplier = 1;
	public Axis bobAxis = Axis.y;
	public bool timeSync = false;

	float randomOffset;
	Vector3 startPosition;

	void Start() {
		startPosition = transform.localPosition;

		if (!timeSync) {
			randomOffset = Random.Range(0f, 2f * Mathf.PI);
		}
	}

	void Update () {
		Vector3 newPosition = transform.localPosition;
		switch (bobAxis) {
			case Axis.x:
				newPosition.x = startPosition.x + localDeltaMultiplier * Mathf.Sin(Time.time * timeMultiplier + randomOffset);
				break;
			case Axis.y:
				newPosition.y = startPosition.y + localDeltaMultiplier * Mathf.Sin(Time.time * timeMultiplier + randomOffset);
				break;
		}
		transform.localPosition = newPosition;
	}
}
