using UnityEngine;
using System.Collections;

public class DoT : MonoBehaviour {
	float dps;

	public void Initialize (float dps, float duration) {
		this.dps = dps;

		InvokeRepeating ("Fire", 1, 1);

		Destroy (this, duration + 0.5f);
	}

	void Fire () {
		gameObject.SendMessage ("Damage", dps);
	}
}
