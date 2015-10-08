using UnityEngine;
using System.Collections;

public class FireCloak : MonoBehaviour, ISpell {
	public float cooldown;
	public float damage;
	public float duration;

	private float timer;
	private Transform transform;
	private Transform player;

	void Start () {
		transform = GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}
	
	public float Cast () {
		timer = Time.time + duration;
		return cooldown;
	}

	void Update() {
		transform.position = player.position;

		if (Time.time >= timer) {
			Destroy (gameObject);
		}
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.SendMessage ("Damage", damage);
		}
	}
}
