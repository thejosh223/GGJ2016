using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	const int MAX_HORNINESS = 100;

	public int horniness { get; set; }
	public float horninessPercentage { get { return (float) horniness / (float) MAX_HORNINESS; } }

	private bool _isDead = false;

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

	void OnCollideWithBullet(PubSub.Signal signal) {
		Debug.Log(signal.data["damage"]);
		if (signal.data != null && signal.data.ContainsKey("damage")) {
			horniness -= (int)signal.data["damage"];
		} else {
			horniness -= 10;
		}
		SweatForSeconds(2f);

		if (horniness <= 0 && !_isDead) {
			GameMgr.Instance.GetPubSubBroker().Publish(PubSub.Channel.PlayerDead, this);
			_isDead = true;
		}
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

	public void StartSweating() {
		animator.SetBool("sweating", true);
	}

	public void StopSweating() {
		animator.SetBool("sweating", false);
	}

	// ----------------- Subscriptions ----------------- //

	void OnEnable() {
		GameMgr.Instance.GetPubSubBroker().Subscribe(PubSub.Channel.EnemyCollide, OnCollideWithBullet);
	}
	
	void OnDestroy() {
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.EnemyCollide, OnCollideWithBullet);
	}
	
	void OnDisable() {
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.EnemyCollide, OnCollideWithBullet);
	}
	
	// ----------------- Singleton ----------------- //

	static Player __instance;
	public static Player Instance {
		get { return __instance; }
	}

}
