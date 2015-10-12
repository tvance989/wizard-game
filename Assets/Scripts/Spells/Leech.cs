﻿using UnityEngine;
using System.Collections;

public class Leech : MonoBehaviour {
	public float speed, capacity, dps, hps;

	private float currentLife;
	private Transform transform, target, player;
	private bool targetReached, capacityReached, playerReached;

	void Awake () {
		transform = GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

		targetReached = capacityReached = playerReached = false;
	}

	public void SetTarget (GameObject t) {
		target = t.GetComponent<Transform> ();
		StartCoroutine (MoveToTarget ());
	}

	IEnumerator MoveToTarget () {
		while (!targetReached) {
			Rotate (target);
			Move ();

			if (Vector3.Distance (transform.position, target.position) <= 0.1f)
				targetReached = true;

			yield return null;
		}

		StartCoroutine (LeechLife ());
	}

	IEnumerator LeechLife () {
		while (!capacityReached) {
			float damage = dps * Time.deltaTime;

			try {
				target.GetComponent<EnemyController> ().Damage (damage);
			} catch {
				break;
			}

			currentLife += damage;

			if (currentLife >= capacity) {
				currentLife = capacity;
				capacityReached = true;
			}

			yield return null;
		}

		StartCoroutine (MoveToPlayer ());
	}
	
	IEnumerator MoveToPlayer () {
		while (!playerReached) {
			Rotate (player);
			Move ();
			
			if (Vector3.Distance (transform.position, player.position) <= 0.1f)
				playerReached = true;
			
			yield return null;
		}
		
		StartCoroutine (HealPlayer ());
	}
	
	IEnumerator HealPlayer () {
		while (currentLife > 0) {
			float health = hps * Time.deltaTime;
			float balance = player.GetComponent<PlayerController> ().Heal (health);
			currentLife -= balance;
			yield return null;
		}
		
		Destroy (gameObject);
	}

	void Update () {
		if (targetReached && !capacityReached) {
			try {
				transform.rotation = target.rotation;
				transform.position = target.position;
			} catch {
				StartCoroutine (MoveToPlayer ());
			}
		} else if (playerReached) {
			transform.rotation = player.rotation;
			transform.position = player.position;
		}
	}

	void Rotate (Transform t) {
		Vector3 dir = t.position - transform.position;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}
	
	void Move () {
		transform.Translate (Vector3.right * speed * Time.deltaTime);
	}
}