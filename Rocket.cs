using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float force = 700f;
    public float radius = 5f;

    public GameObject explosionEffect;
    private ContactPoint Collision_point;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        Collision_point = other.contacts[0];
        Explode();
    }

    void Explode()
    {
        Debug.Log("Pow");

        Instantiate(explosionEffect, transform.position, Quaternion.LookRotation(Collision_point.normal));

        Destroy(gameObject);

        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearObject in collidersToDestroy)
        {
            Robot robot = nearObject.GetComponent<Robot>();
            MicroDestructible md = nearObject.GetComponent<MicroDestructible>();
            destructible dest = nearObject.GetComponent<destructible>();
            WallHider wh = nearObject.GetComponent<WallHider>();

            if (wh != null)
            {
                wh.deletethis();
            }

            if (robot != null)
            {
                robot.IsDead();
            }

            if (dest != null)
            {
                dest.Die();
            }

            if (md != null)
            {
                md.MicroDest();
            }
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

        }
    }
}
