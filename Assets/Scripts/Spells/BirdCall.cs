using UnityEngine;
using System.Collections;

public class BirdCall : Spell {
	public float capacity, hps, a, b, speed;

	private Transform transform, player;
	private PlayerController pc;
	private float currentLife;
	private ParticleSystem particles;

	void Awake () {
		transform = GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		pc = player.GetComponent<PlayerController> ();
		particles = GetComponent<ParticleSystem> ();

		currentLife = capacity;
	}

	public override void Cast () {
		transform.position = new Vector3 (2f, 0f, 0f);
		transform.rotation = Quaternion.identity;
	}
	
	void Update () {
		float x = a * Mathf.Cos (speed * Time.time);
		float y = b * Mathf.Sin (speed * Time.time);

		x += player.position.x;
		y += player.position.y + 1.5f;

		transform.position = new Vector3 (x, y, 0f);

		if (currentLife > 0) {
			float health = hps * Time.deltaTime;
			float balance = pc.Heal (health);

			currentLife -= health - balance;

			if (balance == 0)
				particles.enableEmission = true;
			else
				particles.enableEmission = false;
		} else {
			Destroy (gameObject);
		}
	}
}
