using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movementWaterMage : MonoBehaviour
{

    public float speed = 5f;
    public float acceleration = 3f;
    public int mana = 1000;
    public Rigidbody2D rbody;
    public Animator anim;
    private bool left;
    private bool up;
    public int health = 100;
    public CorruptionBar healthBar;
    public ManaBar manaBar;
    public GameObject waterPoint;
    public GameObject enemyToReplaceWithWhenCorrupted;
    public GameObject aimingArrow;
    public bool withinWell;
    public int wellManaMultiplier;
    public GameObject DialogueUI;
    public bool charging;
    private weaponWaterMage Weap;
    public GameObject chargeLight;
    public float invincibilityFrames;
    private bool invincible = false;
    Vector2 aim;
    Vector2 move;

    /** audio */
    public AudioSource waterAudioSource;

    public void OnMove(InputAction.CallbackContext context)
    {

        if (inDialogue() || charging)//Cutscene Dialogue Specifically
        {
            move = new Vector2(0, 0);
        }
        else
        {
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
        Weap = GetComponent<weaponWaterMage>();
        waterAudioSource = GetComponent<AudioSource>();
        waterAudioSource.Pause();
        healthBar.SetMaxCorruption(health);
        manaBar.SetMaxMana(mana);
    }

    // Update is called once per frame
    void Update()
    {
        if (!charging)
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
        anim.SetFloat("Speed", move.magnitude);
        if (Mathf.Abs(move.x) > Mathf.Abs(move.y) && move.x > 0)
        {
            //Right
            anim.SetInteger("Last Dir", 2);
        }
        else if (Mathf.Abs(move.x) > Mathf.Abs(move.y) && move.x < 0)
        {
            //Left
            anim.SetInteger("Last Dir", 3);
        }
        else if (Mathf.Abs(move.x) < Mathf.Abs(move.y) && move.y > 0)
        {
            //Up
            anim.SetInteger("Last Dir", 1);
        }
        else if (Mathf.Abs(move.x) < Mathf.Abs(move.y) && move.y < 0)
        {
            //Down
            anim.SetInteger("Last Dir", 0);
        }
        anim.SetBool("Charging", charging);
        Weap.charging = charging;

        if (GetComponent<PlayerInput>().currentControlScheme == "Keyboard")
        {
            aimingArrow.GetComponent<SpriteRenderer>().enabled = false;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = (Vector2)(worldPosition - waterPoint.transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            waterPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            if (aim.magnitude >= .05f)
            {
                aimingArrow.GetComponent<SpriteRenderer>().enabled = true;
                Vector2 worldPosition = (Vector2)waterPoint.transform.position + aim;
                Vector2 dir = (Vector2)(worldPosition - (Vector2)waterPoint.transform.position);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                waterPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                aimingArrow.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
     /*   if (move != Vector2.zero)
        {
            float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
            if (angle == 0 || angle == 90 || angle == 180 || angle == -90)
            {
                waterPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                if (angle == 0)
                {
                    waterPoint.transform.position = new Vector2(transform.position.x + 2, transform.position.y - .5f);

                }
                if (angle == 90)
                {
                    waterPoint.transform.position = new Vector2(transform.position.x + 1.5f, transform.position.y + .5f);
                }
                if (angle == 180)
                {
                    waterPoint.transform.position = new Vector2(transform.position.x - 2, transform.position.y - .5f);

                }
                if (angle == -90)
                {
                    waterPoint.transform.position = new Vector2(transform.position.x - 1, transform.position.y - 2);

                }
            }
        }
        else
        {
            waterPoint.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            waterPoint.transform.position = new Vector2(transform.position.x - 1, transform.position.y - 2);
        }*/
        if(mana > 500)
        {
            mana = 500;
        }
        manaBar.SetMana(mana);
        if (rbody.velocity.x == 0 && rbody.velocity.y == 0) waterAudioSource.Pause();
        else if (!waterAudioSource.isPlaying && (rbody.velocity.x != 0f || rbody.velocity.y != 0f)) waterAudioSource.Play();
    }
    private void FixedUpdate()
    {
        // movement
        rbody.velocity = Vector2.MoveTowards(rbody.velocity, move * speed, acceleration);
        if (charging && mana <= 500)
        {
            if (withinWell)
            {
                mana += wellManaMultiplier;
            }
            else
            {
                mana += 1;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            invincible = true;
            health -= damage;
            StartCoroutine(TakeDamage());
            if (health <= 0)
            {
                health = 0;
                healthBar.SetCorruption(0);
                Corrupt();
            }
            healthBar.SetCorruption(100 - health);
        }
    }

    public void Corrupt()
    {
        Instantiate(enemyToReplaceWithWhenCorrupted, transform.position, transform.rotation);
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private bool inDialogue()
    {
        return !DialogueUI.GetComponent<DialogueSystem.DialogueHolder>().dialogueFinished;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ManaUp"))
        {
            Destroy(col.gameObject);
            mana += 500;
        }
    }


    IEnumerator TakeDamage()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, .6f, .6f, .8f);
        yield return new WaitForSeconds(invincibilityFrames / 3);
        GetComponent<SpriteRenderer>().color = Color.white;
        invincible = true;
        yield return new WaitForSeconds(2 * invincibilityFrames / 3);
        invincible = false;
    }
}
