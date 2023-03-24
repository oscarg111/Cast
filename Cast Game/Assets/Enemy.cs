using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    
    /* reverse health bar system suggested where this value would start at 0
     * and tick upwards. The enemy would die when the value becomes high enough
     * (so basically, the reverse of the current implementation)
     */
    public int health = 100;
    public int damage = 15;
    public int burnStacks = 0;
    float timer = 0f;
    // public float burnTickRate = 0.5f;
    // public GameObject death;

    void Update() {
        if (burnStacks == 0) return; // if the enemy is not burned

        if (timer <= Bullet.burnTickRate) {
            timer += Time.deltaTime;
        } else {
            timer = 0;
            TakeDamage(burnStacks*Bullet.burnDamage); 
        } // if
    } // Update

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Instantiate(death, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        movement player = hitInfo.GetComponent<movement>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        
    }


}
