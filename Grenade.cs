using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float force = 700f;
    public float radius = 5f;

    public GameObject explosionEffect;
    float countdown;
    bool hasExploded;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
        hasExploded = false;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && hasExploded == false)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Debug.Log("Boom");
        Instantiate(explosionEffect, transform.position, transform.rotation);

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
                Debug.Log("poo");
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

        Destroy(gameObject);
    }
}
