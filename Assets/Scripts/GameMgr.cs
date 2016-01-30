using UnityEngine;
using System.Collections;
using PubSub;

public class GameMgr : MonoBehaviour {
	static GameMgr _instance;
	private PubSubBroker _pubsubMgr = new PubSubBroker();
	Camera bulletCamera;
	public Vector3 bulletCameraUpperLeft = Vector3.zero;
	public Vector3 bulletCameraLowerRight = Vector3.zero;
	public GameObject bullet;


	void OnEnable () {
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad(_instance);
		}
	}
	
	public PubSubBroker GetPubSubBroker() {
		return _pubsubMgr;
	}

	public Camera GetBulletCamera() {
		if (bulletCamera != null)
			return bulletCamera;

		GameObject go = GameObject.FindGameObjectWithTag("BulletCamera");
		if (go != null)
		{
			bulletCamera = go.GetComponent<Camera>();
			return bulletCamera;
		}

		return null;
	}

	public bool IsBeyondBulletCamera(Vector3 pos) {
		if (GetBulletCamera() == null) return false;

		if (bulletCameraUpperLeft == Vector3.zero && bulletCameraLowerRight == Vector3.zero) {
			bulletCameraUpperLeft = GetBulletCamera().ViewportToWorldPoint(new Vector3(0, 1, 0));
			bulletCameraLowerRight = GetBulletCamera().ViewportToWorldPoint(new Vector3(1, 0, 0));
		}

		Debug.Log(bulletCameraUpperLeft);
		Debug.Log(bulletCameraLowerRight);

		bool beyondX = pos.x <= bulletCameraUpperLeft.x || pos.x >= bulletCameraLowerRight.x;
		bool beyondY = pos.y <= bulletCameraLowerRight.y || pos.y >= bulletCameraUpperLeft.y;
		return beyondX || beyondY;
	}

	static public GameMgr Instance {
		get { return _instance; }
	}
}