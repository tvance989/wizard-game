using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public float speed, maxHealth, dps, range;
	public float speedMultiplier = 1f;

	new Transform transform;
	Rigidbody2D rb;
	Rigidbody2D player;

	void Start () {
		transform = GetComponent<Transform> ();
		rb = GetComponent<Rigidbody2D> ();

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		if (player == null)
			return;

		Rotate ();
		Move ();

		Vector2 diff = player.position - rb.position;
		float dist = diff.sqrMagnitude;
		if (dist <= range)
			player.GetComponent<Health> ().Damage (dps * Time.fixedDeltaTime);
	}

	void Rotate () {
		Vector3 dir = player.position - rb.position;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
		rb.MoveRotation (angle);
	}

	void Move () {
		if (rb.velocity.magnitude < speed)
			rb.AddForce (new Vector2 (transform.up.x, transform.up.y) * speed * speedMultiplier);
	}
}
