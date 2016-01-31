using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour {

	[Range(0, 1)]
	public float intervalRatio = 0.75f;
	public EmitterSet[] emitterSets;
	public bool debugMode = false;

	bool isFiring = false;
	float timeToFireNextEmitter;

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

		// Used for debugging the emitters
		if (Input.GetKeyDown(KeyCode.D)) {
			debugMode = !debugMode;
		}
		for (int i = 1; i <= 9; i++) {
			if (Input.GetKeyDown("" + i)) {
				emitterSets[i - 1].gameObject.SetActive(true);
			}
		}
		if (debugMode) {
			return;
		}

		// Shooting Control

		if (!isFiring) {
			return;
		}

		if (Time.time > timeToFireNextEmitter) {

			_currentEmitterSet = emitterSets[Random.Range(0, emitterSets.Length)];
			if (_currentEmitterSet == null) {
				return;
			}

			if (!_currentEmitterSet.gameObject.activeInHierarchy && enabled) {
				_currentEmitterSet.gameObject.SetActive(true);
				timeToFireNextEmitter = Time.time + _currentEmitterSet.spawningDuration + _currentEmitterSet.travelDurationEstimate * intervalRatio;
			}
		}
	}

	void OnBulletHellStart(PubSub.Signal s) {
		isFiring = true;
		WalkingStateMachine w = s.sender as WalkingStateMachine;
		if (w != null) {
			Debug.Log("adjusting difficulty to: "+(intervalRatio*GameMgr.Instance.difficultyInterval));
			if (((float)w.level*GameMgr.Instance.difficultyInterval) > 0.0f)
				intervalRatio *= GameMgr.Instance.difficultyInterval;
		}
	}

	void OnBulletHellEnd(PubSub.Signal s) {
		isFiring = false;

		foreach (EmitterSet e in emitterSets) {
			e.gameObject.SetActive(false);
		}
	}
}
