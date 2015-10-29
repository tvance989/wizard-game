using UnityEngine;
using System.Collections;

public class GeyserColumn : MonoBehaviour {
	public float damage, force;

	private ParticleSystem particles;
	private new Collider2D collider;
	private bool started = false;

	void Awake () {
		particles = GetComponent<ParticleSystem> ();

		collider = GetComponent<Collider2D> ();
		collider.enabled = false;

		GetComponent<Transform> ().Rotate (new Vector3 (270f, 0f, 0f));
	}

	public void Charge (float wait) {
		StartCoroutine (Shoot (wait));
	}

	IEnumerator Shoot (float wait) {
		yield return new WaitForSeconds (wait);

		particles.Simulate (0f);
		particles.Play ();

		collider.enabled = true;
		started = true;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Enemy") {
			other.gameObject.SendMessage ("Damage", damage);
//			other.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (Random.Range (1f, 5f), force));//.enemies aren't slowing down
		}
	}
	
	void Update() {
		if (started && !particles.isPlaying)
			Destroy (gameObject);
	}
}
