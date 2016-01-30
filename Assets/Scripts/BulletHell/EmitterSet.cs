using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

public class EmitterSet : MonoBehaviour {
	const float DEGREES = 180f;

	public GameObject bulletPrefab;
	public Emitter[] emitters;
	[FormerlySerializedAs("set")]
	public int countShots = 1;

	public float degreeStep = 0.0f;
	[FormerlySerializedAs("_delay")]
	public float delayBetweenShots = 0.0f;
	public bool pingPong = false;
	public bool centerShots = true;

	public int damage;
	public float speed;

	void OnEnable () {
		StartCoroutine(InitCoroutine());
	}

	IEnumerator InitCoroutine() {
		float deg = centerShots ? -((degreeStep * (countShots - 1)) / 2): 0f;
		Debug.Log("Deg: " +deg);

		for (int i = 0; i < countShots; i++) {
			FireEmitters(damage, speed, deg);

			deg += degreeStep;
			if (i <= countShots) {
				yield return new WaitForSeconds(delayBetweenShots);
			}
		}

		if (pingPong) {
			for (int i = 0; i < countShots; i++) {
				FireEmitters(damage, speed, deg);

				deg -= degreeStep;
				if (i <= countShots) {
					yield return new WaitForSeconds(delayBetweenShots);
				}
			}
		}

		gameObject.SetActive(false);
	}

	void FireEmitters(int damage, float speed, float angle) {
		foreach (Emitter e in emitters) {
			e.Fire(bulletPrefab, damage, speed, angle);
		}
	}
}
