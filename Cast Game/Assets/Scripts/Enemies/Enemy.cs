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
    public int health = 500;
    public int damage = 15;
    public int burnStacks = 0;
    float timer = 0f;
    // public float burnTickRate = 0.5f;
    // public GameObject death;

    /** audio */
    public AudioSource enemyAudioSource;
    public AudioClip burn;

    void Start() {
        enemyAudioSource = GetComponent<AudioSource>(); 
    }

    void Update() {
        if (burnStacks == 0) return; // if the enemy is not burned

        if (timer <= Bullet.burnTickRate) {
            timer += Time.deltaTime;
        } else {
            timer = 0;
            enemyAudioSource.PlayOneShot(burn);
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
        movementWaterMage waterMage = hitInfo.GetComponent<movementWaterMage>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        if(waterMage != null)
        {
            waterMage.TakeDamage(damage);
        }
        
    }


}
