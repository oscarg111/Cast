using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public AudioSource noMana;
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Start() {
        noMana = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        movement player = gameObject.GetComponent<movement>();
        if (Input.GetButtonDown("Fire1"))
        {

            if (player.mana > 0) {
                //movement play = player.GetComponent<movement>;
            
                player.mana -= 100;
              
                Shoot();
            } else {
                noMana.Play();
            }
            
        }
    }

    void Shoot()
    {
        // shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
