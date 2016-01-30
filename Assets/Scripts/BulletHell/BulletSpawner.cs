using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour {
	public float interval;
	public EmitterSet[] emitterSets;
	private float _timer;
	public void Init() {

	}

	private EmitterSet _currentEmitterSet;
	public void Update() {
		if (_timer >= interval) {
			_timer = 0f;
			_currentEmitterSet = emitterSets[Random.Range(0, emitterSets.Length)];
			if (!_currentEmitterSet.gameObject.activeInHierarchy)
				_currentEmitterSet.gameObject.SetActive(true);
		} else {
			_timer += Time.deltaTime;
		}
	}
}
