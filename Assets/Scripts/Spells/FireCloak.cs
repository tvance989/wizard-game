using UnityEngine;
using System.Collections;

public class FireCloak : MonoBehaviour, ISpell {
	public float cooldown;
	public float dps;
	public float duration;

	private float timer;
	private Transform transform;
	private Transform player;

	void Start () {
		transform = GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

		transform.parent = player.transform;
	}
	
	public float Cast () {
		timer = Time.time + duration;
		return cooldown;
	}

	void Update() {
		if (Time.time >= timer) {
			Destroy (gameObject);
		}
	}
	
	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.gameObject.SendMessage ("Damage", dps * Time.deltaTime);
		}
	}
}
