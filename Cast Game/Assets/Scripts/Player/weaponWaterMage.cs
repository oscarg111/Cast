using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class weaponWaterMage : MonoBehaviour
{
    public bool withinWell = false;
    public Transform waterPoint;
    public GameObject bulletPrefab;
    private bool fired = false;
    private bool secondary = false;
    public float shotCooldown = .02f;
    public int secondarySpeedMultiplier;
    public float secondarySpreadMultiplier;
    public float wellSpreadMultiplier;
    public float wellCooldownMultiplier;
    private int currentMultiplier;
    private float shotCooldownTimer = 0;
    private float currentCooldown;

    /** audio */
    public AudioSource weaponAudio;
    public AudioClip bubbles;
    public AudioSource noMana;

    public void OnFire(InputAction.CallbackContext context)
    {
        fired = context.action.triggered;
    }

    public void OnSecondary(InputAction.CallbackContext context)
    {
        secondary = context.action.triggered;
    }

    void Start()
    {
        weaponAudio.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        movementWaterMage player = gameObject.GetComponent<movementWaterMage>();
        if (fired)
        {
            if (shotCooldownTimer <= 0)
            {
                for(int i = 0; i < currentMultiplier; i++)
                {  
                    if (player.mana > 0)
                    {
                        //movement play = player.GetComponent<movement>;

                        if (!weaponAudio.isPlaying) weaponAudio.Play();
                        player.mana -= 3;
                        Shoot();
                        shotCooldownTimer = currentCooldown;
                    }
                    else
                    {
                        weaponAudio.Stop();
                        if (!noMana.isPlaying) noMana.Play();
                    }
                }
            }

        } else weaponAudio.Stop();
        if (shotCooldownTimer > 0)
        {
            shotCooldownTimer -= Time.deltaTime;
        }

        if(secondary)
        {
            currentMultiplier = secondarySpeedMultiplier;
        }
        else
        {
            currentMultiplier = 1;
        }
        if(withinWell)
        {
            currentCooldown = shotCooldown / wellCooldownMultiplier;
        }
        else
        {
            currentCooldown = shotCooldown;
        }

    }

    void Shoot()
    {
        // shooting logic
        GameObject currentBullet = Instantiate(bulletPrefab, waterPoint.position, waterPoint.rotation);
        if(secondary)
        {
            currentBullet.GetComponent<WaterBullet>().spread *= secondarySpreadMultiplier;
        }
        if(withinWell)
        {
            currentBullet.GetComponent<WaterBullet>().spread *= wellSpreadMultiplier;
        }
    }
}
