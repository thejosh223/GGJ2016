using UnityEngine;
using System.Collections;

public class FighterController : MonoBehaviour {
	public float moveSpeed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(moveSpeed * Input.GetAxisRaw("Horizontal"), moveSpeed * Input.GetAxisRaw("Vertical")));
	}

	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log("collision :"+other.gameObject);
		if (other.gameObject.tag == "Enemy") {
			GameMgr.Instance.GetPubSubBroker().Publish(PubSub.Channel.EnemyCollide, this);
			Debug.Log("boom");
		}
	}
}