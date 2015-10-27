using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Armyodillos : Spell {
	public int number;

	private float xMin, xMax, yMin, yMax;

	void Awake () {
		yMax = Camera.main.orthographicSize;
		yMin = -yMax;
		xMax = -Camera.main.orthographicSize * Screen.width / Screen.height;
		xMax -= 5f;
		xMin = xMax - 20f;
	}

	public override void Cast () {
		for (int i = 0; i < number; i++) {
			Vector3 pos = new Vector3 (Random.Range (xMin, xMax), Random.Range (yMin, yMax), 0f);

			GameObject clone = Instantiate (Resources.Load ("Spells/Armadillo") as GameObject, pos, Quaternion.identity) as GameObject;
			clone.GetComponent<Armadillo> ().Roll ();
		}

		Destroy (this.gameObject);
	}
}
