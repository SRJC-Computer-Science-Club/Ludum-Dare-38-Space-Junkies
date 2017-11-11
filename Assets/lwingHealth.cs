using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lwingHealth : MonoBehaviour {

	// Use this for initialization
        public int lwingstartingHealth = 100;
        private int lwingcurrentHealth;
        public Slider lwingSlider;
        bool isDead;
        bool isDamaged;
	    private bool isKeyEnabled;

			
    void Awake()
{
    lwingcurrentHealth = lwingstartingHealth;
}
	
	// Update is called once per frame
	void Update () {
		
	}
    
		void TakeDamage(int amount)
    {
        isDamaged = true;

        lwingcurrentHealth -= amount;
        lwingSlider.value = lwingcurrentHealth;
        if (lwingcurrentHealth <= 0 && !isDead)
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
}
