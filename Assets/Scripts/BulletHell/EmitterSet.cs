using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

public class EmitterSet : MonoBehaviour {
	const float DEGREES = 180f;

	public GameObject bulletPrefab;
	[FormerlySerializedAs("set")]
	public int countShots = 1;

	public float degreeStep = 0.0f;
	public float degreeJiggle = 0f;
	[FormerlySerializedAs("_delay")]
	public float delayBetweenShots = 0.0f;
	public bool pingPong = false;
	public bool centerShots = true;

	public int damage;
	public float speed;
	public float speedJiggle = 0f;

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
		Emitter[] emitters = GetComponentsInChildren<Emitter>();
		foreach (Emitter e in emitters) {
			e.Fire(bulletPrefab, 
			       damage, 
			       speed + Random.Range(-speedJiggle, speedJiggle), 
			       angle + Random.Range(-degreeJiggle, degreeJiggle));
		}
	}
}
