using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int damage = 10;
    float timer;
    bool playerInRange;
    public bool tomExists = false;
    GameObject thisObject;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    GroundPlayerController gpc;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update ()
    {
        timer += Time.deltaTime;

        if (playerInRange && enemyHealth.currentHealth > 0 && timer > timeBetweenAttacks)
        {
            Attack();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Debug.Log("Player In Range.");
            playerInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Debug.Log("Player Leaves Range.");
            playerInRange = false;
        }
    }

    private void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0 && !playerHealth.damaged)  // if player is alive
        {
            playerHealth.TakeDamage(damage);
            gpc.Knockback(this.gameObject);
        }
        
    }

    public void TomExists()    // Tom will sometimes be in the rocket, this should trigger when he leaves rocket
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        gpc = player.GetComponent<GroundPlayerController>();
        tomExists = true;
    }
}
