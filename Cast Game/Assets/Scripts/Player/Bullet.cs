using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public static float burnTickRate = 1.5f; // # of seconds between each tick
    public static int burnDamage = 5; // damage per tick
    public Rigidbody2D rb;
    public float spread;
    // Start is called before the first frame update
    void Start()
    {
        float Rand = Random.Range(.9f, 1.1f);
        transform.localScale = new Vector3(transform.localScale.x * Rand, transform.localScale.y * Rand, transform.localScale.z);
        transform.Rotate(new Vector3(0, 0, Random.Range(-spread, spread)));
        rb.velocity = (Vector2)transform.right * speed + (Vector2)GameObject.Find("FireMage").GetComponent<Rigidbody2D>().velocity;
        Destroy(gameObject, 1); //will destroy the bullet 5 seconds
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            enemy.burnStacks++;
            Destroy(gameObject);
        }
        
    }

}
