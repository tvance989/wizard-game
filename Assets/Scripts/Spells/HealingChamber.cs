using UnityEngine;
using System.Collections;

public class HealingChamber: Spell {
	public float hps, duration;
	public GameObject explosion;

	float ttl;
	PlayerController player;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();

		GetComponent<Transform> ().position = player.GetComponent<Transform> ().position;
	}
	
	public override void Cast () {
		player.isFrozen = player.isInvincible = true;
		player.GetComponent<Collider2D> ().enabled = false;

		ttl = Time.time + duration;
	}

	void Update() {
		if (Time.time >= ttl) {
			player.isFrozen = player.isInvincible = false;
			player.GetComponent<Collider2D> ().enabled = true;

			Explode ();
		}

		player.gameObject.SendMessage ("Heal", hps * Time.deltaTime);
	}

	void Explode () {
		Instantiate (explosion, GetComponent<Transform> ().position, Quaternion.identity);

		Destroy (gameObject);
	}
}
