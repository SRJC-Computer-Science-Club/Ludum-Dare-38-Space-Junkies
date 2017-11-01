using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

     public class bodyHealth : MonoBehaviour { 

    // Use this for initialization
	public float bodystartingHealth = 100f;
	private float bodycurrentHealth;
    public Slider bodySlider;
    bool isDead;
    bool isDamaged;

    void Awake()
    {
        bodycurrentHealth = bodystartingHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
	public void TakeDamage(float amount)
    {
        isDamaged = true;

        bodycurrentHealth -= amount;
        bodySlider.value = bodycurrentHealth;
        if (bodycurrentHealth <= 0 && !isDead)
        {
            Death();
        }
    }
    void Death()
    {
        isDead = true;
		Application.LoadLevel(4);
    }

}
