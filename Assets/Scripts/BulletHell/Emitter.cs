using UnityEngine;
using System.Collections;

public class Emitter : MonoBehaviour {
	public int damage = 1;
	//public MoveFunction moveFunction;
	public float speed = 1f;

	public float sineFrequency;
	public float cosFrequency;

	// Use this for initialization
	public void Init (int damage, float speed, float rotation=0f) {
		Debug.Log("init");
		GameObject go = Instantiate<GameObject>(GameMgr.Instance.bullet);
		go.transform.up = transform.up;
		if (rotation > 0.0f) {
			//Debug.Log(go.transform.eulerAngles);
			go.transform.eulerAngles = new Vector3(0f, 0f, go.transform.eulerAngles.z+rotation);
		}
		go.transform.position = transform.position;
		Bullet b = go.GetComponent<Bullet>();
		b.moveSpeed = speed;
		b.damage = damage;
	}
}
