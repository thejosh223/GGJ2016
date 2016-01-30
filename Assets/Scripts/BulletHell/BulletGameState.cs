using UnityEngine;
using System.Collections;
using MonsterLove.StateMachine;

public class BulletGameState : StateBehaviour {
	private enum States {
		Init,
		Play,
		End
	}

	void Awake() {
		Initialize<States>();
	}

	void Start () {
		ChangeState(States.Init);
	}
#region Init state
	void Init_Enter() {
		ChangeState(States.Play);
	}

	void Init_Update() {
    }

	void Init_Exit() {
    }
#endregion

#region Play state
	void Play_Enter() {

	}

	void Play_Update() {

	}

	void Play_Exit() {

	}
#endregion
}
