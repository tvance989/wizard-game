using UnityEngine;
using System.Collections;

public class Wave : Spell {
	public float damage, speed;

	new Transform transform;

	void Start () {
		transform = GetComponent<Transform> ();
	}
	
	public override void Cast () {
		GetComponent<Rigidbody2D> ().velocity = GetComponent<Transform> ().up * speed;
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.SendMessage ("Damage", damage);
			other.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (transform.up.x, transform.up.y) * speed, ForceMode2D.Impulse);
		}
	}
}
