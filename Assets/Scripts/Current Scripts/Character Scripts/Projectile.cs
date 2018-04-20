using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int projectileSpeed = 20;
    public float timerOne = 3.0f;
    public int bulletDamage = 3;
    public GameObject playerShip;

    private Rigidbody2D shot;

    EnemyHealth enemyHealth;

	void Start ()
    {
        playerShip = GameObject.FindGameObjectWithTag("Ship");
        shot = this.GetComponent<Rigidbody2D>();
        Debug.Log("Angle of Bullet: " + this.transform.rotation);
        shot.AddRelativeForce(Vector2.up * projectileSpeed + playerShip.GetComponent<Rigidbody2D>().velocity);
    }

    private void Update()
    {
        timerOne -= Time.deltaTime;
        if (timerOne <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreLayerCollision(9, 12, true);
    }

    public int getBulletDamage()
    {
        return bulletDamage;
    }
}
