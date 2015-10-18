using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Leechionnaire : Spell {
	private static List<GameObject> leeches = new List<GameObject> ();

	void Awake () {
		DestroyPrevious ();
	}

	public override void Cast () {
		Transform transform = GetComponent<Transform> ();

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject enemy in enemies) {
			GameObject clone = Instantiate (Resources.Load ("Spells/Leech") as GameObject, transform.position, transform.rotation) as GameObject;
			leeches.Add (clone);
			clone.GetComponent<Leech> ().SetTarget (enemy);
		}

		Destroy (this.gameObject);
	}

	void DestroyPrevious () {
		foreach (GameObject leech in Leechionnaire.leeches)
			if (leech != null)
				Destroy (leech);

		Leechionnaire.leeches = new List<GameObject> ();
	}
}
