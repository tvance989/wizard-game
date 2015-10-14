using UnityEngine;
using System.Collections;

public class Boulder : Spell {
	public float damage;
	public float speed;

	private Vector2 velocity;

	void Start () {
		velocity = GetComponent<Rigidbody2D> ().velocity;
	}
	
	public override float Cast () {
		GetComponent<Rigidbody2D> ().velocity = GetComponent<Transform> ().up * speed;
		return cooldown;
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.SendMessage ("Damage", damage);
			if (velocity.magnitude > speed / 2f) {
				velocity -= Vector2.one * 0.2f; // slows the boulder with each collision
			}
		}
	}
}
