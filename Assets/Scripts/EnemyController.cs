using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public float speed;
	public float maxHealth;
	public Image healthBar;

	private Transform transform;
	private Transform player;
	private float currentHealth;
	private Image currentHealthBar;

	void Start () {
		transform = GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();

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
		Vector3 dir = player.position - transform.position;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}

	void Move () {
		transform.Translate (Vector3.up * speed * Time.deltaTime);
		// health bar follows the enemy
		healthBar.GetComponent<Transform> ().position = Camera.main.WorldToScreenPoint (transform.position) + Vector3.up * 20;
	}

	public void Damage (float damage) {
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
