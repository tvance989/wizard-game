using UnityEngine;
using System.Collections;

public class Geyser : Spell {
	public int number;

	private Transform transform;
	private ParticleSystem particles;
	private float xMin, xMax, yMin, yMax;

	void Awake () {
		transform = GetComponent<Transform> ();
		transform.eulerAngles = new Vector3 (-90f, 0f, 0f);
		particles = GetComponent<ParticleSystem> ();

		yMax = Camera.main.orthographicSize * 0.75f;
		yMin = -yMax;
		xMin = Camera.main.orthographicSize * Screen.width / Screen.height;
		xMax = -xMin;
	}
	
	public override void Cast () {
		StartCoroutine (Shoot ());
	}

	IEnumerator Shoot () {
		for (int i = 0; i < number; i++) {
			transform.position = new Vector3 (Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0f);
			particles.Simulate (0f, true, true);
			particles.Play ();
			yield return new WaitForSeconds (1.3f);
		}

		Destroy (this.gameObject);
	}
}
