using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	public float radius, damage, force;

	new Transform transform;
	ParticleSystem particles;
	bool started;

	void Awake () {
		transform = GetComponent<Transform> ();
		particles = GetComponent<ParticleSystem> ();
	}

	public void Explode (float radius, float damage, float force) {
		particles.startLifetime = radius / 10f;

		particles.Simulate (0f);
		particles.Play ();

		started = true;

//		Collider2D[] colliders = Physics2D.OverlapCircleAll (new Vector2 (transform.position.x, transform.position.y), radius);

		//.will be deprecated when OnParticleCollision works
		foreach (Collider2D other in Physics2D.OverlapCircleAll (new Vector2 (transform.position.x, transform.position.y), radius)) {
			if (other.tag == "Enemy") {
				other.SendMessage ("Damage", damage);

				Rigidbody2D rb = other.GetComponent<Rigidbody2D> ();
				if (rb) {
					Vector3 direction = other.GetComponent<Transform> ().position - transform.position;
					direction = direction.normalized;
					rb.AddForce (new Vector2 (direction.x, direction.y) * force, ForceMode2D.Impulse);
				}
			}
		}
	}

	void OnParticleCollision (GameObject other) {
		//.look for 2d collision method in 5.3 (dec 8)
	}
	
	void Update () {
		if (started && !particles.isPlaying)
			Destroy (gameObject);
	}
}
