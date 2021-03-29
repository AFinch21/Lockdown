using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    Transform target;
    public float pickUpRadius = 2f;
    public float healthRestored = 50f;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= pickUpRadius && GameObject.Find("Player").GetComponent<PlayerHealth>().currentHealth <= 50f)
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().currentHealth += healthRestored;
            Destroy(this.gameObject);
        }

        if (distance <= pickUpRadius && GameObject.Find("Player").GetComponent<PlayerHealth>().currentHealth > 50f && GameObject.Find("Player").GetComponent<PlayerHealth>().currentHealth < 100)
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().currentHealth += GameObject.Find("Player").GetComponent<PlayerHealth>().maxHealth - GameObject.Find("Player").GetComponent<PlayerHealth>().currentHealth;
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
