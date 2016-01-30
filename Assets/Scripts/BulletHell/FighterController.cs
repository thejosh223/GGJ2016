using UnityEngine;
using System.Collections;

public class FighterController : MonoBehaviour {
	public float moveSpeed = 1f;
	private Vector3 dimensions = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		if (h == 0.00f && v == 0.00f) return;

		Vector3 pos = new Vector3(moveSpeed * h * Time.deltaTime, moveSpeed * v * Time.deltaTime);
		Vector3 newPos = transform.position + pos;

		//lol such hack
		float x = h > 0 ? GetDimensions2D().x : 0f;
		float y = v > 0 ? GetDimensions2D().y : 0f;
		if (!GameMgr.Instance.IsBeyondBulletCamera(new Vector3(newPos.x + x, newPos.y + y, 0f)))
			transform.Translate(pos);
	}

	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log("collision :"+other.gameObject);
		if (other.gameObject.tag == "Enemy") {
			GameMgr.Instance.GetPubSubBroker().Publish(PubSub.Channel.EnemyCollide, this);
			Debug.Log("boom");
		}
	}

	Vector3 GetDimensions2D() {
		if (dimensions == Vector3.zero) {
			Renderer r = GetComponent<Renderer>();

			dimensions = new Vector3(r.bounds.size.x, r.bounds.size.y, 0);
		}
		return dimensions;
	}
}