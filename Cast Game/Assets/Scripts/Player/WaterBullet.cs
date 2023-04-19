using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float spread;
    // Start is called before the first frame update
    void Start()
    {
        float Rand = Random.Range(.75f, 2f);
        transform.localScale = new Vector3(transform.localScale.x * Rand, transform.localScale.y * Rand, transform.localScale.z);
        transform.Rotate(new Vector3(0, 0, Random.Range(-spread, spread)));
        rb.velocity = (Vector2)transform.right * speed + (Vector2)GameObject.Find("WaterMage").GetComponent<Rigidbody2D>().velocity;
        Destroy(gameObject, .75f); //will destroy the bullet 5 seconds
    }

}
