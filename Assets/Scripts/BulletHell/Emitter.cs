using UnityEngine;
using System.Collections;

public class Emitter : MonoBehaviour {
	public int damage = 1;
	//public MoveFunction moveFunction;
	public float speed = 1f;
	
	// Use this for initialization
	public void Init () {
		GameObject go = Instantiate<GameObject>(GameMgr.Instance.bullet);
		go.transform.up = transform.up;
		go.transform.position = transform.position;
		Bullet b = go.GetComponent<Bullet>();
		b.moveSpeed = speed;
		b.damage = damage;
	}
}
