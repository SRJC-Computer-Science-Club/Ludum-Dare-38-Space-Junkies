using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public float monsterSpeed = 1f;
    public float timeBetweenMovements = 0.5f;
    float time;
    Transform player;

    EnemyAttack enemyAttack;

    private void Awake()
    {
        enemyAttack = GetComponent<EnemyAttack>();
    }

    void Update ()
    {
        time += Time.deltaTime;
        if (enemyAttack.tomExists && time > timeBetweenMovements)
        {
            MoveTowardsPlayer();
        }

        MoveLeft();
    }

    void MoveLeft()
    {
        transform.Translate(-Vector2.right * monsterSpeed * Time.deltaTime);
    }

    void MoveRight()
    {
        transform.Translate(Vector2.right * monsterSpeed * Time.deltaTime);
    }

    private void MoveTowardsPlayer()
    {
        time = 0f;
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        // shoot out rays to the left and right of player, whichever leaves planet first is the direction to go
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ship")
        {
            Physics2D.IgnoreLayerCollision(9,13,true);
        }
    }
}
