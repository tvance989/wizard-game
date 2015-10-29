using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quicksand: Spell {
	public float speedMultiplier, dps, duration;
	
	private float ttl;
	private static List<EnemyController> enemies = new List<EnemyController> ();
	
	void Start () {
		Transform transform = GetComponent<Transform> ();
		transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
	}
	
	public override void Cast () {
		ttl = Time.time + duration;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			EnemyController enemy = other.GetComponent<EnemyController> ();

			enemy.speedMultiplier = speedMultiplier;
			enemies.Add (enemy);
		}
	}
	
	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Enemy")
			other.gameObject.SendMessage ("Damage", dps * Time.deltaTime);
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Enemy")
			other.GetComponent<EnemyController> ().speedMultiplier = 1f;
	}
	
	void Update() {
		if (Time.time >= ttl) {
			foreach (var enemy in enemies)
				enemy.speedMultiplier = 1f;

			Destroy (gameObject);
		}
	}
}
