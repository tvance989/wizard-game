using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float speedMultiplier = 1f;
	public bool isFrozen, isInvincible;

	private new Transform transform;
	private Health health;
	
	void Awake () {
		transform = GetComponent<Transform> ();
		health = GetComponent<Health> ();
	}

	void Update () {
		Rotate ();
		Move ();
	}

	void Rotate () {
		if (isFrozen)
			return;

		Vector3 mousePos = Input.mousePosition;
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x -= objectPos.x;
		mousePos.y -= objectPos.y;
		float playerRotationAngle = Mathf.Atan2 (mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;
		
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, playerRotationAngle));
	}

	void Move () {
		if (isFrozen)
			return;

		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (x, y, 0);
		movement *= speed * speedMultiplier * Time.deltaTime;

		transform.Translate (movement, Space.World);
	}
	
	public float Heal (float x) {
		return health.Heal (x);
	}
}
