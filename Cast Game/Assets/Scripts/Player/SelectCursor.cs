using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SelectCursor : MonoBehaviour
{
    private Vector2 movementInput = Vector2.zero;
    public bool fired = false;
    public bool chosen = false;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        fired = context.action.triggered;
    }


    private void FixedUpdate()
    {
        if(!chosen)
        {
            transform.Translate((Vector3)movementInput * .1f);
        }
        if(fired)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.01f);
            if (hit.collider != null)
            {
                if ((hit.collider.gameObject.tag == "FireMage" || hit.collider.gameObject.tag == "WaterMage") && hit.collider.gameObject.transform.childCount == 0)
                {
                    transform.parent = hit.collider.gameObject.transform;
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                    chosen = true;
                    GameObject.Find("ControlManager").GetComponent<ControlManager>().connectedDevices++;
                    GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
        //PlayerInput myP = new PlayerInput();
        //myP.SwitchCurrentControlScheme(GetComponent<PlayerInput>().currentControlScheme, GetComponent<PlayerInput>().devices[0]);
    }

}
