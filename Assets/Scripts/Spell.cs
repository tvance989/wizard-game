using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {
	public Sprite icon;
	public float cooldown;

	public virtual float Cast () {
//		if (!nextCast.ContainsKey (name))
//			nextCast.Add (name, 0f);
//
//		if (Input.GetButton ("Fire1") && Time.time >= nextCast [name])
//			gameObject.SendMessage ("Cast", null, SendMessageOptions.RequireReceiver);
//
//		nextCast [name] = Time.time + cooldown;
		Debug.Log ("Did you forget to override this method?");
		return cooldown;
	}
}
