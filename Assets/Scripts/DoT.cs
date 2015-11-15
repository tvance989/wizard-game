using UnityEngine;
using System.Collections;

public class DoT : MonoBehaviour {
	public GameObject target;
	public float dps, duration;
	public bool started;

	float ttl;

	public static void DoDoT (GameObject target, float dps, float duration) {
		DoT dot = target.AddComponent<DoT> ();
		dot.target = target;
		dot.dps = dps;
		dot.duration = duration;
		dot.started = true;
	}

	void Start () {
		ttl = Time.time + duration;
	}
	
	void Update () {
		if (!started)
			return;
		if (Time.time >= ttl)
			Destroy (this);

		target.SendMessage ("Damage", dps * Time.deltaTime);
	}
}
