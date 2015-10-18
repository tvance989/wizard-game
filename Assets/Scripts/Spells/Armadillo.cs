using UnityEngine;
using System.Collections;

public class Armadillo : MonoBehaviour {
	public float damage, avgSpeed;

	void Awake () {
		GetComponent<Transform> ().eulerAngles = new Vector3 (0f, 0f, Random.Range (0f, 360f));
	}

	public void Roll () {
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();

		float speed = avgSpeed * Random.Range (0.8f, 1.2f);

		rb.angularVelocity = -speed * 360 / (2 * Mathf.PI); // convert linear velocity to angular
		rb.velocity = Vector3.right * speed;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.SendMessage ("Damage", damage);
		}
	}
}
