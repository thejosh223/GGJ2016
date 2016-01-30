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
		enemySpawner.SpawnEnemy(15f);
	}

}
