using UnityEngine;
using System.Collections;
using Assets.Scripts;

class ObstacleBehaviour : MonoBehaviour
{
    /// <summary>
    /// The domain-specific obstacle instance.
    /// </summary>
    private Obstacle obstacle;

    // Use this for initialization
    void Start()
    {
        this.obstacle = new Obstacle(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        /*
        Below code should be forwarded to domain object:
        
        if (collision.gameObject.name == "Avatar")
        {
            StateManager.Instance.die();
        }
        */
    }
}
