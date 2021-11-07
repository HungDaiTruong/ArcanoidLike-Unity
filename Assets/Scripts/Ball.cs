using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float speed = 20f;
    Rigidbody rigidBody;
    Vector3 velocity;
    Renderer render;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();
        Invoke("Launch", 0.5f);
    }

    void Launch()
    {
        rigidBody.velocity = Vector3.up * speed;
    }

    // Update is called 100 times/seconds
    void FixedUpdate()
    {
        // Normalize the speed, ergo shrink it down to 1 and then multiply it by speed, making it equal to our speed value permanetly
        rigidBody.velocity = rigidBody.velocity.normalized * speed;
        velocity = rigidBody.velocity; // Stores velocity value

        // When the rendered ball is no longer visible, destroy it and deduct one ball from the Game Manager
        if (!render.isVisible)
        {
            GameManager.Instance.Balls--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // On collision, velocity is reflected according to the previous velocity and the normal of the first contact point in the array
        rigidBody.velocity = Vector3.Reflect(velocity, collision.contacts[0].normal);
    }
}
