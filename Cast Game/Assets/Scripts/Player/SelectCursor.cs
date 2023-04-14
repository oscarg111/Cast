using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectCursor : MonoBehaviour
{
    private Vector2 movementInput = Vector2.zero;
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }



    private void FixedUpdate()
    {
        transform.Translate((Vector3)movementInput * .1f);
    }
}
