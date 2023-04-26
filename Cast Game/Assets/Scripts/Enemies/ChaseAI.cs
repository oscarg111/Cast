using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAI : MonoBehaviour
{
    public GameObject fireMage;  
    public GameObject waterMage;
    public float speed;
    public float chase_distance;
    public float accel = 1;
    private SpriteRenderer sprender;

    // this gets the distance between the player and enemy
    private float fireDistance;
    private float waterDistance;
    private GameObject closer;

    /** audio */
    public AudioSource enemyAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        sprender = GetComponent<SpriteRenderer>();
        fireMage = GameObject.Find("FireMage");
        waterMage = GameObject.Find("WaterMage");
    }

    // Update is called once per frame
    /*
     Get the distance from the enemy to fire or water
    check to see which one is closer
    if water closer 
        move to them 
    else
        move to fire
     */

    void FixedUpdate() {
        if (fireMage != null && waterMage != null) bothAlive();
        else if (fireMage != null && waterMage == null) oneAlive(fireMage);
        else if (fireMage == null && waterMage != null) oneAlive(waterMage);
        else this.enabled = false;
    }

    void bothAlive()
    {
        fireDistance = Vector2.Distance(transform.position, fireMage.transform.position);
        waterDistance = Vector2.Distance(transform.position, waterMage.transform.position);
		
        // Only allows for chase if the player is close enough
        if(fireDistance < chase_distance || waterDistance < chase_distance)
        {
            if(GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                sprender.flipX = true;
            }
            else
            {
                sprender.flipX = false;
            }
            if (!enemyAudioSource.isPlaying) enemyAudioSource.Play();
            if (fireDistance <= waterDistance) closer = fireMage;
            else closer = waterMage;
            
            Vector2 direction = closer.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GetComponent<Rigidbody2D>().velocity = Vector2.MoveTowards(GetComponent<Rigidbody2D>().velocity, (Vector2)(closer.transform.position - transform.position).normalized * speed, accel*Time.fixedDeltaTime);
            //transform.position = Vector2.MoveTowards(this.transform.position, closer.transform.position, speed * Time.deltaTime);
        } else enemyAudioSource.Pause();
    }

    void oneAlive(GameObject player) 
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		
        // Only allows for chase if the player is close enough
        if(distance < chase_distance)
        {
            if (GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                sprender.flipX = true;
            }
            else
            {
                sprender.flipX = false;
            }
            if (!enemyAudioSource.isPlaying) enemyAudioSource.Play();
            GetComponent<Rigidbody2D>().velocity = Vector2.MoveTowards(GetComponent<Rigidbody2D>().velocity, (Vector2)(player.transform.position - transform.position).normalized * speed, accel * Time.fixedDeltaTime);
            //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        } else enemyAudioSource.Pause();
    }
}
