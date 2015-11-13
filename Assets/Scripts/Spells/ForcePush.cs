using UnityEngine;
using System.Collections;

public class ForcePush : Spell {
	public float force, duration;

	float ttl;
	new Transform transform;
	Transform player;

	void Start () {
		transform = GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

		transform.Rotate (new Vector3 (0, 0, 78));
		transform.SetParent (player);
	}
	
	public override void Cast () {
		ttl = Time.time + duration;
	}
	
	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Enemy")
			other.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (player.up.x, player.up.y) * force);
	}
	
	void Update() {
		if (Time.time >= ttl)
			Destroy (gameObject);
	}
}
