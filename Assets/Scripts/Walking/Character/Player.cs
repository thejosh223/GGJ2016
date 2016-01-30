using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	const int MAX_HORNINESS = 100;
	public int horniness { get; set; }
	public float horninessPercentage { get { return (float) horniness / (float) MAX_HORNINESS; } }

	Animator animator;
	Coroutine sweatingCoroutine;

	void Awake() {
		__instance = this;
		horniness = MAX_HORNINESS;
	}

	void Start() {
		animator = GetComponentInChildren<Animator>();
		
		GameMgr.Instance.GetPubSubBroker().Subscribe(PubSub.Channel.EnemyCollide, OnCollideWithBullet);
	}

	void OnCollideWithBullet(PubSub.Signal signal) {
		Debug.Log(signal.data["damage"]);
		if (signal.data != null && signal.data.ContainsKey("damage"))
			horniness -= (int)signal.data["damage"];
		else
			horniness -= 10;

		SweatForSeconds(2f);

		if (horniness <= 0) {

		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha9)) {
			SweatForSeconds(2f);
		}
		if (Input.GetKeyDown(KeyCode.L)) {
			horniness -= 10;
		} else  if (Input.GetKeyDown(KeyCode.P)) {
			horniness += 10;
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

	static Player __instance;
	public static Player Instance {
		get { return __instance; }
	}

}
