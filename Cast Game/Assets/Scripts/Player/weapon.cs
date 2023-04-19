using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class weapon : MonoBehaviour
{
    public bool withinFire = false;
    public float fireCooldownMultiplier;
    public float fireSizeMultiplier;
    public AudioSource noMana;
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

    public void OnFire(InputAction.CallbackContext context)
    {
        fired = context.action.triggered;
    }

    public void OnSecondary(InputAction.CallbackContext context)
    {
        secondary = context.action.triggered;
    }

    void Start() {
        noMana = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        movement player = gameObject.GetComponent<movement>();
        if (fired)
        {
            if(flameThrowerCooldownTimer <= 0)
            {
                if (player.mana > 0)
                {
                    //movement play = player.GetComponent<movement>;

                    player.mana -= 3;
                    Shoot();
                    flameThrowerCooldownTimer = flameThrowerCooldown;
                }
                else
                {
                    noMana.Play();
                }
            }
            
        }
        if(flameThrowerCooldownTimer > 0)
        {
            flameThrowerCooldownTimer -= Time.deltaTime;
        }
        if (secondary)
        {
            if (fireBallCooldownTimer <= 0)
            {
                if (player.mana > 0)
                {
                    //movement play = player.GetComponent<movement>;

                    player.mana -= 3;
                    ShootFireBall();
                    fireBallCooldownTimer = currentFireBallCooldown;
                }
                else
                {
                    noMana.Play();
                }
            }

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
}
