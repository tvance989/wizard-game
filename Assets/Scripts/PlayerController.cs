using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float speedMultiplier = 1f;
	public bool isFrozen, isInvincible;
	
	Rigidbody2D rb;
	Health health;
	
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		rb.centerOfMass = new Vector2 (0, 0);

		health = GetComponent<Health> ();
	}

	void FixedUpdate () {
		Rotate ();
		Move ();
	}

	void Rotate () {
		if (isFrozen) //.eventually offload this onto the attribute system
			return;

		Vector2 mousePos = Input.mousePosition;
		Vector2 objectPos = Camera.main.WorldToScreenPoint (rb.position);
		mousePos.x -= objectPos.x;
		mousePos.y -= objectPos.y;
		float playerRotationAngle = Mathf.Atan2 (mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;

		rb.MoveRotation (playerRotationAngle);
	}

	void Move () {
		if (isFrozen)
			return;

		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector3 (x, y);
		movement *= speed * speedMultiplier * Time.fixedDeltaTime;

		rb.MovePosition (rb.position + movement);
	}
	
	public float Heal (float x) {
		return health.Heal (x);
	}
}
