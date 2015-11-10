using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {
	public float maximum, current;
	public Image healthBar;
	public bool isInvincible;//.is there a better way to handle this? maybe a health status system (burning, invincible, poisoned, etc)

	Image obj;
	Transform targetTransform, healthBarTransform;
	Image curHealthBar;

	void Start () {
		current = maximum;
		CreateHealthBar ();
	}

	void CreateHealthBar () {
		targetTransform = GetComponent<Transform> ();

		obj = Instantiate (healthBar) as Image;
		healthBarTransform = obj.GetComponent<Transform> ();

		healthBarTransform.SetParent (GameObject.FindGameObjectWithTag ("Canvas").GetComponent<Transform> ());
		curHealthBar = obj.GetComponentsInChildren<Image> () [1];
	}
	
	void Update () {
		//.set parent instead of updating every frame?
		healthBarTransform.position = Camera.main.WorldToScreenPoint (targetTransform.position) + Vector3.up * 25;
	}

	public float Heal (float health) {
		float balance = 0f;
		
		current += health;
		
		if (current > maximum) {
			balance = current - maximum;
			current = maximum;
		}
		
		curHealthBar.fillAmount = current / maximum;
		
		return balance;
	}
	
	public void Damage (float damage) {
		if (isInvincible)
			return;

		current -= damage;
		
		if (current <= 0)
			Kill ();

		curHealthBar.fillAmount = current / maximum;
	}

	void Kill () {
		Destroy (obj);
		Destroy (gameObject);
	}
}
