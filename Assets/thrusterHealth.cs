using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class thrusterHealth : MonoBehaviour { 

	// Use this for initialization
	public int thrusterstartingHealth = 100;
	private int thrustercurrentHealth;
	public Slider thrusterSlider;
	bool isDead;
	bool isDamaged;

	void Awake()
	{
		thrustercurrentHealth = thrusterstartingHealth;
	}

	// Update is called once per frame
	void Update()
	{

	}
	public void TakeDamage(int amount)
	{
		isDamaged = true;

		thrustercurrentHealth -= amount;
		thrusterSlider.value = thrustercurrentHealth;
		if (thrustercurrentHealth <= 0 && !isDead)
		{
			Death();
		}
	}
	void Death()
	{
		isDead = true;
		GameObject thePlayer = GameObject.Find ("DaMainShip");          //this disables forward and backward movement
		ModularShipControl reference = thePlayer.GetComponent<ModularShipControl> ();
		reference.thrustForce = 0;


	}

}
