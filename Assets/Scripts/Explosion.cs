using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	public float damage;

	private ParticleSystem particles;

	void Start () {
		particles = GetComponent<ParticleSystem> ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy")
			other.gameObject.SendMessage ("Damage", damage);
	}
	
	void Update () {
		if (!particles.isPlaying)
			Destroy (gameObject);
	}
}
