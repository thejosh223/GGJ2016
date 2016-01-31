using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DaysSurvivedCounter : MonoBehaviour {

	public string prefixString;
	Text text;

	void Start() {
		text = GetComponent<Text>();
	}

	void Update() {
		text.text = prefixString + PersistentData.levelsDefeated;
	}

}
