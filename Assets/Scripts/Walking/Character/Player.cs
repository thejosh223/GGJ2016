using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	const int MAX_HORNINESS = 100;

	public int horniness { get; set; }
	public float horninessPercentage { get { return (float) horniness / (float) MAX_HORNINESS; } }

	private bool _isDead = false;
	private bool isSweating = false;

	Animator animator;
	Transform spriteTransform;
	Coroutine intensificationCoroutine;

	void Awake() {
		__instance = this;
		horniness = MAX_HORNINESS;
	}

	void Start() {
		animator = GetComponentInChildren<Animator>();
		spriteTransform = GetComponentInChildren<SpriteRenderer>().transform;
		_isDead = false;
	}

	void Update() {
		if (isSweating) {
			Enemy enemy = GetComponentInParent<EnemySpawner>().currentEnemy;
			if (enemy != null) {
				animator.speed = enemy.currentPercentage * 2f;
			}
		}

//		if (Input.GetKeyDown(KeyCode.I)) {
//			IntensifyForSeconds(1f);
//		}
	}

	void OnCollideWithBullet(PubSub.Signal signal) {
		Debug.Log(signal.data["damage"]);
		if (signal.data != null && signal.data.ContainsKey("damage")) {
			horniness -= (int)signal.data["damage"];
		} else {
			horniness -= 10;
		}

		if (horniness <= 0 && !_isDead) {
			GameMgr.Instance.GetPubSubBroker().Publish(PubSub.Channel.PlayerDead, this);
			_isDead = true;
		}

		// Animations
//		SweatForSeconds(2f);
		IntensifyForSeconds(1f);
	}

	void OnWaveEnd(PubSub.Signal signal) {
		horniness += GameMgr.Instance.hpRegenPerLevel;
		if (horniness > MAX_HORNINESS)
			horniness = 100;
	}

	public void IntensifyForSeconds(float duration) {
		if (intensificationCoroutine != null) {
			StopCoroutine(intensificationCoroutine);
		}
		intensificationCoroutine = StartCoroutine(StartIntensification(duration));
	}

	IEnumerator StartIntensification(float duration) {
		float startTime = Time.time;
		while (Time.time < (startTime + duration)) {
			spriteTransform.localPosition = new Vector3(Mathf.Sin(Time.time * Random.Range(100f, 128f)) * 0.02f, 
			                                            Mathf.Sin(Time.time * Random.Range(100f, 128f)) * 0.02f, 
			                                            0f);
			yield return null;
		}
		spriteTransform.localPosition =	Vector3.zero;
	}

	void StartBulletHell(PubSub.Signal s) {
		StartSweating();
	}

	void StopBulletHell(PubSub.Signal s) {
		StopSweating();
		animator.speed = 1f;
	}

	public void StartSweating() {
		isSweating = true;
		animator.SetBool("sweating", true);
	}

	public void StopSweating() {
		isSweating = false;
		animator.SetBool("sweating", false);
	}

	// ----------------- Subscriptions ----------------- //

	void OnEnable() {
		GameMgr.Instance.GetPubSubBroker().Subscribe(PubSub.Channel.EnemyCollide, OnCollideWithBullet);
		GameMgr.Instance.GetPubSubBroker().Subscribe(PubSub.Channel.BulletHellStart, StartBulletHell);
		GameMgr.Instance.GetPubSubBroker().Subscribe(PubSub.Channel.BulletHellEnd, StopBulletHell);
		GameMgr.Instance.GetPubSubBroker().Subscribe(PubSub.Channel.PostBulletHellEnd, OnWaveEnd);
	}
	
	void OnDestroy() {
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.EnemyCollide, OnCollideWithBullet);
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.BulletHellStart, StartBulletHell);
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.BulletHellEnd, StopBulletHell);
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.PostBulletHellEnd, OnWaveEnd);
	}
	
	void OnDisable() {
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.EnemyCollide, OnCollideWithBullet);
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.BulletHellStart, StartBulletHell);
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.BulletHellEnd, StopBulletHell);
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.PostBulletHellEnd, OnWaveEnd);
	}
	
	// ----------------- Singleton ----------------- //

	static Player __instance;
	public static Player Instance {
		get { return __instance; }
	}

}
