using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rwingHealth : MonoBehaviour {
	
	// Use this for initialization
	public int rwingstartingHealth = 100;
	private int rwingcurrentHealth;
	public Slider rwingSlider;
	bool isDead;
	bool isDamaged;
	private Rigidbody2D rigi;
	private int kkona;


		void Awake()
		{
		rwingcurrentHealth = rwingstartingHealth;
		rigi = GetComponent<Rigidbody2D> ();
		gaykona = rigi.velocity.x;
		}

		// Update is called once per frame
		void Update()
		{
		
		}
		public void TakeDamage(int amount)
		{
			isDamaged = true;

			rwingcurrentHealth -= amount;
			rwingSlider.value = rwingcurrentHealth;
			if (rwingcurrentHealth <= 0 && !isDead)
			{
				Death();
			}
		}
		void Death()
	{
		isDead = true;
		GameObject thePlayer = GameObject.Find ("DaMainShip");
		ModularShipControl playerScript = thePlayer.GetComponent<ModularShipControl> ();
		playerScript.rotationSpeed -= 49;
	}
	void OnCollisionEnter(Collision collision) {
		TakeDamage (kkona);
	}
		
		
}
