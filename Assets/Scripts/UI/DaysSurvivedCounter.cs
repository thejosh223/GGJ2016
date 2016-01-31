using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DaysSurvivedCounter : MonoBehaviour {

	public string prefixString;
	public int addToLevelsDefeated = 0;
	Text text;

	void Start() {
		text = GetComponent<Text>();
	}

	void Update() {
		text.text = prefixString + (PersistentData.levelsDefeated + addToLevelsDefeated);
	}

}
