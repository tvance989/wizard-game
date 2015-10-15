using UnityEngine;
using System.Collections;

public class Fireball : Spell {
	public float damage, speed;

	public static float multiplier = 1f;
	
	public override void Cast () {
		GetComponent<Transform> ().localScale *= multiplier;
		GetComponent<Rigidbody2D> ().velocity = GetComponent<Transform> ().up * speed;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.SendMessage ("Damage", damage * multiplier);
			Destroy (gameObject);
		}
	}
}
