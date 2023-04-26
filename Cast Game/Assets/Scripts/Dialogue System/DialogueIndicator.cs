using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueIndicator : MonoBehaviour
{

    
    
    public Collider2D interactable;
    public Collider2D playerCollider;
    

    public GameObject alert;
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Non-trigger Colliders entering Collision");
        {
            alert.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Non-trigger Colliders exiting Collision");
        {
        alert.SetActive(false);
        }
    }
}