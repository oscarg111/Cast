using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class weapon : MonoBehaviour
{
    public AudioSource noMana;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private bool fired = false;
    public float shotCooldown = .02f;
    private float shotCooldownTimer = 0;

    public void OnFire(InputAction.CallbackContext context)
    {
        fired = context.action.triggered;
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
            if(shotCooldownTimer <= 0)
            {
                if (player.mana > 0)
                {
                    //movement play = player.GetComponent<movement>;

                    player.mana -= 100;
                    Shoot();
                    shotCooldownTimer = shotCooldown;
                }
                else
                {
                    noMana.Play();
                }
            }
            
        }
        if(shotCooldownTimer > 0)
        {
            shotCooldownTimer -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        // shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
