﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public float absoluteSpeed;
	public float targetX;

	ScrollController scrollController;

	void Start() {
		scrollController = transform.parent.GetComponentInParent<ScrollController>();
	}

	void Update() {
		transform.position += Vector3.right * scrollController.direction * absoluteSpeed * Time.deltaTime;
		if (scrollController.isMovingLeft) {
			if (transform.position.x < targetX) {
				GameMgr.Instance.GetPubSubBroker().Publish(PubSub.Channel.BulletHellTimeUp, this);
			}
		} else {
			if (transform.position.x > targetX) {
				GameMgr.Instance.GetPubSubBroker().Publish(PubSub.Channel.BulletHellTimeUp, this);
			}
		}
	}
}
