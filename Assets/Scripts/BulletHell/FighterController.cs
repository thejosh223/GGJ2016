using UnityEngine;
using System.Collections;

public class FighterController : MonoBehaviour {

	public float moveSpeed = 1f;
	private Vector3 dimensions = Vector3.zero;

	public Sprite damageSprite;
	Sprite defaultSprite;
	SpriteRenderer spriteRenderer;

	void Awake() {
		__instance = this;
	}

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		defaultSprite = spriteRenderer.sprite;
	}

	IEnumerator AnimateDamage() {
		spriteRenderer.sprite = damageSprite;
		yield return new WaitForSeconds(0.5f);
		spriteRenderer.sprite = defaultSprite;
	}

	void Update () {
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		if (h == 0.00f && v == 0.00f) return;

		Vector3 pos = new Vector3(moveSpeed * h * Time.deltaTime, moveSpeed * v * Time.deltaTime);
		Vector3 newPos = transform.position + pos;

		//lol such hack
		Vector3 dim = GetDimensions2D();
//		float x = h > 0 ? GetDimensions2D().x : 0f;
//		float y = v > 0 ? GetDimensions2D().y : 0f;
		if (!GameMgr.Instance.IsBeyondBulletCamera(new Vector3(newPos.x + dim.x / 2, newPos.y + dim.y / 2, 0f)) &&
		    !GameMgr.Instance.IsBeyondBulletCamera(new Vector3(newPos.x - dim.x / 2, newPos.y - dim.y / 2, 0f))) {
			transform.Translate(pos);
		}
	}

	Hashtable data = new Hashtable();
	void OnCollisionEnter2D(Collision2D other) {
//		Debug.Log("collision :"+other.gameObject);
		if (other.gameObject.tag == "Enemy") {
			data["damage"] = other.gameObject.GetComponent<Bullet>().damage;
			GameMgr.Instance.GetPubSubBroker().Publish(PubSub.Channel.EnemyCollide, this, data);

			// Animate damage
			StartCoroutine(AnimateDamage());
		}
	}

	Vector3 GetDimensions2D() {
		if (dimensions == Vector3.zero) {
			Renderer r = GetComponent<Renderer>();
			dimensions = new Vector3(r.bounds.size.x, r.bounds.size.y, 0);
		}
		return dimensions;
	}

	private static FighterController __instance;
	public static FighterController Instance {
		get { return __instance; }
	}

}