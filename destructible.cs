using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructible : MonoBehaviour {
    
    public float health = 50f;
    public GameObject destroyedVersion;
    private float bulletDamage;

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (GameObject.Find("Weapon").GetComponent<ProjectileShooter>() != null)
        {
            bulletDamage = GameObject.Find("Weapon").GetComponent<ProjectileShooter>().BulletDamage;
        }

        if (collisionInfo.collider.tag == "Bullet")
            {
            health -= bulletDamage;
            if (health <= 0f)
            {
                Die();
            }
        }
    }
    public void Die ()
    {
        Destroy(gameObject);
        Instantiate(destroyedVersion, transform.position, transform.rotation);
    }

}