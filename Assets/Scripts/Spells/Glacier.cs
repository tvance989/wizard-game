using UnityEngine;
using System.Collections;

public class Glacier : Spell {
	public float dps, speed;
	
	public override void Cast () {
		Transform transform = GetComponent<Transform> ();
		transform.position = new Vector3 (Camera.main.orthographicSize * Screen.width / Screen.height + 5f, 0f, 0f);
		transform.eulerAngles = new Vector3 (0f, 0f, 90f);

		GetComponent<Rigidbody2D> ().velocity = transform.up * speed;
	}
	
	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.SendMessage ("Damage", dps * Time.deltaTime);
		}
	}
}
