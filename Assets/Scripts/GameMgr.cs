using UnityEngine;
using System.Collections;
using PubSub;

public class GameMgr : MonoBehaviour {
	static GameMgr _instance;
	private PubSubBroker _pubsubMgr = new PubSubBroker();
	void OnEnable () {
		if (_instance == null) {
			_instance = this;
			Debug.Log(_instance);
			DontDestroyOnLoad(_instance);
		}
	}
	
	public PubSubBroker GetPubSubBroker() {
		return _pubsubMgr;
	}
	
	static public GameMgr Instance {
		get { return _instance; }
	}
}