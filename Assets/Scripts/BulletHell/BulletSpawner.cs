using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour {
	public float interval;
	public EmitterSet[] emitterSets;
	private float _timer;
	private bool _enabled = false;
	void OnEnable() {
		GameMgr.Instance.GetPubSubBroker().Subscribe(PubSub.Channel.BulletHellStart, OnBulletHellStart);
		GameMgr.Instance.GetPubSubBroker().Subscribe(PubSub.Channel.BulletHellEnd, OnBulletHellEnd);
	}
	void OnDisable() {
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.BulletHellStart, OnBulletHellStart);
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.BulletHellEnd, OnBulletHellEnd);
	}

	private EmitterSet _currentEmitterSet;
	public void Update() {
		if (!_enabled) return;
		if (_timer >= interval) {
			_timer = 0f;
			_currentEmitterSet = emitterSets[Random.Range(0, emitterSets.Length)];
			if (_currentEmitterSet == null) return;
			if (!_currentEmitterSet.gameObject.activeInHierarchy)
				_currentEmitterSet.gameObject.SetActive(true);
		} else {
			_timer += Time.deltaTime;
		}
	}

	void OnBulletHellStart(PubSub.Signal s) {
		_enabled = true;
	}
	void OnBulletHellEnd(PubSub.Signal s) {
		_enabled = false;
	}
}
