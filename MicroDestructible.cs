using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroDestructible : MonoBehaviour
{
    public float RigidbodyMass;
    private float force = 0f;
    private float radius = 5f;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "HighCalliberBullet")
        {
            MicroDest();
        }
    }

    public void MicroDest ()
    {
        gameObject.AddComponent<Rigidbody>();
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.mass = RigidbodyMass;
    }
}