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
		GameObject go = Instantiate<GameObject>(GameMgr.Instance.bullet);
		go.transform.up = transform.up;
		if (rotation > 0.0f) {
			Debug.Log(go.transform.eulerAngles);
			go.transform.Rotate(new Vector3(0f, 0f, rotation));
			Debug.Log(go.transform.eulerAngles);
			Debug.Break();
		}
		go.transform.position = transform.position;
		Bullet b = go.GetComponent<Bullet>();
		b.moveSpeed = speed;
		b.damage = damage;
	}
}
