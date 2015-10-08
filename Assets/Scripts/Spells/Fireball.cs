using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour, ISpell {
	public float cooldown;
	public float damage;
	public float speed;
	
	public float Cast () {
		GetComponent<Rigidbody2D> ().velocity = GetComponent<Transform> ().up * speed;
		return cooldown;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.SendMessage ("Damage", damage);
			Destroy (gameObject);
		}
	}
}
