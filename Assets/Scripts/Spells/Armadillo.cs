using UnityEngine;
using System.Collections;

public class Armadillo : MonoBehaviour {
	public float damage, speed;

	public void Roll () {
		GetComponent<Rigidbody2D> ().velocity = Vector3.right * speed;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.SendMessage ("Damage", damage);
		}
	}
}
