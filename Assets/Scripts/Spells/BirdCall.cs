using UnityEngine;
using System.Collections;

public class BirdCall : Spell {
	public float capacity, hps;

	private Transform transform, player;
	private PlayerController pc;
	private float currentLife;

	void Awake () {
		transform = GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		pc = player.GetComponent<PlayerController> ();

		currentLife = capacity;
	}

	public override void Cast () {
		transform.position = new Vector3 (2f, 0f, 0f);
	}
	
	void Update () {
		transform.position = new Vector3 (player.position.x, player.position.y + 1.5f, 0f);

		if (currentLife > 0) {
			float health = hps * Time.deltaTime;
			float balance = pc.Heal (health);
			currentLife -= health - balance;
		} else {
			Destroy (gameObject);
		}
	}
}
