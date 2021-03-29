using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructibleTarget : MonoBehaviour {
    
    public float health = 50f;
    public GameObject destroyedVersion;
    private float bulletDamage;
    private float pelletDamage;
    public float targetsRemaining = 4f;

    private void OnCollisionEnter(Collision collisionInfo)
    {

        if (collisionInfo.collider.tag == "Bullet")
        {
            bulletDamage = GameObject.Find("Weapon").GetComponent<ProjectileShooter>().BulletDamage;
            health -= bulletDamage;
            if (health <= 0f)
            {
                Die();
            }
        }
        if (collisionInfo.collider.tag == "Shotgun Pellet")
        {
            pelletDamage = GameObject.Find("Weapon").GetComponent<ShotgunProjectileShooter>().BulletDamage;
            health -= pelletDamage;
            if (health <= 0f)
            {
                Die();
            }
        }
    }
    void Die ()
    {
        Destroy(gameObject);
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        targetsRemaining -= 1;

    }

}