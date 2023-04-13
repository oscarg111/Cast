using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    public float speed = 5f;
    public int mana = 1000;
    public Rigidbody2D rbody;
    public Animator anim;
    private bool left;
    private bool up;
    public int health = 100;
    public CorruptionBar healthBar;
    public ManaBar manaBar;
    public GameObject firePoint;
    public GameObject enemyToReplaceWithWhenCorrupted;
    Vector2 move;

    private void Start()
    {
        healthBar.SetMinCorruption(100 - health);
        manaBar.SetMaxMana(mana);
    }

    // Update is called once per frame
    void Update()
    {
        // input
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        // adjust animator parameters
        // will uncomment when have actual animations, for now, will set the rotation as 
        // the movement
        anim.SetFloat("Horizontal", move.x);
        anim.SetFloat("Vertical", move.y);
        anim.SetFloat("speed", move.sqrMagnitude);
        
        if (move != Vector2.zero)
        {
            float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
            if (angle == 0 || angle == 90 || angle == 180 || angle == -90)
            {
                firePoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                if (angle == 0)
                {
                    firePoint.transform.position = new Vector2(transform.position.x + 2, transform.position.y - .5f);

                }
                if (angle == 90)
                {
                    firePoint.transform.position = new Vector2(transform.position.x + 1.5f, transform.position.y + .5f);
                }
                if (angle == 180)
                {
                    firePoint.transform.position = new Vector2(transform.position.x - 2, transform.position.y - .5f);

                }
                if (angle == -90)
                {
                    firePoint.transform.position = new Vector2(transform.position.x - 1, transform.position.y - 2);

                }
            } 
        }
        
        else
        {
            //firePoint.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            //firePoint.transform.position = new Vector2(transform.position.x - 1, transform.position.y - 2);
        }
        if (move.x == 0 && move.y == 0 && mana <= 1000)
        {
            mana += 1;
        }
        manaBar.SetMana(mana);
    }
    private void FixedUpdate()
    {
        // movement
        rbody.MovePosition(rbody.position + move * speed * Time.fixedDeltaTime);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetCorruption(100 - health);
        if (health <= 0)
        {
            Corrupt();
        }
    }

    public void Corrupt() {
        Instantiate(enemyToReplaceWithWhenCorrupted, transform.position, transform.rotation);
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ManaUp"))
        { 
            Destroy(col.gameObject);
            if (mana + 500 > 1000)
            {
                mana = 1000;
            }
            else
            {
                mana += 500;
            }
            
        }
    }
}
