using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	const int MAX_HORNINESS = 100;

	public int horniness { get; set; }
	public float horninessPercentage { get { return (float) horniness / (float) MAX_HORNINESS; } }

	private bool _isDead = false;
	private bool isSweating = false;

	Animator animator;
	Coroutine sweatingCoroutine;

	void Awake() {
		__instance = this;
		horniness = MAX_HORNINESS;
	}

	void Start() {
		animator = GetComponentInChildren<Animator>();
		_isDead = false;
	}

	void Update() {
		if (isSweating) {
			Enemy enemy = GetComponentInParent<EnemySpawner>().currentEnemy;
			if (enemy != null) {
				animator.speed = enemy.currentPercentage * 2f;
			}
		}
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
	}

	public void SweatForSeconds(float duration) {
		if (sweatingCoroutine != null) {
			StopCoroutine(sweatingCoroutine);
		}
		sweatingCoroutine = StartCoroutine(SweatCoroutine(duration));
	}

	IEnumerator SweatCoroutine(float duration) {
		StartSweating();
		yield return new WaitForSeconds(duration);
		StopSweating();
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
	}
	
	void OnDestroy() {
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.EnemyCollide, OnCollideWithBullet);
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.BulletHellStart, StartBulletHell);
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.BulletHellEnd, StopBulletHell);
	}
	
	void OnDisable() {
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.EnemyCollide, OnCollideWithBullet);
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.BulletHellStart, StartBulletHell);
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.BulletHellEnd, StopBulletHell);
	}
	
	// ----------------- Singleton ----------------- //

	static Player __instance;
	public static Player Instance {
		get { return __instance; }
	}

}
