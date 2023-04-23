using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    public bool pressed = false;
    public void OnPress(InputAction.CallbackContext context)
    {
        pressed = context.performed;
    }

    private void LateUpdate()
    {
        pressed = false;
    }
}
