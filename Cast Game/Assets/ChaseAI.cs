using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAI : MonoBehaviour
{
    public GameObject player;  
    public float speed;
    public float chase_distance;

    // this gets the distance between the player and enemy
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        
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
    void Update()
    {
        if (player == null) this.enabled = false;
        else {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		
            // Only allows for chase if the player is close enough
            if(distance < chase_distance)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
        }
    }
}
