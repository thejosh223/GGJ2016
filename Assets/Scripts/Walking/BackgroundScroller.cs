using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour {

	public Sprite[] sprites;
	public float scrollSpeedMultiplier;
//	public bool makeContinuous;

	ScrollController parentScroller;
	Transform leftMostObject;
	Transform rightMostObject;

	void Start() {
		parentScroller = GetComponentInParent<ScrollController>();
		UpdateSprites();
	}

	void Update() {
		transform.position += Vector3.right * parentScroller.direction * parentScroller.speed * scrollSpeedMultiplier * Time.deltaTime;
		UpdateSprites();
	}

	void UpdateSprites() {
		Camera cam = parentScroller.camera;
		Vector3 leftBound = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f));
		leftBound.z = 0f;
		Vector3 rightBound = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0f));
		rightBound.z = 0f;

		if (transform.childCount == 0) {
			// Spawn one in left-most position
			leftMostObject = SpawnRandomSpriteAt(leftBound);
			rightMostObject = leftMostObject;
		}

		// Spawning stuff on the left and right bounds
		while (rightMostObject.position.x < rightBound.x) {
			SpriteRenderer s = rightMostObject.GetComponent<SpriteRenderer>();
			Vector3 sBound = rightMostObject.position + Vector3.right * rightMostObject.localScale.x * s.sprite.texture.width / s.sprite.pixelsPerUnit;
			rightMostObject = SpawnRandomSpriteAt(sBound);
		}
		while (leftMostObject.position.x > leftBound.x) {
			SpriteRenderer s = rightMostObject.GetComponent<SpriteRenderer>();
			Vector3 sBound = leftMostObject.position;
			leftMostObject = SpawnRandomSpriteAt(sBound, false);
		}

		// Destroying things on the left and right bounds
//		for (int i = 0; i < 
	}

	Transform SpawnRandomSpriteAt(Vector3 pos, bool isRight = true) {
		GameObject g = new GameObject();
		g.transform.SetParent(transform);
		g.transform.position = pos;
		if (isRight) {
			g.transform.SetAsLastSibling();
		} else {
			g.transform.SetAsLastSibling();
		}
		SpriteRenderer s = g.AddComponent<SpriteRenderer>();
		s.sprite = sprites[Random.Range(0, sprites.Length)];

		if (!isRight) {
			g.transform.position -= Vector3.right * g.transform.localScale.x * s.sprite.texture.width / s.sprite.pixelsPerUnit;
		}

		return g.transform;
	}

}
