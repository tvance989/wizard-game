using UnityEngine;
using System.Collections;

public class Magmud : Spell {
	public float dps, duration;
	
	private float ttl;
	
	void Start () {
		Transform transform = GetComponent<Transform> ();
		transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);
	}
	
	public override void Cast () {
		ttl = Time.time + duration;
	}
	
	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Enemy")
			other.gameObject.SendMessage ("Damage", dps * Time.deltaTime);
	}
	
	void Update() {
		if (Time.time >= ttl)
			Destroy (gameObject);
	}
}
