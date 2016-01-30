using UnityEngine;
using System.Collections;

public class FighterController : MonoBehaviour {
	public float moveSpeed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(moveSpeed * Input.GetAxis("Horizontal"), moveSpeed * Input.GetAxis("Vertical")));
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy") {
			GameMgr.Instance.GetPubSubBroker().Publish(PubSub.Channel.EnemyCollide, this);
		}
	}
}
