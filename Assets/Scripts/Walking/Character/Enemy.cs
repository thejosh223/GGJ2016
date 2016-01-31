using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour {

	public SpriteRenderer spriteRenderer;
	public float absoluteSpeed;
	public float targetX;

	float startX;
	ScrollController scrollController;

	void Start() {
		scrollController = transform.parent.GetComponentInParent<ScrollController>();
		spriteRenderer.sprite = scrollController.currentLocation.GetRandomLocationSprite();

		startX = transform.position.x;
	}

	void Update() {
		transform.position += Vector3.right * scrollController.direction * absoluteSpeed * Time.deltaTime;

		if (scrollController.isMovingLeft && transform.position.x < targetX ||
		    !scrollController.isMovingLeft && transform.position.x > targetX) {

			GameMgr.Instance.GetPubSubBroker().Publish(PubSub.Channel.BulletHellEnd, this);
			Destroy(gameObject);
		}
	}

	public float currentPercentage {
		get {
			float width = targetX - startX;
			return Mathf.Clamp01((transform.position.x - startX) / width);
		}
	}
}
