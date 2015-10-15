using UnityEngine;
using System.Collections;

public class Fireball : Spell {
	public float damage;
	public float speed;
	
	public override void Cast () {
		GetComponent<Rigidbody2D> ().velocity = GetComponent<Transform> ().up * speed;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.SendMessage ("Damage", damage);
			Destroy (gameObject);
		}
	}
}
