using UnityEngine;
using System.Collections;

public class Bomb: Spell {
	public float fuse, radius, damage, force;
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
		GameObject clone = Instantiate (explosion, GetComponent<Transform> ().position, Quaternion.identity) as GameObject;
		clone.GetComponent<Explosion> ().Explode (radius, damage, force);
		
		Destroy (gameObject);
	}
}
