using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public int projectileSpeed = 10;
    private Rigidbody2D shot;

	void Start ()
    {
        shot = this.GetComponent<Rigidbody2D>();
        shot.AddRelativeForce(Vector2.up * projectileSpeed);
    }
}
