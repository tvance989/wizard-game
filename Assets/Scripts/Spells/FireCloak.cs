using UnityEngine;
using System.Collections;

public class FireCloak : Spell {
	public float dps;
	public float duration;

	private float ttl;
	private Transform transform;
	private Transform player;

	void Start () {
		transform = GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

		transform.parent = player.transform;
	}
	
	public override void Cast () {
		ttl = Time.time + duration;
	}

	void Update() {
		if (Time.time >= ttl) {
			Destroy (gameObject);
		}
	}
	
	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.gameObject.SendMessage ("Damage", dps * Time.deltaTime);
		}
	}
}
