using UnityEngine;
using System.Collections;

public class Leechionnaire : MonoBehaviour, ISpell {
	public float cooldown;

	public float Cast () {
		Transform transform = GetComponent<Transform> ();

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject enemy in enemies) {
			GameObject clone = Instantiate (Resources.Load ("Spells/Leech") as GameObject, transform.position, transform.rotation) as GameObject;
			clone.GetComponent<Leech> ().SetTarget (enemy);
		}

		return cooldown;
	}
}
