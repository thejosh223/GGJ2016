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
	// Use this for initialization
	void Start () {
		ChangeState(States.Init);
	}
#region Init state
	void Init_Enter() {
	}

	void Init_Update() {
		
    }

	void Init_Exit() {
		
    }
#endregion
}
