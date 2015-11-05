using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {
	public float maxHealth;
	public Image healthBar;
	public bool isInvincible;//.is there a better way to handle this? maybe a health status system (burning, invincible, poisoned, etc)

	private float curHealth;
	private new Transform transform;
	private Transform playerTransform;
	private Image curHealthBar;

	void Awake () {
		playerTransform = GetComponent<Transform> ();
		curHealth = maxHealth;
		CreateHealthBar ();

	}

	void CreateHealthBar () {
		healthBar = Instantiate (healthBar) as Image;
		transform = healthBar.GetComponent<Transform> ();
		transform.SetParent (GameObject.FindGameObjectWithTag ("Canvas").GetComponent<Transform> ());
		transform.Translate (new Vector3 (0, 3));
		curHealthBar = healthBar.GetComponentsInChildren<Image> () [1];
	}
	
	void Update () {
		//.set parent instead of updating every frame?
		transform.position = Camera.main.WorldToScreenPoint (playerTransform.position) + Vector3.up * 30;
	}

	public float Heal (float health) {
		float balance = 0f;
		
		curHealth += health;
		
		if (curHealth > maxHealth) {
			balance = curHealth - maxHealth;
			curHealth = maxHealth;
		}
		
		curHealthBar.fillAmount = curHealth / maxHealth;
		
		return balance;
	}
	
	public void Damage (float damage) {
		if (isInvincible)
			return;

		curHealth -= damage;
		
		if (curHealth <= 0)
			Kill ();

		curHealthBar.fillAmount = curHealth / maxHealth;
	}

	void Kill () {
		Destroy (healthBar);
		Destroy (gameObject);
	}
}
