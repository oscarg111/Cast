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
    public GameObject DialogueUI;
    public bool charging = false;
    public GameObject chargeLight;
    private int lastDir = 0;
    private weapon Weap;
    Vector2 move;
    Vector2 aim;

    private CutsceneDialogueController cutsceneTrigger;

    /** audio */
    public AudioSource fireAudioSource; // footstep

    public void OnMove(InputAction.CallbackContext context)
    {

        if (inDialogue() || charging)//Cutscene Dialogue Specifically
        {
            move = new Vector2(0, 0);
        }
        else {
            move = context.ReadValue<Vector2>();
        }
    }

    public void OnCharge(InputAction.CallbackContext context)
    {
        charging = context.action.triggered;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        aim = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        fireAudioSource = GetComponent<AudioSource>();
        Weap = GetComponent<weapon>();
        fireAudioSource.Stop();
        healthBar.SetMaxCorruption(health);
        manaBar.SetMaxMana(mana);
    }

    // Update is called once per frame
    void Update()
    {
        if(!charging)
            chargeLight.SetActive(false);
        if (inDialogue() || charging)//Cutscene Dialogue Specifically
        {
            move = new Vector2(0, 0);
        }
        // adjust animator parameters
        // will uncomment when have actual animations, for now, will set the rotation as 
        // the movement
        anim.SetFloat("Horizontal", move.x);
        anim.SetFloat("Vertical", move.y);
        anim.SetFloat("speed", move.magnitude);
        if(Mathf.Abs(move.x) > Mathf.Abs(move.y) && move.x > 0)
        {
            //Right
            anim.SetInteger("Last Dir", 2);
        }
        else if(Mathf.Abs(move.x) > Mathf.Abs(move.y) && move.x < 0)
        {
            //Left
            anim.SetInteger("Last Dir", 3);
        }
        else if(Mathf.Abs(move.x) < Mathf.Abs(move.y) && move.y > 0)
        {
            //Up
            anim.SetInteger("Last Dir", 1);
        }
        else if(Mathf.Abs(move.x) < Mathf.Abs(move.y) && move.y < 0)
        {
            //Down
            anim.SetInteger("Last Dir", 0);
        }
        anim.SetBool("Charging", charging);
        Weap.charging = charging;
        
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
        if (mana > 500)
        {
            mana = 500;
        }
        manaBar.SetMana(mana);
        if (rbody.velocity.x == 0f && rbody.velocity.y == 0f) fireAudioSource.Stop();
        else if (!fireAudioSource.isPlaying && (rbody.velocity.x != 0f || rbody.velocity.y != 0f)) fireAudioSource.Play();
    }
    private void FixedUpdate()
    {
        // movement
        rbody.velocity = Vector2.MoveTowards(rbody.velocity, move * speed, acceleration);
        if (charging && mana <= 500)
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
        return !DialogueUI.GetComponent<DialogueSystem.DialogueHolder>().dialogueFinished;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cutsceneTrigger = null;
    }
}
