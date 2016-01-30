using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float horniness { get; set; }

	Animator animator;
	Coroutine sweatingCoroutine;

	void Awake() {
		__instance = this;
	}

	void Start() {
		animator = GetComponentInChildren<Animator>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha9)) {
			SweatForSeconds(2f);
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
