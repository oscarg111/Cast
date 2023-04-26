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
    public AudioSource playerAudioSource;
    public AudioClip burn;
    public AudioClip die;
    private bool dying = false;

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
        if(!dying)
        {
            enemyAudioSource.Stop();
            dying = true;
            // Instantiate(death, transform.position, Quaternion.identity);
            enemyAudioSource.PlayOneShot(die);
            StartCoroutine(DieCoroutine());
        }
    }

    IEnumerator DieCoroutine()
    {
        GetComponent<ChaseAI>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Destroy(transform.GetChild(0).gameObject);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D hitInfo)
    {
        movement player = hitInfo.gameObject.GetComponent<movement>();
        movementWaterMage waterMage = hitInfo.gameObject.GetComponent<movementWaterMage>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        if (waterMage != null)
        {
            waterMage.TakeDamage(damage);
        }
    }
}
