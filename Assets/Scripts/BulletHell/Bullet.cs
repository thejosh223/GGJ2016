using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float moveSpeed;
	public int damage;

	void Update () {
		transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
	}

	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log("bullet hit something");
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Environment") {
			Destroy(gameObject);
        }
    }
}
