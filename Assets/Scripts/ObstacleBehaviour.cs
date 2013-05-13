﻿using UnityEngine;
using System.Collections;

class ObstacleBehaviour : MonoBehaviour, IObstacleBehaviour
{
    /// <summary>
    /// The domain-specific obstacle instance.
    /// </summary>
    private Obstacle obstacle;

    /// <summary>
    /// Used for initialization by Unity. The Start method is called just
    /// before any of the Update methods is called the first time.
    /// </summary>
    void Awake()
    {
        this.obstacle = new Obstacle(this);
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    /// </summary>
    /// <param name="collision">Collision information</param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ShoppingCart")
        {
			this.obstacle.Collision();
        }
    }
}
