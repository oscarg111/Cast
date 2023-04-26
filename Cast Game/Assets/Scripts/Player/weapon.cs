using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class weapon : MonoBehaviour
{
    public GameObject DialogueUI;
    public bool withinFire = false;
    public float fireCooldownMultiplier;
    public float fireSizeMultiplier;
    public Transform firePoint;
    public GameObject fireSpreadPrefab;
    public GameObject fireBallPrefab;
    private bool fired = false;
    private bool secondary = false;
    public float flameThrowerCooldown = .02f;
    public float fireBallCooldown;
    private float flameThrowerCooldownTimer = 0;
    private float fireBallCooldownTimer = 0;
    private float currentFireBallCooldown;

    /** audio */
    public AudioSource flamethrower;
    public AudioSource noMana;

    public void OnFire(InputAction.CallbackContext context)
    {
        fired = context.action.triggered;
    }

    public void OnSecondary(InputAction.CallbackContext context)
    {
        secondary = context.action.triggered;
    }

    void Start() {
        flamethrower.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        movement player = gameObject.GetComponent<movement>();
        if (fired && !inDialogue())
        {
            if (fireBallCooldownTimer <= 0)
            {
                if (player.mana > 0)
                {
                    //movement play = player.GetComponent<movement>;

                    if (!flamethrower.isPlaying) flamethrower.Play();
                    player.mana -= 3;
                    ShootFireBall();
                    fireBallCooldownTimer = currentFireBallCooldown;
                }
                else
                {
                    flamethrower.Stop();
                    if (!noMana.isPlaying) noMana.Play();
                }
            } // else flamethrower.Pause();
        } 
        else if (secondary && !inDialogue())
        {

            if (flameThrowerCooldownTimer <= 0)
            {
                if (player.mana > 0)
                {
                    //movement play = player.GetComponent<movement>;

                    if (!flamethrower.isPlaying) flamethrower.Play();
                    player.mana -= 3;
                    Shoot();
                    flameThrowerCooldownTimer = flameThrowerCooldown;
                }
                else
                {
                    flamethrower.Stop();
                    if (!noMana.isPlaying) noMana.Play();
                }
            } // else flamethrower.Pause();

        } else flamethrower.Stop();

        if(flameThrowerCooldownTimer > 0)
        {
            flameThrowerCooldownTimer -= Time.deltaTime;
        }
        
        if (fireBallCooldownTimer > 0)
        {
            fireBallCooldownTimer -= Time.deltaTime;
        }
        if(withinFire)
        {
            currentFireBallCooldown = fireBallCooldown / fireCooldownMultiplier;
        }
        else
        {
            currentFireBallCooldown = fireBallCooldown;
        }
    }

    void Shoot()
    {
        // shooting logic
        //noMana.PlayOneShot(firePrimary);
        Instantiate(fireSpreadPrefab, firePoint.position, firePoint.rotation);
    }
    void ShootFireBall()
    {
        // shooting logic
        GameObject fireBall = Instantiate(fireBallPrefab, firePoint.position, firePoint.rotation);
        if(withinFire)
        {
            fireBall.transform.localScale = (Vector2)fireBall.transform.localScale * fireSizeMultiplier;
        }
    }

    private bool inDialogue()
    {
        return !DialogueUI.GetComponent<DialogueSystem.DialogueHolder>().dialogueFinished;
    }
}
