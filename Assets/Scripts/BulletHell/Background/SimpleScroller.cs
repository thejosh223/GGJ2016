﻿using UnityEngine;
using System.Collections;

public class SimpleScroller : MonoBehaviour {

	public Camera camera;
	public float direction;
	public float speed;
	public Sprite sprite;
	public int spriteLayer;
	public float glowDuration = 0f;

	void Start() {
		UpdateSprites();
	}

	void Update() {
		transform.position += Vector3.right * direction * speed * Time.deltaTime;
		UpdateSprites();
	}

	void UpdateSprites() {
		Vector3 leftBound = camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
		leftBound.z = 0f;
		Vector3 rightBound = camera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f));
		rightBound.z = 0f;

		if (transform.childCount == 0) {
			// Spawn one in left-most position
			SpawnSpriteAt(leftBound);
		}

		// Set LeftMost object and RightMost object
		Transform leftMostTransform = transform.GetChild(0);
		Transform rightMostTransform = transform.GetChild(transform.childCount - 1);

		// Spawning stuff on the left and right bounds
		while (rightMostTransform.position.x < rightBound.x) {
			SpriteRenderer s = rightMostTransform.GetComponent<SpriteRenderer>();
			Vector3 sBound = rightMostTransform.position + Vector3.right * rightMostTransform.localScale.x * s.sprite.texture.width / s.sprite.pixelsPerUnit;
			rightMostTransform = SpawnSpriteAt(sBound, true);
		}
		while (leftMostTransform.position.x > leftBound.x) {
			SpriteRenderer s = rightMostTransform.GetComponent<SpriteRenderer>();
			Vector3 sBound = leftMostTransform.position;
			leftMostTransform = SpawnSpriteAt(sBound, false);
		}

		// Destroying things on the left and right bounds
		leftBound += Vector3.left;
		rightBound += Vector3.right;
		for (int i = 0; i < transform.childCount; i++) {

			// Destroy the lefts
			Transform leftTransform = transform.GetChild(i);
			Sprite leftSprite = leftTransform.GetComponent<SpriteRenderer>().sprite;
			Vector3 leftPosition = leftTransform.position + Vector3.right * leftTransform.localScale.x * leftSprite.texture.width / leftSprite.pixelsPerUnit;
			if (leftPosition.x < leftBound.x) {
				Destroy(transform.GetChild(i).gameObject);
			}

			// Destroy the rights
			Transform rightTransform = transform.GetChild(transform.childCount - i - 1);
			if (rightTransform.position.x > rightBound.x) {
				Destroy(rightTransform.gameObject);
			}
		}
	}

	Transform SpawnSpriteAt(Vector3 pos, bool isRight = true) {
		GameObject g = new GameObject("BG_" + Random.Range(1000, 9999));
		g.transform.SetParent(transform);
		g.transform.position = pos;
		g.SetLayerRecursively(gameObject.layer);
		if (isRight) {
			g.transform.SetAsLastSibling();
		} else {
			g.transform.SetAsFirstSibling();
		}

		SpriteRenderer s = g.AddComponent<SpriteRenderer>();
		s.sprite = sprite;
		s.sortingOrder = spriteLayer;

		if (!isRight) {
			g.transform.position -= Vector3.right * g.transform.localScale.x * s.sprite.texture.width / s.sprite.pixelsPerUnit;
		}

		if (glowDuration > 0f) {
			Glower glower = g.AddComponent<Glower>();
			glower.alphaDuration = glowDuration;
		}

		return g.transform;
	}
}
