using UnityEngine;
using System.Collections;

public class Boulder : Spell {
	public float damage, speed, spawnDistance;

	private Transform transform;
	private Rigidbody2D rb;

	void Awake () {
		transform = GetComponent<Transform> ();
		rb = GetComponent<Rigidbody2D> ();

		// start at mouse click
		transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);

		// randomly rotate and move back (off screen)
		transform.Rotate (0f, 0f, Random.Range(0f,360f));
		transform.Translate (Vector3.down * spawnDistance);
	}
	
	public override void Cast () {
		rb.velocity = transform.up * speed; // roll forward (toward mouse click)
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.SendMessage ("Damage", damage);

			// slow boulder with each collision
			if (rb.velocity.magnitude > speed / 2f)
				rb.velocity *= 0.8f;
		}
	}
}
