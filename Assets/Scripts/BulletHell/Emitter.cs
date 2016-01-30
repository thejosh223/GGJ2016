using UnityEngine;
using System.Collections;

public class Emitter : MonoBehaviour {

	public int damage = 1;
	public float speed = 1f;

	public float sineFrequency;
	public float cosFrequency;

	public void Fire (GameObject prefab, int damage, float speed, float rotation = 0f) {
		GameObject go = Instantiate<GameObject>(prefab);
		go.transform.up = transform.up;
		go.transform.eulerAngles = new Vector3(0f, 0f, go.transform.eulerAngles.z + rotation);
		go.transform.position = transform.position;

		Bullet b = go.GetComponent<Bullet>();
		b.moveSpeed = speed;
		b.damage = damage;
	}
}
