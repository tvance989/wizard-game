using UnityEngine;
using System.Collections;

public class Bomb: Spell {
	public float fuse, blastRadius, damage;
	public GameObject explosion;

	float ttl;

	public override void Cast () {
		ttl = Time.time + fuse;
	}

	void Update() {
		if (Time.time >= ttl)
			Explode ();
	}

	void Explode () {
		Instantiate (explosion, GetComponent<Transform> ().position, Quaternion.identity);

		Destroy (gameObject);
	}
}
