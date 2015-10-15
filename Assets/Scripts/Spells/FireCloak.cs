using UnityEngine;
using System.Collections;

public class FireCloak : Spell {
	public float dps, duration, fireballMultiplier;

	private float ttl;
	private Transform transform, player;

	void Start () {
		transform = GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

		transform.parent = player.transform;
	}
	
	public override void Cast () {
		Fireball.multiplier = 2f;
		ttl = Time.time + duration;
	}

	void Update() {
		if (Time.time >= ttl) {
			Fireball.multiplier = 1f;
			Destroy (gameObject);
		}
	}
	
	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.gameObject.SendMessage ("Damage", dps * Time.deltaTime);
		}
	}
}
