using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoBox : MonoBehaviour
{
    Transform target;
    public float pickUpRadius = 2f;
    public float ammoRestored = 60f;
    public float maxReserveAmmo = 120f;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= pickUpRadius && GameObject.Find("Weapon").GetComponent<ProjectileShooter>().reserveAmmo <= 60)
        {
            GameObject.Find("Weapon").GetComponent<ProjectileShooter>().reserveAmmo += ammoRestored;
            Destroy(this.gameObject);
            Debug.Log("poo");
        }

        if (distance <= pickUpRadius && GameObject.Find("Weapon").GetComponent<ProjectileShooter>().reserveAmmo > 60f && GameObject.Find("Weapon").GetComponent<ProjectileShooter>().reserveAmmo < 120)
        {
            GameObject.Find("Weapon").GetComponent<ProjectileShooter>().reserveAmmo += maxReserveAmmo - GameObject.Find("Weapon").GetComponent<ProjectileShooter>().reserveAmmo;
            Destroy(this.gameObject);
            Debug.Log("Pee");
            Debug.Log(GameObject.Find("Weapon").GetComponent<ProjectileShooter>().reserveAmmo);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
