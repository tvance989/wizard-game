using UnityEngine;
using System.Collections;

public class Geyser : Spell {
	public int number;
	public float minWait, maxWait;

	private float xMin, xMax, yMin, yMax;

	void Awake () {
		yMax = Camera.main.orthographicSize * 0.75f;
		yMin = -Camera.main.orthographicSize;
		xMin = Camera.main.orthographicSize * Screen.width / Screen.height;
		xMax = -xMin;
	}
	
	public override void Cast () {
		for (int i = 0; i < number; i++) {
			GameObject clone = Instantiate (
				Resources.Load ("Spells/GeyserColumn") as GameObject,
				new Vector3 (Random.Range (xMin, xMax), Random.Range (yMin, yMax), 0f),
				Quaternion.identity
			) as GameObject;

			clone.GetComponent<GeyserColumn> ().Charge (Random.Range (minWait, maxWait));
		}
		
		Destroy (this.gameObject);
	}
}
