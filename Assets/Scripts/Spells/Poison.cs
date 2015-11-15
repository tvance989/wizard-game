using UnityEngine;
using System.Collections;

public class Poison: Spell {
	public float splashRadius, dps, duration;

	Rigidbody2D rb;
	ParticleSystem particles;
	bool started;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		particles = GetComponent<ParticleSystem> ();
	}

	public override void Cast () {
		Throw (Camera.main.ScreenToWorldPoint (Input.mousePosition));
	}

	void Throw (Vector3 target) {
		Vector2 dir = (target - GetComponent<Transform> ().position);
		rb.AddForce (dir, ForceMode2D.Impulse);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy")
			Splash ();
	}

	void Splash () {
		particles.Simulate (0);
		particles.Play ();
		started = true;
		GetComponent<Renderer> ().enabled = false;

		foreach (Collider2D other in Physics2D.OverlapCircleAll (new Vector2 (rb.position.x, rb.position.y), splashRadius)) {
			if (other.tag == "Enemy")
				DoT.DoDoT (other.gameObject, dps, duration);
		}
	}

	void Update () {
		if (started && !particles.isPlaying)
			Destroy (gameObject);
	}
}
