using UnityEngine;
using System.Collections;

public class DoT : MonoBehaviour {
	float dps, duration, ttl;

	public void Initialize (float dps, float duration) {
		this.dps = dps;
		this.duration = duration;

		ttl = Time.time + this.duration;
		
		StartCoroutine (Initiate ());
	}

	IEnumerator Initiate () {
		while (Time.time < ttl) {
			gameObject.SendMessage ("Damage", dps * Time.deltaTime);
			yield return null;
		}

		Destroy (this);
	}
}
