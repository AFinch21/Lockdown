using UnityEngine;

public class EnemyTarget : MonoBehaviour {
    
    public float enemyHealth = 50f;
    public GameObject deadEnemy;

    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0f)
        {
            enemyDie();
        }
    }
    void enemyDie ()
    {
        Destroy(gameObject);
        Instantiate(deadEnemy, transform.position, transform.rotation);

    }

}