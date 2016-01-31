using UnityEngine;
using System.Collections;
using MonsterLove.StateMachine;

public class WalkingStateMachine : StateBehaviour {

	public enum WalkingStates {
		IdleWalking,
		EnemySpawn,
		BulletHell,
		EnemyLeave,
		PostBulletHell
	}

	void OnEnable() {
		GameMgr.Instance.GetPubSubBroker().Subscribe(PubSub.Channel.PlayerDead, OnPlayerDead);
	}

	void OnDisable() {
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.PlayerDead, OnPlayerDead);
	}

	void Start() {
		Initialize<WalkingStates>();
		ChangeState(WalkingStates.IdleWalking);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
			Time.timeScale *= Mathf.Sqrt(2f);
		} else if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
			Time.timeScale /= Mathf.Sqrt(2f);
		}
	}

	protected override void ChangeState(System.Enum newState)
	{
		if (this != null)
			stateMachine.ChangeState(newState);
	}

	// --- Idle --- //
	[Header("Idle Variables")]
	public float idleDurationMin = 4f;
	public float idleDurationMax = 6f;
	public int level = 0;
	float startGameTime;

	void IdleWalking_Enter() {
		Debug.Log("[WalkingStateManger] IdleWalking Start.");
		startGameTime = Time.time + Random.Range(idleDurationMin, idleDurationMax);
	}

	void IdleWalking_Update() {
		if (startGameTime < Time.time || Input.GetKeyDown(KeyCode.F)) {
			ChangeState(WalkingStates.EnemySpawn);
		}
	}

	// --- Enemy Spawn --- //
	[Header("Enemy Spawn Variables")]
	public EnemySpawner enemySpawner;

	void EnemySpawn_Enter() {
		Debug.Log("[WalkingStateManger] EnemySpawn Start.");

		enemySpawner.SpawnEnemy(15f);
		ChangeState(WalkingStates.BulletHell);
	}

	// --- Bullet Hell --- //
	void BulletHell_Enter() {
		Debug.Log("[WalkingStateManger] BulletHell Start.");
		GameMgr.Instance.GetPubSubBroker().Subscribe(PubSub.Channel.BulletHellEnd, ChangeStateToPostBulletHell);
		GameMgr.Instance.GetPubSubBroker().Publish(PubSub.Channel.BulletHellStart, this);
	}

	void ChangeStateToPostBulletHell(PubSub.Signal s) {
		ChangeState(WalkingStates.PostBulletHell);
	}

	// --- Post Bullet Hell --- //
	void PostBulletHell_Enter() {
		Debug.Log("[WalkingStateManger] PostBulletHell Start.");
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.BulletHellEnd, ChangeStateToPostBulletHell);
	}

	bool stateHasSuccessPlayed = false;
	void PostBulletHell_Update() {
		if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !stateHasSuccessPlayed) {
			SuccessMessageUI.Instance.ShowMessage(SuccessMessageUI.SuccessMessage.Success, () => ChangeState(WalkingStates.IdleWalking));
			stateHasSuccessPlayed = true;
		}
	}

	void PostBulletHell_Exit() {
		FadeOutOverlay.Instance.FadeOutIn(0.25f, 0.1f, () => {
			stateHasSuccessPlayed = false;
			GameMgr.Instance.GetPubSubBroker().Publish(PubSub.Channel.PostBulletHellEnd, this);
		});

		level++;
		PersistentData.levelsDefeated = level;
	}

	void OnPlayerDead(PubSub.Signal s) {
		Debug.Log("You Died!");
		PersistentData.levelsDefeated = level;
		FadeOutOverlay.Instance.FadeOut(1.5f, () => Application.LoadLevel("GameOver"));
	}
}
