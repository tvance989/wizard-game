using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public interface ISpell {
	float Cast (); // returns the spell's cooldown
}

public class SpellController : MonoBehaviour {
	public Text activeSpellText;
	public Image[] spellCooldowns;
	public GameController gameController;

	private GameObject[] spells;
	private Transform spellSpawn;
	private float[] nextCast;
	private float[] cooldownTimers;
	private int activeSpell;

	void Start () {
		spells = new GameObject[7];
		nextCast = new float[7];
		cooldownTimers = new float[7];

		spellSpawn = GetComponent<Transform> ();

		SwitchSpells (0);
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

		if (Input.GetButton ("Fire1")) {
			if (Time.time >= nextCast [activeSpell]) {
				Cast (activeSpell);
			}
		}

		UpdateSpellCooldowns ();
	}
	
	void Cast (int index) {
		GameObject clone = Instantiate (spells [index], spellSpawn.position, spellSpawn.rotation) as GameObject;
		cooldownTimers [index] = clone.GetComponent<ISpell> ().Cast ();
		
		spellCooldowns [index].fillAmount = 0f;
		nextCast [index] = Time.time + cooldownTimers [index];
	}

	void SwitchSpells (int index) {
		if (spells [index] == null) {
			activeSpellText.text = "";
			return;
		}

		spellCooldowns [activeSpell].color = new Color (255, 255, 255);

		spellCooldowns [index].color = new Color (255, 255, 0);
		activeSpellText.text = "Active Spell: " + spells [index].GetComponent<ISpell> ().GetType ().ToString ();

		activeSpell = index;
	}

	void UpdateSpellCooldowns () {
		//.make this into an animation so it doesn't have to update every frame
		for (int i = 0; i < spellCooldowns.Length; i++) {
			spellCooldowns [i].enabled = false;
			if (spells.Length > i)
				if (spells [i] != null)
					spellCooldowns [i].enabled = true;

			if (spellCooldowns [i].fillAmount < 1)
				spellCooldowns [i].fillAmount += Time.deltaTime / cooldownTimers [i];
		}
	}

	public void UpdateAvailableSpells (Item[] items) {
		string[] names = gameController.GetAvailableSpells (items.Where (s => s != null).ToArray ());
		spells = new GameObject [names.Length];

		for (int i = 0; i < names.Length; i++) {
			try {
				spells [i] = Resources.Load (names [i]) as GameObject;
			} catch (System.Exception ex) {
				Debug.Log(ex);
			}
		}

		spells = spells.Where (s => s != null).ToArray ();

		UpdateSpellCooldowns ();
	}
}
