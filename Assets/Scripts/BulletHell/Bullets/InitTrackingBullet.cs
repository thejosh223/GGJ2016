using UnityEngine;
using System.Collections;

public class InitTrackingBullet : Bullet {

	void Start() {
		transform.up = FighterController.Instance.transform.position - transform.position;
	}

}
