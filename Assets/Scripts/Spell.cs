using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {
	public Sprite icon;
	public float cooldown;

	public virtual void Cast () {
		Debug.Log ("Did you forget to override this method?");
	}
}
