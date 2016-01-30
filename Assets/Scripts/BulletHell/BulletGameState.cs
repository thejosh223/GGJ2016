using UnityEngine;
using System.Collections;
using MonsterLove.StateMachine;

public class BulletGameState : StateBehaviour {
	private enum States {
		Init,
		Play,
		End
	}

	private EnemySpawner _enemySpawner = new EnemySpawner();

	void Awake() {
		Initialize<States>();
	}
	// Use this for initialization
	void Start () {
		ChangeState(States.Init);
	}
#region Init state
	void Init_Enter() {
		_enemySpawner.Init();
	}

	void Init_Update() {
		
    }

	void Init_Exit() {
		
    }
#endregion
}
