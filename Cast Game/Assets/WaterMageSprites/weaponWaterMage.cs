using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponWaterMage : MonoBehaviour
{

    public Transform waterPoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        movementWaterMage player = gameObject.GetComponent<movementWaterMage>();
        if (Input.GetButtonDown("Fire2") && player.mana > 0)
        {

            //movement play = player.GetComponent<movement>;

            player.mana -= 100;

            Shoot();
        }
    }

    void Shoot()
    {
        // shooting logic
        Instantiate(bulletPrefab, waterPoint.position, waterPoint.rotation);
    }
}
