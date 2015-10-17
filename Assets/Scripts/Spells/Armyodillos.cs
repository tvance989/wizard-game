using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Armyodillos : Spell {
	public int number;

	public override void Cast () {GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		float y = Camera.main.orthographicSize;
		float x = y * Screen.width / Screen.height;
		for (int i = 0; i < number; i++) {
			Vector3 pos = new Vector3 (Random.Range (-x-10f, -x), Random.Range (-y, y), 0f);
			//.change spawn to somewhere off screen. maybe roll in from same direction or all random
			GameObject clone = Instantiate (Resources.Load ("Spells/Armadillo") as GameObject, pos, Quaternion.identity) as GameObject;
			clone.GetComponent<Armadillo> ().Roll ();
		}

		Destroy (this.gameObject);
	}
}
