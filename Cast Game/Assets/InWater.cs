using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWater : MonoBehaviour
{
    //public movementWaterMage 

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Non-trigger Colliders entering Collision");
        {
            //movementWaterMage.withinWell(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Non-trigger Colliders exiting Collision");
        {
        //movementWaterMage.withinWell(true);
        }
    }
}
