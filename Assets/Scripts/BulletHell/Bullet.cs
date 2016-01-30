using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float moveSpeed;
	public int damage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
	}

	//TODO get rid of bullet if out of range
}
