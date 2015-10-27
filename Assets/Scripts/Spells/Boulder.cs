using UnityEngine;
using System.Collections;

public class Boulder : Spell {
	public float damage, speed, spawnDistance;

	private new Transform transform;
	private new Rigidbody2D rigidbody;

	void Awake () {
		transform = GetComponent<Transform> ();
		rigidbody = GetComponent<Rigidbody2D> ();

		// start at mouse click
		transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);

		// randomly rotate and move back (off screen)
		transform.Rotate (0f, 0f, Random.Range(0f,360f));
		transform.Translate (Vector3.down * spawnDistance);
	}
	
	public override void Cast () {
		rigidbody.velocity = transform.up * speed; // roll forward (toward mouse click)
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.SendMessage ("Damage", damage);

			// slow boulder with each collision
			if (rigidbody.velocity.magnitude > speed / 2f)
				rigidbody.velocity *= 0.8f;
		}
	}
}
