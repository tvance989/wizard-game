using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;

	private Transform transform;

	void Start () {
		transform = GetComponent<Transform> ();
	}

	void Update () {
		Rotate ();
		Move ();
	}
	
	void Rotate () {
		Vector3 mousePos = Input.mousePosition;
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x -= objectPos.x;
		mousePos.y -= objectPos.y;
		float playerRotationAngle = Mathf.Atan2 (mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;
		
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, playerRotationAngle));
	}

	void Move () {
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (x, y, 0);
		movement *= speed * Time.deltaTime;

		transform.Translate (movement, Space.World);
	}
}
