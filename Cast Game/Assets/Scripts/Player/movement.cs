using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    public float speed = 5f;
    public float acceleration = 3f;
    public int mana = 500;
    public Rigidbody2D rbody;
    public Animator anim;
    private bool left;
    private bool up;
    public int health = 100;
    public CorruptionBar healthBar;
    public ManaBar manaBar;
    public GameObject firePoint;
    public GameObject enemyToReplaceWithWhenCorrupted;
    public GameObject aimingArrow; 
    public bool withinFire = false;
    public int fireManaMultiplier;
    Vector2 move;
    Vector2 aim;

    private CutsceneDialogueController cutsceneTrigger;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        aim = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        healthBar.SetMaxCorruption(health);
        manaBar.SetMaxMana(mana);
    }

    // Update is called once per frame
    void Update()
    {

        //if (inDialogue)//Cutscene Dialogue Specifically
        //{
            //movement lock and horde spawning pause go here
        //}

        // adjust animator parameters
        // will uncomment when have actual animations, for now, will set the rotation as 
        // the movement
        anim.SetFloat("Horizontal", move.x);
        anim.SetFloat("Vertical", move.y);
        anim.SetFloat("speed", move.sqrMagnitude);
        
        if(GetComponent<PlayerInput>().currentControlScheme == "Keyboard")
        {
            aimingArrow.GetComponent<SpriteRenderer>().enabled = false;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = (Vector2)(worldPosition - firePoint.transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            firePoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            if(aim.magnitude >= .05f)
            {
                aimingArrow.GetComponent<SpriteRenderer>().enabled = true;
                Vector2 worldPosition = (Vector2)firePoint.transform.position + aim;
                Vector2 dir = (Vector2)(worldPosition - (Vector2)firePoint.transform.position);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                firePoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                aimingArrow.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        if (move != Vector2.zero)
        {
            
            /*if (angle == 0 || angle == 90 || angle == 180 || angle == -90)
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
            } */
        }
        
        else
        {
            //firePoint.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            //firePoint.transform.position = new Vector2(transform.position.x - 1, transform.position.y - 2);
        }
        if (move.x == 0 && move.y == 0 && mana <= 500)
        {
            if (withinFire)
            {
                mana += fireManaMultiplier;
            }
            else
            {
                mana += 1;
            }
        }
        if (mana > 500)
        {
            mana = 500;
        }
        manaBar.SetMana(mana);
    }
    private void FixedUpdate()
    {
        // movement
        rbody.velocity = Vector2.MoveTowards(rbody.velocity, move * speed, acceleration);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            healthBar.SetCorruption(0);
            Corrupt();
        }
        healthBar.SetCorruption(100 - health);
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            cutsceneTrigger = collision.gameObject.GetComponent<CutsceneDialogueController>();

            if (Input.GetKey(KeyCode.E))//temp, switch input systems here
                collision.gameObject.GetComponent<CutsceneDialogueController>().ActivateDialogue();
        }
    }

    private bool inDialogue()
    {
        if (cutsceneTrigger != null)
            return cutsceneTrigger.DialogueActive();
        else
            return false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cutsceneTrigger = null;
    }
}
