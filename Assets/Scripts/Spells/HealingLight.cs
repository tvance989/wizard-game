using UnityEngine;
using System.Collections;

public class HealingLight : Spell {
	public float health, duration;

	private float ttl;

	void Start () {
		GetComponent<Transform> ().parent = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}
	
	public override void Cast () {
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().Heal (health);
		ttl = Time.time + duration;
	}

	void Update() {
		if (Time.time >= ttl) {
			Destroy (gameObject);
		}
	}
}
