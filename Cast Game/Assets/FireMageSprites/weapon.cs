using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        movement player = gameObject.GetComponent<movement>();
        if (Input.GetButtonDown("Fire1") && player.mana > 0)
        {

            //movement play = player.GetComponent<movement>;
            
            player.mana -= 100;
              
            Shoot();
        }
    }

    void Shoot()
    {
        // shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
