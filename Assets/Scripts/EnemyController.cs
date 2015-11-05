using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public float speed, maxHealth, dps, range;
	public float speedMultiplier = 1f;

	private new Transform transform;
	private Transform player;

	void Start () {
		transform = GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}

	void Update () {
		if (player == null)
			return;

		Rotate ();
		Move ();

		Vector3 diff = player.position - transform.position;
		float dist = diff.sqrMagnitude;
		if (dist <= range)
			player.GetComponent<Health> ().Damage (dps * Time.deltaTime);
	}

	void Rotate () {
		Vector3 dir = player.position - transform.position;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}

	void Move () {
		transform.Translate (Vector3.up * speed * speedMultiplier * Time.deltaTime);
	}
}
