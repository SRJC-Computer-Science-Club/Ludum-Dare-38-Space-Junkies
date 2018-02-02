using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 10;
    public int currentHealth;
    public GameObject colliderShot;

    private bool damaged;
    private bool isDead = false;
    private int shotDamage;

    void Start ()
    {
        currentHealth = startingHealth;
	}
	
	void Update ()
    {
		if (damaged == true)
        {
            damaged = false;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "playerShot")
        {
            Destroy(other.gameObject);
            TakeDamage(other.GetComponent<Projectile>().getBulletDamage());
            Instantiate(colliderShot, other.transform.position, other.transform.rotation);
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }


}
