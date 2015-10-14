using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Leechionnaire : Spell {
	private static Leechionnaire lastCast;
	private List<GameObject> leeches = new List<GameObject> ();

	void Start () {
		DestroyInstances ();
		lastCast = this;
	}

	public override float Cast () {
		Transform transform = GetComponent<Transform> ();

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject enemy in enemies) {
			GameObject clone = Instantiate (Resources.Load ("Spells/Leech") as GameObject, transform.position, transform.rotation) as GameObject;
			leeches.Add (clone);
			clone.GetComponent<Leech> ().SetTarget (enemy);
		}

		return cooldown;
	}

	void DestroyInstances () {
		if (lastCast == null)
			return;

		foreach (GameObject leech in lastCast.leeches) {
			if (leech != null)
				Destroy (leech);
		}
		Destroy (lastCast.gameObject);

		lastCast = null;
	}
}
