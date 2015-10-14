using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpellController : MonoBehaviour {
	public Text activeSpellText;
	public Image[] spellCooldowns;
	public GameController gameController;

	private Spell[] spells = new Spell[7];
	private Transform spellSpawn;
	private static Dictionary<string,float> nextCasts = new Dictionary<string,float> ();
	private int activeSpell;

	void Start () {
		spellSpawn = GetComponent<Transform> ();
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1))
			SwitchSpells (0);
		else if (Input.GetKeyDown (KeyCode.Alpha2))
			SwitchSpells (1);
		else if (Input.GetKeyDown (KeyCode.Alpha3))
			SwitchSpells (2);
		else if (Input.GetKeyDown (KeyCode.Alpha4))
			SwitchSpells (3);
		else if (Input.GetKeyDown (KeyCode.Alpha5))
			SwitchSpells (4);
		else if (Input.GetKeyDown (KeyCode.Alpha6))
			SwitchSpells (5);
		else if (Input.GetKeyDown (KeyCode.Alpha7))
			SwitchSpells (6);

		if (Input.GetButton ("Fire1"))
			if (Time.time > nextCasts [spells [activeSpell].GetType ().ToString ()])
				Cast (activeSpell);

		UpdateSpellCooldowns ();
	}
	
	void Cast (int index) {
		if (spells [index] == null)
			return;

		GameObject clone = Instantiate (spells [index].gameObject, spellSpawn.position, spellSpawn.rotation) as GameObject;
		Spell spellScript = clone.GetComponent<Spell> ();
		spellScript.Cast ();

		nextCasts [spellScript.GetType ().ToString ()] = Time.time + spellScript.cooldown;
	}

	void SwitchSpells (int index) {
		if (spells.Length <= index)
			return;

		spellCooldowns [activeSpell].transform.parent.GetComponent<Image> ().color = new Color (255, 255, 255, 0.5f);

		activeSpell = index;
		spellCooldowns [activeSpell].transform.parent.GetComponent<Image> ().color = new Color (255, 255, 0, 1f);

		activeSpellText.text = "Active Spell: " + spells [activeSpell].GetComponent<Spell> ().GetType ().ToString ();
	}

	void UpdateSpellCooldowns () {
		//.make this into an animation so it doesn't have to update every frame
		for (int i = 0; i < spellCooldowns.Length; i++) {
			spellCooldowns [i].enabled = false;
			if (spells.Length > i)
				if (spells [i] != null)
					spellCooldowns [i].enabled = true;

			if (spellCooldowns [i].enabled)
				spellCooldowns [i].fillAmount = 1 - ((nextCasts [spells [i].GetType ().ToString ()] - Time.time) / spells [i].cooldown);
		}
	}

	public void UpdateAvailableSpells (Item[] items) {
		string[] newSpells = gameController.GetSpellsFromItems (items.Where (i => i != null).ToArray ());
		
		spells = new Spell[newSpells.Length];

		for (int i = 0; i < newSpells.Length; i++) {
			try {
				GameObject temp = Resources.Load ("Spells/" + newSpells [i]) as GameObject;
				spells [i] = temp.GetComponent<Spell> ();
			} catch {
				Debug.Log("Error: Could not load spell " + newSpells [i]);
			}
		}

		spells = spells.Where (i => i != null).ToArray ();

		for (int i = 0; i < spells.Length; i++) {
			spellCooldowns [i].GetComponent<Image> ().sprite = spells [i].icon;

			if (!nextCasts.ContainsKey (spells [i].name))
				nextCasts.Add (spells [i].name, 0f);
		}

		UpdateSpellCooldowns ();
	}
}
