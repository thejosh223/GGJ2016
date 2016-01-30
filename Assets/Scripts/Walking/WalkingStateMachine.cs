using UnityEngine;
using System.Collections;
using MonsterLove.StateMachine;

public class WalkingStateMachine : StateBehaviour {

	public enum WalkingStates {
		IdleWalking,
		EnemySpawn,
		BulletHell,
		PostBulletHell
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

	// --- Idle --- //
	[Header("Idle Variables")]
	public float idleDurationMin = 4f;
	public float idleDurationMax = 6f;
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

	void BulletHell_Exit() {
		GameMgr.Instance.GetPubSubBroker().Unsubscribe(PubSub.Channel.BulletHellEnd, ChangeStateToPostBulletHell);
	}

	void ChangeStateToPostBulletHell(PubSub.Signal s) {
		ChangeState(WalkingStates.PostBulletHell);
	}

	// --- Post Bullet Hell --- //
	void PostBulletHell_Enter() {
		Debug.Log("[WalkingStateManger] PostBulletHell Start.");

		GameMgr.Instance.GetPubSubBroker().Publish(PubSub.Channel.BulletHellEnd, this);
		ChangeState(WalkingStates.IdleWalking);
		SuccessMessageUI.Instance.ShowMessage(SuccessMessageUI.SuccessMessage.Success, () => ChangeState(WalkingStates.IdleWalking));
	}

	void PostBulletHell_Exit() {
		FadeOutOverlay.Instance.FadeOutIn(0.25f, 0.1f, () => GameMgr.Instance.GetPubSubBroker().Publish(PubSub.Channel.PostBulletHellEnd, this));
	}


}
