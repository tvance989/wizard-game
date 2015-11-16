using UnityEngine;
using System.Collections;

public class Poison: Spell {
	public float splashRadius, dps, duration;

	Rigidbody2D rb;
	ParticleSystem ps;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
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
		ps = GetComponent<ParticleSystem> ();
		ps.Simulate (0);
		ps.Play ();

		GetComponent<Renderer> ().enabled = false;

		foreach (Collider2D other in Physics2D.OverlapCircleAll (new Vector2 (rb.position.x, rb.position.y), splashRadius))
			if (other.tag == "Enemy")
				other.gameObject.AddComponent<DoT> ().Initialize (dps, duration);
	}

	void Update () {
		if (ps && !ps.IsAlive ())
			Destroy (gameObject);
	}
}
