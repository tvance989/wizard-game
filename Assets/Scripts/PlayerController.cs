using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float speedMultiplier = 1f;
	public bool isFrozen, isInvincible;
	public float maxHealth;
	public Image healthBar;

	private new Transform transform;
	private float currentHealth;
	private Image currentHealthBar;

	void Start () {
		transform = GetComponent<Transform> ();
		
		currentHealth = maxHealth;
		CreateHealthBar ();
	}

	void Update () {
		Rotate ();
		Move ();
	}
	
	void CreateHealthBar () {
		healthBar = Instantiate (healthBar) as Image;
		healthBar.transform.SetParent (GameObject.FindGameObjectWithTag ("Canvas").GetComponent<Transform> ());
		currentHealthBar = healthBar.GetComponentsInChildren<Image> () [1];
	}
	
	void Rotate () {
		if (isFrozen)
			return;

		Vector3 mousePos = Input.mousePosition;
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x -= objectPos.x;
		mousePos.y -= objectPos.y;
		float playerRotationAngle = Mathf.Atan2 (mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;
		
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, playerRotationAngle));
	}

	void Move () {
		if (isFrozen)
			return;

		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (x, y, 0);
		movement *= speed * speedMultiplier * Time.deltaTime;

		transform.Translate (movement, Space.World);

		healthBar.GetComponent<Transform> ().position = Camera.main.WorldToScreenPoint (transform.position) + Vector3.up * 30;
	}

	public float Heal (float health) {
		float balance = 0f;

		currentHealth += health;

		if (currentHealth > maxHealth) {
			balance = currentHealth - maxHealth;
			currentHealth = maxHealth;
		}
		
		currentHealthBar.fillAmount = currentHealth / maxHealth;

		return balance;
	}
	
	public void Damage (float damage) {
		if (isInvincible)
			return;

		currentHealth -= damage;
		
		if (currentHealth <= 0)
			Die ();
		
		currentHealthBar.fillAmount = currentHealth / maxHealth;
	}
	
	void Die () {
		Destroy (healthBar);
		Destroy (gameObject);
	}
}
