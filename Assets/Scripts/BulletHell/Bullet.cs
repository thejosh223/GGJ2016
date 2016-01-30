using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float moveSpeed;
	public int damage;

	void Update () {
		transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
	}

	//TODO get rid of bullet if out of range
	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log("bullet hit something");
		if (other.gameObject.tag == "Environment" || other.gameObject.tag == "Player") {
			Destroy(gameObject);
		}
	}
}
