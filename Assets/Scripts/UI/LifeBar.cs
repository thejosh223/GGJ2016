using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {

	Player player;

	void Start() {
		player = Player.Instance;
	}

	void Update() {
		Vector3 v = transform.localScale;
		v.x = player.horninessPercentage;
		transform.localScale = v;
	}

}
