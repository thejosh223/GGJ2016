using UnityEngine;
using System.Collections;

public enum Location
{
	Suburb = 0,
	Alley = 1,
	City = 2,
	Office = 3
}

public static class LocationExtensions {

	public static Sprite GetRandomLocationSprite(this Location loc) {
		int[] array = {};
		switch (loc) {
			case Location.Suburb:
				array = new int[] { 1 };
				break;
			case Location.Alley:
				array = new int[] { 2 };
				break;
			case Location.City:
				array = new int[] { 3 };
				break;
			case Location.Office:
				array = new int[] { 4, 5 };
				break;
		}

		return Resources.Load<Sprite>("Sprites/Characters/girl" + array[Random.Range(0, array.Length - 1)]);
	}

}